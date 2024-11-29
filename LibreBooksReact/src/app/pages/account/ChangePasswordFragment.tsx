import { ChangeEvent, FormEvent, useCallback, useState } from "react";
import { IFormField, useFormUtils } from "../../../core/forms";
import { useAppSettings, useHttp, usePageTitle, useValidators } from "../../../hooks";
import CreatePasswordComponent from "./components/CreatePasswordComponent";
import { Button, Divider, Field, Input, makeStyles, Spinner, Toast, ToastTitle, tokens, Tooltip } from "@fluentui/react-components";
import { TbLock, TbLockOpen2, TbX } from "react-icons/tb";
import { ajax, AjaxError } from "rxjs/ajax";
import { ApiRoutes } from "../../../strings";
import { StatusCodes } from "http-status-codes";
import { useAppContext } from "../../../contexts/AppContext";
import { intent } from "../../../strings/ui";
import { from } from "rxjs";
import { ITransactionResult } from "../../../core/Transactions";

interface IChangePasswordModel {
    [key: string]: IFormField<string>
    password: IFormField<string>
    confirmPassword: IFormField<string>
    oldPassword: IFormField<string>
}

const initialChangePasswordModel: IChangePasswordModel = {
    password: {
        value: ""
    } as IFormField<string>,
    confirmPassword: {
        value: ""
    } as IFormField<string>,
    oldPassword: {
        value: ""
    } as IFormField<string>,
}


export default function ChangePasswordFragment() {
    usePageTitle("Change Password")
    const styles = MakeChangePasswordFragmentStyles()
    const { passwordValidator } = useValidators()
    const { fieldErrors } = useFormUtils()
    const { createApiPath } = useAppSettings()
    const { toaster } = useAppContext()
    const { headers } = useHttp()
    const [model, setModel] = useState<IChangePasswordModel>(initialChangePasswordModel)
    const [showPassword, setShowPassword] = useState(false)
    const [loading, setLoading] = useState(false)

    function onInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { value, name } = event.target

        const newVal = value.replace(/\s+/g, "")

        if (name.endsWith("word")) {
            if (newVal != model[name].value) {
                setModel(state => {
                    const newModel = {
                        ...state,
                        [name]: {
                            value: newVal
                        } as IFormField<string>
                    }
                    if (name == "password" && model.confirmPassword.value)
                        newModel.confirmPassword = { value: "" } as IFormField<string>

                    if (name == "confirmPassword") {
                        if (!model.password.value) {
                            newModel.password.error = "Password is requred."
                            newModel.confirmPassword.value = ""
                        }
                        else {
                            if (newVal !== model.password.value)
                                newModel.confirmPassword.error = "Password do not match."
                            else if (value == model.password.value && model.confirmPassword.error)
                                newModel.confirmPassword.error = undefined
                        }
                    }

                    return newModel
                })
            }
        } else {
            if (model[name].value != newVal)
                setModel(state => ({
                    ...state,
                    [name]: {
                        value: newVal
                    } as IFormField<string>
                }))
        }
    }

    const renderPasswordToggleElement = useCallback(() => {
        return (<Tooltip
            content={`Click to ${showPassword ? "hide" : "show"} password`}
            relationship="label">
            <Button appearance="subtle"
                size="small"
                onClick={() => setShowPassword(state => !state)}
                icon={!showPassword ? <TbLock size={16} /> : <TbLockOpen2 size={16} />} />
        </Tooltip>)
    }, [showPassword])

    function validateModel() {
        let valid = true;
        const validatedModel = { ...model }

        if (!model.oldPassword.value) {
            validatedModel.oldPassword.error = fieldErrors.required("Current password")
            valid = false;
        }

        if (!model.password.value) {
            validatedModel.password.error = fieldErrors.required("Password")
            valid = false;
        } else if (!passwordValidator.validate(model.password.value)) {
            validatedModel.password.error = fieldErrors.passwordWeak
            valid = false;
        }

        if (model.password.value && model.confirmPassword)
            if (model.password.value !== model.confirmPassword.value) {
                validatedModel.confirmPassword.error = fieldErrors.passwordMismatch
            }

        if (!valid)
            setModel(validatedModel)

        return valid
    }

    function onFormSubmit(event: FormEvent) {
        event.preventDefault()
        setLoading(true)

        if (!validateModel()) {
            setLoading(false)
            return
        }

        ajax<ITransactionResult<unknown>>({
            url: createApiPath(ApiRoutes.Account.ChangePassword),
            withCredentials: true,
            body: JSON.stringify({
                oldPassword: model.oldPassword.value,
                password: model.password.value
            }),
            method: "POST",
            headers: {
                [headers.contentType.name]: headers.contentType.values.Json
            }
        }).subscribe({
            next: (resp) => {
                console.log(resp)
                const { succeeded, errors } = resp.response
                if (succeeded) {
                    clearModelState()
                    toaster.dispatchToast(
                        <Toast>
                            <ToastTitle>Password was successfully changed.</ToastTitle>
                        </Toast>, {
                        intent: intent.success
                    })
                } else if (errors.length > 0) {
                    const newModel = { ...model }
                    from(errors).subscribe(error => {
                        const key = error.code![0].toLocaleLowerCase() + error.code!.slice(1)
                        newModel[key].error = error.description!
                    })
                    setModel(newModel)
                }
                setLoading(false)
            },
            error: (error: AjaxError) => {
                if (error.status === StatusCodes.BAD_REQUEST) {
                    console.log(error)
                    const errors = error.response.errors ?? error.response
                    const errorKeys = Object.keys(errors)

                    if (errorKeys.length > 0) {
                        const _newModel = { ...model }

                        from(errorKeys).subscribe(key => {
                            const _key = key[0].toLocaleLowerCase() + key.slice(1)

                            if (!Object.keys(_newModel).includes(_key))
                                return console.log("incorrect key")

                            try {
                                _newModel[_key].error = errors[key]![0]
                            } catch {
                                console.log(`Exception Occured. Failed to retrieve object with key ${_key}`)
                            }
                        })
                        setModel(_newModel)
                    }
                }
                setLoading(false)
            }
        })
    }

    function clearModelState() {
        setModel(initialChangePasswordModel)
    }

    return (
        <form
            onSubmit={onFormSubmit}
            className={`animate__animated animate__fadeIn ${styles}`}>
            <div className={`${styles.formFields} row`}>
                <div className="col-12">
                    <Divider appearance="strong">Confirm your current password.</Divider>
                </div>
                <div className="col-12">
                    <Field className={styles.field} label={"Current Password"}
                        validationMessage={model.oldPassword.error}
                        validationState={
                            (
                                (model.oldPassword.value ? !passwordValidator.validate(model.oldPassword.value) : false) || model.oldPassword.error
                                    ? "error"
                                    : (model.oldPassword.value && passwordValidator.validate(model.oldPassword.value) ? "success" : "none")
                            )
                        }>
                        <Input value={model.oldPassword.value}
                            onChange={onInputChange}
                            autoComplete="off"
                            //placeholder="Enter your current password"
                            name="oldPassword"
                            disabled={loading}
                            contentAfter={renderPasswordToggleElement()}
                            type={showPassword ? "text" : "password"}
                            aria-label="Current Password input" />
                    </Field>
                </div>
                <div className={`col-12 ${styles.dividerWrapper}`}>
                    <Divider appearance="strong">Create your new password.</Divider>
                </div>
                <CreatePasswordComponent showPassword={showPassword}
                    password={model.password}
                    confirmPassword={model.confirmPassword}
                    onInputChange={onInputChange}
                    disabled={loading}
                    passwordToggleElement={renderPasswordToggleElement()} />
                <div className='col-4'>
                    <Button onClick={clearModelState} disabled={loading} appearance="secondary" icon={<TbX />}>Reset</Button>
                </div>
                <div className='col-8'>
                    <Button appearance='primary'
                        type='submit'
                        disabled={loading}
                        className={styles.submitButton}>
                        {loading ? <Spinner label="Updating..." appearance='inverted' size="tiny" /> : "Change Password"}
                    </Button>
                </div>
            </div>
        </form>
    )
}

const MakeChangePasswordFragmentStyles = makeStyles({
    root: {

    },
    field: {

    },
    submitButton: {
        width: "100%"
    },
    formFields: {
        width: "100%",
        maxWidth: "340px",
        margin: " 0 auto",
        rowGap: tokens.spacingVerticalS
    },
    dividerWrapper:
    {
        paddingTop: tokens.spacingVerticalM
    }
})