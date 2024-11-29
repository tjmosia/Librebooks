import { ChangeEvent, FormEvent, useEffect, useState } from 'react'
import { Button, Spinner, makeStyles, tokens, Field } from '@fluentui/react-components'
import { useAuthContext } from '../../../contexts/AuthContext'
import CreatePasswordComponent from './components/CreatePasswordComponent'
import useSessionData from '../../../extensions/SessionData'
import { AuthSessionVars } from './AuthStrings'
import { IAppUser } from '../../../types/identity'
import { useNavigate } from 'react-router'
import AppRoutes from '../../../strings/AppRoutes'
import { ajax, AjaxError } from 'rxjs/ajax'
import { ITransactionResult } from '../../../core/Transactions'
import { StatusCodes } from 'http-status-codes'
import { from } from 'rxjs'
import { Intent } from '../../../strings/ui/Intent'
import { useAppSettings, useHttp, usePageTitle, useValidators } from '../../../hooks'
import { ApiRoutes } from '../../../strings'

const ResetPasswordPageStyles = makeStyles({
    field: {
        display: "flex",
        flexDirection: "column",
        marginTop: `${tokens.spacingVerticalM}`
    },
})

interface IModel {
    [key: string]: { value: string }
    password: {
        value: string
        error?: string
    },
    confirmPassword: {
        value: string
        error?: string
    }
}

export default function ResetPasswordPage() {
    usePageTitle("Reset Password")
    const styles = ResetPasswordPageStyles()
    const session = useSessionData()
    const navigate = useNavigate()
    const user = session.get<IAppUser>(AuthSessionVars.User)
    const verificationCode = session.get<string>(AuthSessionVars.VerificationCode)
    const verificationHashString = session.get<string>(AuthSessionVars.VerificationHashString)
    const { createApiPath } = useAppSettings()
    const { headers } = useHttp()
    const { setFormTitle, setFormMessage, username, loading, setLoading, setAlert } = useAuthContext()
    const { passwordValidator } = useValidators()

    /*********************************************************************************************************************************
     * STATE
     *********************************************************************************************************************************/
    const [model, setModel] = useState<IModel>({
        password: {
            value: ""
        },
        confirmPassword: {
            value: ""
        },
    })


    /*********************************************************************************************************************************
     * METHODS
     *********************************************************************************************************************************/
    function InputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
        const { value, name } = event.currentTarget

        const newVal = value.replace(/\s+/g, "")
        console.log(event.currentTarget.value)

        if (newVal != model[name].value) {
            setModel(state => {
                const newModel = {
                    ...state,
                    [name]: {
                        value: newVal
                    }
                }
                if (name == "password" && model.confirmPassword.value)
                    newModel.confirmPassword = { value: "" }

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
    }

    function handleFormSubmitEvent(event: FormEvent) {
        event.preventDefault()
        setLoading!(true)

        if (!validateModel()) {
            setLoading!(false)
            return
        }

        ajax<ITransactionResult<null>>({
            url: createApiPath(ApiRoutes.Auth.PasswordReset),
            method: "POST",
            body: JSON.stringify({
                email: username,
                code: verificationCode,
                codeHashString: verificationHashString,
                password: model.password.value
            }),
            headers: {
                [headers.contentType.name]: headers.contentType.values.Json
            }
        }).subscribe({
            next: resp => {
                console.log(resp)
                if (resp.status === StatusCodes.OK) {
                    const result = resp.response
                    if (result.succeeded) {
                        setAlert!({
                            intent: Intent.success,
                            message: "Password was successfully reset."
                        })
                        setLoading!(false)
                        returnToLogin()
                    } else if (result.errors.length > 0) {
                        from(result.errors).subscribe(item => {
                            if (item.code === "Model")
                                setAlert!({
                                    intent: Intent.error,
                                    message: "Password matches your current password."
                                })
                            if (item.code === "Password")
                                setModel(prevState => ({
                                    ...prevState,
                                    password: {
                                        value: model.password.value,
                                        error: item.description ?? undefined
                                    }
                                }))
                        })

                        setLoading!(false)
                    }
                } else if (resp.status === StatusCodes.NO_CONTENT) {
                    setAlert!({
                        intent: Intent.error,
                        message: "Unregistered email. Please try again."
                    })
                    navigate(AppRoutes.Auth.Username)
                }
            },
            error: (error: AjaxError) => {
                console.log(error.response)
                // if (error.status === StatusCodes.BAD_REQUEST) {
                //     error.response
                // }
                setLoading!(false)
            }
        })

    }

    function validateModel() {
        let valid = true
        const newModel = { ...model }

        if (model.password.value) {
            if (!passwordValidator.validate(model.password.value)) {
                newModel.password.error = "Password doesn't meet requirements."
                valid = false
            } else {
                if (!model.confirmPassword.value) {
                    newModel.confirmPassword.error = "Confirm password is required."
                    valid = false
                }
                else if (model.confirmPassword.value !== model.password.value) {
                    newModel.confirmPassword.error = "Passwords do not match."
                    valid = false
                }
            }
        }
        else {
            newModel.password.error = "Password is required."
        }

        setModel(newModel)
        return valid
    }

    function clearVerificationSessionData() {
        session.remove(AuthSessionVars.VerificationCode)
        session.remove(AuthSessionVars.VerificationHashString)
    }

    function returnToLogin() {
        clearVerificationSessionData()
        navigate(AppRoutes.Auth.Login)
    }

    /*********************************************************************************************************************************
     * EFFECTS
     *********************************************************************************************************************************/

    useEffect(() => {
        setFormTitle!("Reset Password")
        setFormMessage!(`${user?.firstName}, create a your new password. Make sure to create a strong password that is hard to guess.`)
    }, [setFormTitle, setFormMessage, user])

    useEffect(() => {
        if (!user || !verificationCode || !verificationHashString)
            navigate(AppRoutes.Auth.Username)
    }, [navigate, user, verificationCode, verificationHashString])


    return (<>
        <form onSubmit={handleFormSubmitEvent}
            method="post"
            className="form">
            <CreatePasswordComponent
                inputChangeHandler={InputChangeHandler}
                password={model.password}
                confirmPassword={model.confirmPassword} />
            <Field className={styles.field}>
                <Button
                    title='Reset password and return to login'
                    size='large'
                    appearance='primary'
                    type='submit'
                    disabled={loading}>
                    {loading ? <Spinner size="tiny" /> : "Reset and Continue"}
                </Button>
            </Field>
            <Field className={styles.field}>
                <Button type='button' appearance='transparent' onClick={() => returnToLogin()}>Discard & Return to Login</Button>
            </Field>
        </form>
    </>)
}


