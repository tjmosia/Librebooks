
import { ChangeEvent, FormEvent, useEffect, useState } from 'react'
import { Button, Divider, Field, Input, Spinner, Tag, Text, Toast, ToastBody, ToastTitle, makeStyles, tokens } from '@fluentui/react-components'
import { IFormField } from '../../../core/forms'
import { useFormUtils } from '../../../core/forms/FormsHook'
import { useAppSettings, useHttp, usePageTitle } from '../../../hooks'
import { ITransactionResult } from '../../../core/extensions/TransactionTypes'
import { IAppUser } from '../../../types/identity'
import { ajax, AjaxError, AjaxResponse } from 'rxjs/ajax'
import { StatusCodes } from 'http-status-codes'
import useIdentityManager from '../../../hooks/IdentityManager'
import { from } from 'rxjs'
import { Animations, ApiRoutes } from '../../../strings'
import { useAppContext } from '../../../contexts/AppContext'
import { useValidators } from '../../../core/extensions'

interface IContactInfoFragmentModel {
    [key: string]: IFormField<string>
    email: IFormField<string>
    code: IFormField<string>
}

export default function ContactInfoFragment() {
    /************************************************************************************************************************************************
     * INJECTABLES
     ***********************************************************************************************************************************************/
    usePageTitle("Contact Info")
    const styles = MakePersonalInfoFragmentStyles()
    const { fieldErrors } = useFormUtils()
    const [loading, setLoading] = useState(false)
    const { headers } = useHttp()
    const { createApiPath } = useAppSettings()
    const identityManager = useIdentityManager()
    const user = identityManager.getUser()!
    const appContext = useAppContext()

    /************************************************************************************************************************************************
     * STATE
     ***********************************************************************************************************************************************/
    const [requestLoading, setRequestLoading] = useState(false)
    const { emailvalidator } = useValidators()
    const [codeHashString, setCodeHashString] = useState<string | undefined>()
    const [model, setModel] = useState<IContactInfoFragmentModel>({
        code: {
            value: ""
        },
        email: {
            value: ""
        }
    })

    /************************************************************************************************************************************************
     * METHODS
     ***********************************************************************************************************************************************/
    function clearModelState() {
        setModel({
            code: { value: "" },
            email: { value: "" }
        })
    }

    function onFormSubmit(event: FormEvent) {
        event.preventDefault()
        setLoading(true)

        if (!validateModel()) {
            setLoading(false)
            return;
        }

        ajax<ITransactionResult<IAppUser>>({
            url: createApiPath(ApiRoutes.Account.UpdateContactInfo),
            method: "POST",
            body: JSON.stringify({
                email: model.email.value,
                code: model.code.value,
                codeHashString: codeHashString,
            }),
            headers: {
                [headers.contentType.name]: headers.contentType.values.Json
            },
            withCredentials: true
        }).subscribe({
            next: (resp: AjaxResponse<ITransactionResult<IAppUser>>) => {
                const { succeeded, errors } = resp.response
                setLoading(false)
                if (succeeded) {
                    clearModelState()
                    identityManager.confirmSignIn()
                    appContext.toaster.dispatchToast(
                        <Toast>
                            <ToastTitle>Contact Info Updated!</ToastTitle>
                            <ToastBody >Your email address was successfully changed.</ToastBody>
                        </Toast>, {
                        intent: "success",
                        politeness: "polite",
                        timeout: Animations.ToastTimeOut
                    })
                } else if (errors.length > 0) {
                    const newModel = { ...model }
                    from(errors).subscribe(error => {
                        const _key = error.code![0].toLocaleLowerCase() + error.code!.slice(1)
                        newModel[_key].error = error.description ?? undefined
                    })
                    setModel(newModel)
                }
            },
            error: (error: AjaxError) => {
                if (error.status === StatusCodes.BAD_REQUEST) {
                    const errors = error.response.errors ?? error.response
                    const errorKeys = Object.keys(errors)

                    if (errorKeys.length > 0) {
                        const updatedModel = { ...model }
                        from(errorKeys)
                            .subscribe(key => {
                                if (!Object.keys(updatedModel).includes(key))
                                    console.log("incorrect key")
                                const _key = key[0].toLocaleLowerCase() + key.slice(1)
                                try {
                                    updatedModel[_key].error = errors[key]![0]
                                } catch {
                                    console.log(`Exception Occured. Failed to retrieve object with key ${_key}`)
                                }
                            })
                        setModel(updatedModel)
                    }
                }

                setLoading(false)

                if (error.status === StatusCodes.UNAUTHORIZED)
                    identityManager.confirmSignIn()
            }
        })
    }

    function onInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { value, name } = event.target
        const newVal = value.replace(/\s+/g, "")

        if (model[name].value != newVal) {
            setModel(state => ({
                ...state,
                [name]: {
                    value: name === "code" ? newVal.substring(0, 8) : newVal.toLowerCase()
                }
            }))
        }
    }

    function validateModel() {
        let valid = true;
        const validatedModel = { ...model }

        if (!model.email.value) {
            validatedModel.email.error = fieldErrors.required("email")
            valid = false;
        }

        if (!model.code.value) {
            model.code.error = fieldErrors.required("Verification code")
            valid = false;
        }

        if (!valid)
            setModel(validatedModel)

        return valid
    }

    function emailIsValid() {
        if (model.email.error)
            return

        const validatedModel = { ...model }

        if (!model.email.value) {
            validatedModel.email.error = "Email is required."
        } else {
            if (!emailvalidator.validate(model.email.value ?? ""))
                validatedModel.email.error = "Enter a valid email address."

            if (model.email.value == user.email)
                validatedModel.email.error = "You're already using this email."
        }

        if (validatedModel.email.error) {
            setModel(validatedModel)
            return false
        }

        return true
    }

    function onRequestButtonClick() {
        if (!emailIsValid() || model.email.error)
            return

        setRequestLoading(true)
        const currentModel = model
        ajax<ITransactionResult<{ codeHashString: string }>>({
            url: createApiPath(ApiRoutes.Account.SendVerificationCode),
            method: "POST",
            body: JSON.stringify({
                email: currentModel.email.value
            }),
            headers: {
                [headers.contentType.name]: headers.contentType.values.Json
            },
            withCredentials: true
        }).subscribe({
            next: resp => {
                const newModel = { ...currentModel }
                const { succeeded, errors, model } = resp.response

                if (succeeded) {
                    setCodeHashString(model?.codeHashString ?? "")
                    appContext.toaster.dispatchToast(
                        <Toast>
                            <ToastTitle>Verification Code Sent!</ToastTitle>
                            <ToastBody >We've sent a verification code to your inbox.</ToastBody>
                        </Toast>, {
                        intent: "success",
                        politeness: "polite",
                        timeout: Animations.ToastTimeOut
                    })
                    newModel.code.error = undefined
                    newModel.code.value = ""
                } else if (errors.length > 0) {
                    from(errors).subscribe(error => {
                        newModel.email = {
                            value: currentModel.email.value,
                            error: error.description ?? undefined
                        }

                        newModel.code = {
                            value: ""
                        }
                    })
                }

                setModel(newModel)
                setRequestLoading(false)
            },
            error: (error: AjaxError) => {
                if (error.status === StatusCodes.BAD_REQUEST) {
                    const errors = error.response.errors
                    const errorKeys = Object.keys(errors)

                    if (errorKeys.length > 0) {
                        const _newModel = { ...model }

                        from(errorKeys).subscribe(key => {
                            const _key = key[0].toLocaleLowerCase() + key.slice(1)
                            try {
                                _newModel[_key].error = errors[key]![0]
                            } catch {
                                console.log(`Exception Occured. Failed to retrieve object with key ${_key}`)
                            }
                        })

                        setModel(_newModel)
                    }
                }
                setRequestLoading(false)
                if (error.status === StatusCodes.UNAUTHORIZED)
                    identityManager.confirmSignIn()
            }
        })
    }

    /************************************************************************************************************************************************
     * EFFECTS
     ***********************************************************************************************************************************************/
    useEffect(() => {
        if (codeHashString)
            console.log(codeHashString)
    }, [codeHashString])

    useEffect(() => {
        if (model.email.value && model.code.value) {
            setModel(state => ({
                ...state,
                code: {
                    value: ""
                }
            }))
        }
    }, [model.email.value, model.code.value])

    return (<>
        <form onSubmit={onFormSubmit}
            className='animate__animated animate__fadeIn'
            method="post">
            <div className={styles.formFieldsWrapper}>
                <div className='row'>
                    <div className='col-12'>
                        <Tag appearance='brand' shape='circular' className={styles.emailTag}>
                            {user.email}
                        </Tag>
                    </div>
                    <div className="col-12 mb-2 mt-2">
                        <Divider>New Email</Divider>
                    </div>
                    <div className='col-12'>
                        <Field label="Email"
                            className={styles.field}
                            validationState={model.email.error ? "error" : "none"}
                            validationMessage={model.email.error}>
                            <Input
                                value={model.email.value}
                                name="email"
                                aria-label='Email Input'
                                disabled={loading || requestLoading}
                                onChange={onInputChange}
                            />
                        </Field>
                    </div>
                    {/* <div className="col-12 mb-2 mt-2">
                        <Divider>Verify Your New Email</Divider>
                    </div> */}
                    <div className="col-12 mb-2">
                        <Text size={200} weight='regular' align='center' wrap>
                            You are required to verify your new email. Request a verification code and enter it below to continue.
                        </Text>
                    </div>
                    <div className='col-12'>
                        <Field label="Verification Code"
                            className={styles.field}
                            validationState={model.code.error ? "error" : "none"}
                            validationMessage={model.code.error}>
                            <Input
                                value={model.code.value}
                                name="code"
                                input={{
                                    style: {
                                        letterSpacing: tokens.spacingHorizontalM
                                    }
                                }}
                                placeholder='--------'
                                aria-label='Verification Code Input'
                                disabled={loading || requestLoading}
                                onChange={onInputChange}
                                contentAfter={
                                    <Button type='button'
                                        onClick={onRequestButtonClick}
                                        size='small'
                                        appearance='primary'
                                        disabled={loading || requestLoading}>
                                        {requestLoading ? <Spinner appearance='inverted' size="extra-tiny" /> : "Request"}
                                    </Button>
                                }
                            />
                        </Field>
                    </div>
                    <div className='col-12 mt-2'>
                        <Button appearance='primary'
                            type='submit'
                            disabled={loading || requestLoading}
                            className={styles.submitButton}>
                            {loading ? <Spinner appearance='inverted' size="tiny" /> : "Change Email"}
                        </Button>
                    </div>
                </div>
            </div>
        </form>
    </>)
}

const MakePersonalInfoFragmentStyles = makeStyles({
    wrapper: {
        display: "flex",
        flexDirection: "column"
    },
    field: {
        display: "flex",
        flexDirection: "column",
        marginBottom: `${tokens.spacingVerticalM}`,
        width: "100%"
    },
    submitButton: {
        width: "100%"
    },
    formFieldsWrapper: {
        display: 'flex',
        flexDirection: "column",
        maxWidth: "340px",
        margin: "0 auto"
    },
    successAlert: {
        marginBottom: tokens.spacingVerticalM
    },
    emailTag: {
        width: "100%",
        textAlign: "center"
    }
})
