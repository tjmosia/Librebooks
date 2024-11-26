import { ChangeEvent, FormEvent, useContext, useEffect, useState } from 'react'
import { Input, Button, Spinner, makeStyles, tokens, Field, MessageBar, MessageBarBody } from '@fluentui/react-components'
import { AuthContext } from '../../../contexts/auth.context'
import { BsEnvelope } from 'react-icons/bs'
import { useNavigate } from 'react-router'
import AppRoutes from '../../../strings/AppRoutes'
import useSessionData from '../../../extensions/SessionData'
import StatusCodes from 'http-status-codes'
import { AuthSessionVars, AuthVerificationReasons } from './AuthStrings'
import { ITransactionResult } from '../../../core/Transactions'
import { ISendVerificationCodeResponse } from './AuthTypes'
import { useSearchParams } from 'react-router-dom'
import { ajax, AjaxError } from 'rxjs/ajax'
import { IUserLoginDto } from '../account/LoginDto.type'
import { useAppSettings, useHttp, usePageTitle, useValidators } from '../../../hooks'
import { ApiRoutes } from '../../../strings'

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


export default function UsernamePage() {
    usePageTitle!("Sign in or Sign up")
    const styles = LoginStyles()
    const search = useSearchParams()
    const navigate = useNavigate()
    const { setFormTitle, loading, setLoading, username, setFormMessage, setUsername, setUser } = useContext(AuthContext)
    const session = useSessionData()
    const { createApiPath } = useAppSettings()
    const [error, setError] = useState<string | undefined>()
    const [model, setModel] = useState<IModel>({
        email: username ?? ""
    })
    const { headers } = useHttp()
    const { emailvalidator } = useValidators()

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
                console.log(response)
                const data = response.response
                if (data && data.succeeded) {
                    console.log(data.model!.codeHashString)
                    session.add(AuthSessionVars.VerificationHashString, data.model!.codeHashString)
                    session.add(AuthSessionVars.VerificationReason, AuthVerificationReasons.Registration)
                    session.add(AuthSessionVars.FromPath, AppRoutes.Auth.Username)
                    setUsername!(model.email)
                    setLoading!(false)
                    navigate(AppRoutes.Auth.VerifyEmail)
                }
            },
            error: (error: AjaxError) => {
                console.log(error)
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

    function FormSubmitHandler(event: FormEvent) {
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
                    setLoading!(false)
                    navigate(AppRoutes.Auth.Login)
                    return
                }

                if (response.status === StatusCodes.NO_CONTENT)
                    sendRegistrationVerificationCode()

                setLoading!(false)
            },
            error: (error: AjaxError) => {
                console.log(error)
                if (error.status === StatusCodes.BAD_REQUEST)
                    setModelError("Pleace check your email.")
                setLoading!(false)
            }
        })
    }

    function modelValid() {
        if (!model.email)
            return false
        if (!emailvalidator.validate(model.email)) {
            setModel(model => ({
                email: model.email,
                error: "Please check your email."
            }))
            return false
        }
        return true
    }

    function InputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
        const { value } = event.target
        console.log(value)
        setModel({
            email: value.replace(/[\s]/, ""),
        })
    }

    useEffect(() => {
        setFormTitle!("Sign in or Sign up")
        session.remove(AuthSessionVars.Username)
        setFormMessage!("Continue with your email address.")

        const returnUrl = search[0].get("returnUrl");
        if (returnUrl)
            session.add(AuthSessionVars.ReturnUrl, returnUrl)

    }, [setFormTitle, session, setFormMessage, search])

    useEffect(() => {
        if (error) setTimeout(() => setError(undefined), 5000)
    }, [error])

    return (<>
        <form onSubmit={FormSubmitHandler}
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
                <Input onChange={InputChangeHandler}
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


