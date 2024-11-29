import { ChangeEvent, FormEvent, useCallback, useState } from 'react'
import { Button, Field, Input, Select, SelectOnChangeData, Spinner, Toast, ToastBody, ToastTitle, makeStyles, tokens } from '@fluentui/react-components'
import { DatePicker } from '@fluentui/react-datepicker-compat'
import { BsCalendarPlus } from 'react-icons/bs'
import { IFormField } from '../../../core/forms'
import { useFormUtils } from '../../../core/forms/forms.hook'
import { useAppSettings, useHttp, usePageTitle } from '../../../hooks'
import { ITransactionResult } from '../../../core/Transactions'
import { IAppUser } from '../../../types/identity'
import { ajax, AjaxError, AjaxResponse } from 'rxjs/ajax'
import { StatusCodes } from 'http-status-codes'
import useIdentityManager from '../../../hooks/IdentityManager'
import { from } from 'rxjs'
import { Animations, ApiRoutes } from '../../../strings'
//import { useIdentityStore } from '../../../stores'
import { useAppContext } from '../../../contexts/AppContext'

interface IPersonalInfoFragmentModel {
    [key: string]: IFormField<string | undefined> | IFormField<Date | undefined | null>
    firstName: IFormField<string>,
    lastName: IFormField<string | undefined>,
    gender: IFormField<string | undefined>,
    birthday: IFormField<Date | undefined | null>
}

export default function PersonalInfoFragment() {
    usePageTitle("Personal Info")
    const styles = MakePersonalInfoPageStyles()
    const { fieldErrors } = useFormUtils()
    const [loading, setLoading] = useState(false)
    const { headers } = useHttp()
    const { createApiPath } = useAppSettings()
    const identityManager = useIdentityManager()
    const user = identityManager.getUser()!
    const appContext = useAppContext()

    const [model, setModel] = useState<IPersonalInfoFragmentModel>({
        firstName: { value: user.firstName },
        lastName: { value: user.lastName },
        email: { value: user.email },
        birthday: { value: new Date(user!.birthday!) },
        gender: { value: user.gender },
    })

    function handleFormSubmitEvent(event: FormEvent) {
        event.preventDefault()
        setLoading(true)

        if (!validateModel()) {
            setLoading(false)
            return;
        }

        ajax<ITransactionResult<IAppUser>>({
            url: createApiPath(ApiRoutes.Account.UpdatePersonInfo),
            method: "POST",
            body: JSON.stringify({
                firstName: model.firstName.value,
                lastName: model.lastName.value,
                birthday: model.birthday?.value,
                gender: model.gender.value
            }),
            headers: {
                [headers.contentType.name]: headers.contentType.values.Json
            },
            withCredentials: true
        }).subscribe({
            next: (response: AjaxResponse<ITransactionResult<IAppUser>>) => {
                if (response.status === StatusCodes.OK) {
                    const data = response.response
                    identityManager.updateUser(data.model!)
                    console.log()
                    appContext.toaster.dispatchToast(
                        <Toast>
                            <ToastTitle>Personal Info Updated!</ToastTitle>
                            <ToastBody >Your personal information was successfully updated.</ToastBody>
                        </Toast>, {
                        intent: "success",
                        politeness: "polite",
                        timeout: Animations.ToastTimeOut
                    })
                }

                setLoading(false)
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

    function inputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
        const { value, name } = event.target

        const newVal = value.replace(/\s+/g, "")

        if (model[name].value != newVal)
            setModel(state => ({
                ...state,
                [name]: {
                    value: newVal
                }
            }))
    }

    const validateModel = useCallback(() => {
        let valid = true;
        const validatedModel = { ...model }
        if (!model.firstName.value) {
            validatedModel.firstName.error = fieldErrors.required("First name")
            valid = false;
        }
        if (!model.lastName.value) {
            validatedModel.lastName.error = fieldErrors.required("Last name")
            valid = false;
        }
        if (!model.birthday.value) {
            validatedModel.birthday.error = fieldErrors.required("Birthday")
            valid = false;
        }
        if (!model.gender.value) {
            validatedModel.gender.error = fieldErrors.required("Gender")
            valid = false;
        }

        if (!valid)
            setModel(validatedModel)

        return valid
    }, [fieldErrors, model])

    function SelectChangeEventHandler(_event: ChangeEvent<HTMLSelectElement>, data: SelectOnChangeData) {
        setModel(state => ({
            ...state,
            gender: {
                value: data.value,
                error: ""
            }
        }))
    }

    function DateSelectEventHandler(date: typeof model.birthday.value) {
        setModel(state => ({
            ...state,
            birthday: {
                value: date,
                error: ""
            }
        }))
    }

    function onFormatDate(date?: Date) {
        return !date ? "" : (date.getFullYear()) + "/" + (date.getMonth() + 1) + "/" + date.getDate()
    };

    return (<>
        <form onSubmit={handleFormSubmitEvent}
            className='animate__animated animate__fadeIn'
            method="post">
            <div className={styles.formFieldsWrapper + " row"}>
                <div className='col-12'>
                    <Field label="First name"
                        className={styles.field}
                        validationState={model.firstName.error ? "error" : "none"}
                        validationMessage={model.firstName.error}>
                        <Input
                            value={model.firstName.value}
                            name="firstName"
                            //appearance="underline"
                            aria-label='First Name Input'
                            disabled={loading}
                            onChange={inputChangeHandler}
                        />
                    </Field>
                </div>
                <div className='col-12'>
                    <Field label="Last name"
                        className={styles.field}
                        validationState={model.lastName.error ? "error" : "none"}
                        validationMessage={model.lastName.error}>
                        <Input
                            value={model.lastName.value}
                            name="lastName"
                            //appearance="underline"
                            aria-label='Last Name Input'
                            disabled={loading}
                            onChange={inputChangeHandler}
                        />
                    </Field>
                </div>
                <div className='col-12'>
                    <Field label="Birthday"
                        className={styles.field}
                        validationState={model.birthday.error ? "error" : "none"}
                        validationMessage={model.birthday.error}>
                        <DatePicker
                            className={styles.dateControl}
                            required
                            onSelectDate={DateSelectEventHandler}
                            formatDate={onFormatDate}
                            type='text'
                            aria-label='Birthday Input'
                            //appearance="underline"
                            name='birthday'
                            disabled={loading}
                            contentAfter={<BsCalendarPlus size={16} />}
                            value={model.birthday.value ? new Date(model.birthday.value) : null}
                        />
                    </Field>
                </div>
                <div className='col-12'>
                    <Field label="Gender"
                        className={styles.field}
                        validationState={model.gender.error ? "error" : "none"}
                        validationMessage={model.gender.error}>
                        <Select name='gender'
                            disabled={loading}
                            //appearance="underline"
                            value={model.gender.value}
                            onChange={SelectChangeEventHandler}>
                            <option value="" hidden></option>
                            <option value="Male">Male</option>
                            <option value="Female">Female</option>
                        </Select>
                    </Field>
                </div>
                <div className='col-12 mt-2'>
                    <Button appearance='primary'
                        type='submit'
                        disabled={loading}
                        className={styles.submitButton}>
                        {loading ? <Spinner label="Updating..." appearance='inverted' size="tiny" /> : "Update"}
                    </Button>
                </div>
            </div>
        </form>
    </>)
}

const MakePersonalInfoPageStyles = makeStyles({
    wrapper: {
        display: "flex",
        flexDirection: "column"
    },
    field: {
        display: "flex",
        flexDirection: "column",
        width: "100%"
    },
    dateControl: {
        width: "100%"
    },
    submitButton: {
        width: "100%"
    },
    divider: {
        margin: `${tokens.spacingHorizontalL} 0`
    },
    formFieldsWrapper: {
        display: 'flex',
        flexDirection: "column",
        maxWidth: "340px",
        margin: "0 auto",
        rowGap: tokens.spacingVerticalM
    },
    successAlert: {
        marginBottom: tokens.spacingVerticalM
    }
})
