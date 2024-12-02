import { ChangeEvent, FormEvent, useContext, useEffect, useState } from 'react'
import { Input, Button, Spinner, makeStyles, tokens, Field, MessageBar, MessageBarBody } from '@fluentui/react-components'
import { AuthContext } from '../../../contexts/AuthContext'
import { BsEnvelope } from 'react-icons/bs'
import { useNavigate } from 'react-router'
import AppRoutes from '../../../strings/AppRoutes'
import useSessionData from '../../../core/extensions/SessionData'
import StatusCodes from 'http-status-codes'
import { AuthSessionVars, AuthVerificationReasons } from './AuthStrings'
import { ITransactionResult } from '../../../core/extensions/TransactionTypes'
import { ISendVerificationCodeResponse } from './AuthTypes'
import { useSearchParams } from 'react-router-dom'
import { ajax, AjaxError } from 'rxjs/ajax'
import { useAppSettings, useHttp, usePageTitle } from '../../../hooks'
import { ApiRoutes } from '../../../strings'
import { useValidators } from '../../../core/extensions'
import { useFormUtils } from '../../../core/forms'

const LoginStyles = makeStyles({
    wrapper: {
        display: "flex",
        flexDirection: "column"
    },
    field: {
        display: "flex",
        flexDirection: "column",
        margin: `${tokens.spacingVerticalL} 0`
    },
    links: {
        display: "flex",
        flexDirection: "row",
        justifyContent: "center",
        margin: `${tokens.spacingVerticalL} 0`
    }
})

interface IModel {
    email: string
    error?: string
}

interface IUserLoginDto {
    firstName: string
    lastName: string
    accessToken: string
    username: string
    photo: string
}


export default function UsernamePage() {

    /*********************************************************************************************************************************
     * SERVICES
     *********************************************************************************************************************************/
    usePageTitle!("Sign in or Sign up")
    const styles = LoginStyles()
    const search = useSearchParams()
    const navigate = useNavigate()
    const { setFormTitle, loading, setLoading, username, setFormMessage, setUsername, setUser } = useContext(AuthContext)
    const session = useSessionData()
    const { createApiPath } = useAppSettings()
    const { headers } = useHttp()
    const { emailvalidator } = useValidators()
    const { fieldErrors } = useFormUtils()

    /*********************************************************************************************************************************
     * STATE
     *********************************************************************************************************************************/
    const [error, setError] = useState<string | undefined>()
    const [model, setModel] = useState<IModel>({
        email: username ?? ""
    })

    /*********************************************************************************************************************************
     * METHODS
     *********************************************************************************************************************************/
    function sendRegistrationVerificationCode() {
        ajax<ITransactionResult<ISendVerificationCodeResponse>>({
            url: createApiPath(ApiRoutes.Auth.SendVerificationCode),
            method: "POST",
            body: JSON.stringify({
                email: model.email,
                reason: AuthVerificationReasons.Registration
            }),
            headers: {
                [headers.contentType.name]: headers.contentType.values.Json
            }
        }).subscribe({
            next: (response) => {
                const data = response.response

                if (data.succeeded) {
                    session.add(AuthSessionVars.VerificationHashString, data.model!.codeHashString)
                    session.add(AuthSessionVars.VerificationReason, AuthVerificationReasons.Registration)
                    session.add(AuthSessionVars.FromPath, AppRoutes.Auth.Username)
                    setUsername!(model.email)
                    setLoading!(false)
                    navigate(AppRoutes.Auth.VerifyEmail)
                }
            },
            error: (error: AjaxError) => {
                if (error.status !== StatusCodes.BAD_REQUEST)
                    setError("Unable to send verification code.")

                setModelError("Pleace check your email.")
                setLoading!(false)
            }
        })
    }

    function setModelError(errorMessage: string) {
        setModel(model => ({
            ...model,
            error: errorMessage
        }))
    }

    function onSubmit(event: FormEvent) {
        event.preventDefault()
        setLoading!(true)

        if (!modelValid()) {
            setLoading!(false)
            return
        }

        ajax<IUserLoginDto>({
            url: createApiPath(ApiRoutes.Auth.Username),
            method: "POST",
            body: JSON.stringify({ email: model.email }),
            headers: {
                [headers.contentType.name]: headers.contentType.values.Json
            }
        }).subscribe({
            next: (response) => {

                if (response.status === StatusCodes.OK) {
                    const data = response.response
                    setUsername!(model.email)
                    setUser!(data)
                    navigate(AppRoutes.Auth.Login)
                } else if (response.status === StatusCodes.NO_CONTENT) {
                    sendRegistrationVerificationCode()
                }

                setLoading!(false)
            },
            error: (error: AjaxError) => {
                if (error.status === StatusCodes.BAD_REQUEST)
                    setModelError("Pleace check your email.")
                setLoading!(false)
            }
        })
    }

    function modelValid() {
        if (!model.email) {
            setModel({
                email: "",
                error: fieldErrors.required("Email")
            })
            return false
        } else if (!emailvalidator.validate(model.email)) {
            setModel(model => ({
                email: model.email,
                error: "Please check your email."
            }))
            return false
        }

        return true
    }

    function onInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { value } = event.target
        setModel({
            email: value.replace(/\s+/g, ""),
        })
    }

    /*********************************************************************************************************************************
     * EFFECTS
     *********************************************************************************************************************************/
    useEffect(() => {
        setFormTitle!("Sign in or Sign up")
        session.remove(AuthSessionVars.Username)
        setFormMessage!("Continue with your email address.")

        const returnUrl = search[0].get("returnUrl");

        if (returnUrl)
            session.add(AuthSessionVars.ReturnUrl, search[0].get("returnUrl") ?? "/")

    }, [setFormTitle, session, setFormMessage, search])

    useEffect(() => {
        if (error)
            setTimeout(() => setError(undefined), 5000)
    }, [error])

    return (<>
        <form onSubmit={onSubmit}
            method="post"
            className="form">
            {error ?
                <MessageBar intent='error'>
                    <MessageBarBody>{error}</MessageBarBody>
                </MessageBar>
                : ""}
            <Field label="Username"
                validationMessage={model.error}
                validationState={model.error ? "error" : "none"}>
                <Input onChange={onInputChange}
                    contentBefore={<BsEnvelope size={16} />}
                    disabled={loading}
                    value={model.email}
                    appearance="outline"
                    aria-label="Username input" />
            </Field>
            <div className={styles.field}>
                <Button type='submit'
                    appearance='primary'
                    size='large'
                    disabled={loading}>
                    {loading ? <Spinner size="tiny" /> : "Continue"}
                </Button>
            </div>
        </form>
    </>)
}


