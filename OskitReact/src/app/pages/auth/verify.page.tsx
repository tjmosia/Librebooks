import { ChangeEvent, createRef, FormEvent, useEffect, useState } from 'react'
import { Input, Button, makeStyles, tokens, Field, ToastIntent, MessageBar, MessageBarBody, MessageBarTitle } from '@fluentui/react-components'
import { usePageTitle } from '../../../hooks/page.hook'
import AppRoutes from '../../../strings/AppRoutes'
import { useNavigate } from 'react-router-dom'
import { useAuthContext } from '../../../contexts/auth.context'
import { ITransactionResult } from '../../../core/transactions.core'
import { ISendVerificationCodeResponse } from './auth.types'
import { useHttp } from '../../../hooks/http.hook'
import { useAppSettings } from '../../../strings/AppSettings'
import useSessionData from '../../../extensions/SessionData'
import { AuthSessionVars, AuthVerificationReasons } from './auth.strings'
import { StatusCodes } from 'http-status-codes'
import { ajax, AjaxError } from 'rxjs/ajax'
import { ApiRoutes } from '../../../strings/ApiRoutes'

const VerifyEmailPageStyles = makeStyles({
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
    },
    otpInput: {
        letterSpacing: tokens.spacingHorizontalXXL,
        textAlign: "center",
    }
})

interface IModel {
    otp: string
    error?: string
}

export default function VerifyEmailPage() {
    usePageTitle("Login")
    const navigate = useNavigate()
    const styles = VerifyEmailPageStyles()
    const { setFormTitle, setFormMessage, loading, setLoading, username } = useAuthContext()
    const form = createRef<HTMLFormElement>()
    const { headers } = useHttp()
    const { createApiPath } = useAppSettings()
    const session = useSessionData()
    const verificationReason = session.get<string>(AuthSessionVars.VerificationReason)
    const verificationHashString = session.get<string>(AuthSessionVars.VerificationHashString)
    const [alert, setAlert] = useState<{ message?: string, intent?: ToastIntent } | undefined>(undefined)

    const [model, setModel] = useState<IModel>({
        otp: ""
    })
    const resetModel = () => setModel({ otp: "" })

    /********************************************************************
     * METHODS
     *******************************************************************/

    const continueToNext = () => {
        session.remove(AuthSessionVars.VerificationReason)
        if (!verificationReason)
            navigate(session.get<string>(AuthSessionVars.FromPath) ?? AppRoutes.Auth.Username)
        if (verificationReason === AuthVerificationReasons.Registration)
            navigate(AppRoutes.Auth.Register)
        if (verificationReason === AuthVerificationReasons.PasswordReset)
            navigate(AppRoutes.Auth.ResetPassword)
    }

    function formSubmitEventHandler(event: FormEvent) {
        event.preventDefault()

        if (model.otp.length !== 8)
            return

        setLoading!(true)

        ajax<ITransactionResult<null>>({
            url: createApiPath(ApiRoutes.Auth.Verify),
            method: "POST",
            headers: {
                [headers.contentType.name]: headers.contentType.values.Json
            },
            body: JSON.stringify({
                email: username,
                code: model.otp,
                codeHashString: verificationHashString!,
                reason: verificationReason
            })
        }).subscribe({
            next: (response) => {
                if (response.status === StatusCodes.OK) {
                    const data = response.response
                    if (data.succeeded) {
                        session.add(AuthSessionVars.VerificationCode, model.otp)
                        setLoading!(false)
                        continueToNext()
                    } else {
                        setModel({
                            otp: "",
                            error: "Invalid OTP provided."
                        })
                    }
                }
            },
            error: (error: AjaxError) => {
                console.log(error)
                setLoading!(false)
                setAlert({ message: "System error. Please try again or change your email.", intent: "error" })

            }
        })
    }

    function inputChangeEventHandler(event: ChangeEvent<HTMLInputElement>) {

        const newVal = event.currentTarget.value.replace(/[\s]/, "")

        if (newVal !== model.otp)
            setModel({
                otp: newVal
            })
    }

    function resendVerificationCode() {
        setLoading!(true)
        resetModel()
        ajax<ITransactionResult<ISendVerificationCodeResponse>>({
            url: createApiPath(ApiRoutes.Auth.SendVerificationCode),
            method: "POST",
            body: JSON.stringify({
                email: username,
                reason: AuthVerificationReasons.PasswordReset
            }),
            headers: {
                [headers.contentType.name]: headers.contentType.values.Json
            }
        }).subscribe({
            next: (response) => {
                console.log(response)
                const data = response.response

                if (data && data.succeeded) {
                    session.add(AuthSessionVars.VerificationHashString, data.model!.codeHashString)
                    setLoading!(false)

                    setAlert({
                        message: "Verification code successfully sent!",
                        intent: "success"
                    })
                }
            },
            error: (error: AjaxError) => {
                console.log(error)
                setAlert({
                    message: "Unable to sned verification code.",
                    intent: "error"
                })
                setLoading!(false)
            }
        })
    }

    /********************************************************************
     * EFFECTS
     *******************************************************************/
    useEffect(() => {
        setFormTitle!("Verify Your Email")
        setFormMessage!("We've sent a verification OTP to your email. Please enter the OTP below:")
    }, [setFormTitle, setFormMessage])

    useEffect(() => {
        if (model.otp.length == 8)
            form.current?.requestSubmit()
    }, [model.otp, form])

    useEffect(() => {
        if (alert)
            setTimeout(() => {
                setAlert(undefined)
            }, 3000)
    }, [alert])

    useEffect(() => {
        if (!verificationHashString || !verificationReason || !username)
            navigate(session.get<string>(AuthSessionVars.FromPath) ?? AppRoutes.Auth.Username)
    }, [verificationReason, verificationHashString, navigate, username, session])

    return (<>
        <div className={styles.wrapper}>
            {alert ? (
                <MessageBar key="formAlert" intent={alert.intent}>
                    <MessageBarBody>
                        <MessageBarTitle>{alert.message}</MessageBarTitle>
                    </MessageBarBody>
                </MessageBar>) : null}
            <form method="post"
                onSubmit={formSubmitEventHandler}
                ref={form}
                className="form">
                <Field className={styles.field}
                    aria-label="Verification OTP Input"
                    validationState={model.error ? "error" : "none"}
                    validationMessage={model.error}>
                    <Input as="input"
                        maxLength={8}
                        placeholder='--------'
                        autoComplete='off'
                        input={{
                            style: {
                                textAlign: "center",
                                letterSpacing: tokens.spacingHorizontalM
                            }
                        }}
                        className={styles.otpInput}
                        onChange={inputChangeEventHandler}
                        name="username"
                        size='large'
                        disabled={loading}
                        value={model.otp}
                        appearance="outline"
                        aria-label="Username input" />
                </Field>
                {/* <div className={styles.field}>
                    <Button appearance='primary' disabled={loading}>
                        {loading ? <Spinner size="tiny" /> : "Continue"}
                    </Button>
                </div> */}
                <div className={styles.links}>
                    <Button appearance='transparent'
                        aria-label='Forggotten password link'
                        onClick={resendVerificationCode}>
                        Resend Code
                    </Button>
                </div>
            </form>
        </div>
    </>)
}


