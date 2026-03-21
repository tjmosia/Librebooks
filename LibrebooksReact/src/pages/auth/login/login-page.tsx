import {
    Button, Caption1, Field,
    Input, MessageBar,
    Spinner,
    Toolbar,
    ToolbarButton,
    ToolbarDivider,
} from "@fluentui/react-components";
import { type ChangeEvent, type SubmitEvent, useContext, useEffect, useState } from "react";
import { MdLock, MdLockOpen, MdPassword } from "react-icons/md";
import { useNavigate } from "react-router";
import { ajax, type AjaxError } from "rxjs/ajax";
import type { IFormField } from "../../../core/forms";
import type { ITransactionResult } from "../../../core/http";
import type { IClaimsPrincipal } from "../../../core/identity";
import { useIdentityService } from "../../../hooks/use-identity-service.ts";
import { serverData } from "../../../strings";
import { SessionData } from "../../../utils/session-data-utils.ts";
import { sendEmailVerificationCode } from "../auth-funcs.ts";
import { AuthLayoutContext } from "../auth-layout-contexts.ts";
import { AuthSessionVars } from "../auth-session-vars.ts";
import './login-page.css';

const initialPasswordField: IFormField<string> = {
    value: ""
}

export default function LoginPage() {
    const { loading, setLoading, setFormTitle, setFormMessage, user } = useContext(AuthLayoutContext)
    const [passwordField, setPasswordField] = useState(initialPasswordField)
    const [showPassword, setShowPassword] = useState<boolean>(false)
    const [rootError, setRootError] = useState<string>("")
    const { login } = useIdentityService()
    const navigate = useNavigate()

    useEffect(() => {
        setFormTitle(`Welcome back ${user?.firstName}`)
        setFormMessage('Login with your password.')
    }, [setFormTitle])

    function isValidPassword() {
        const _password = { ...passwordField }

        if (_password.errorMessage)
            return false

        if (!_password.value)
            _password.errorMessage = "Password is required."
        else if (_password.value!.length < 8)
            _password.errorMessage = "Please check your password."

        if (_password.errorMessage) {
            setPasswordField(_password)
            return false
        }

        return true
    }

    function onSubmit(e: SubmitEvent<HTMLFormElement>) {
        e.preventDefault()

        if (!isValidPassword())
            return

        setLoading(true)

        ajax<ITransactionResult<IClaimsPrincipal>>({
            method: "POST",
            url: serverData.route("/auth/login"),
            body: JSON.stringify({
                email: user?.email,
                password: passwordField.value,
            }),
            headers: {
                "Content-Type": "application/json",
            },
            withCredentials: true
        }).subscribe({
            next: (response) => {
                const data = response.response
                if (data.succeeded) {
                    login(data.model!)
                    SessionData.clear()
                    navigate("/app")
                } else {
                    for (const error of data.errors) {
                        if (error.code == "Password") {
                            setPasswordField(prev => {
                                return ({
                                    ...prev,
                                    errorMessage: error.description
                                });
                            })
                            break
                        }
                    }
                }
                setLoading(false)
            },
            error: (error: AjaxError) => {
                if (error.status === 0) {
                    setRootError("Network error. Please check your internet connection.")
                } else if (error.status === 400) {
                    const response = error.response as ITransactionResult
                    if (response.errors && response.errors!.length > 0)
                        for (const error of response.errors) {
                            if (error.code == "Email")
                                navigate("/auth")
                            else
                                setPasswordField(prev => ({
                                    ...prev,
                                    errorMessage: error.description
                                }))
                        }
                }

                setLoading(false)
            }
        })
    }

    function onPasswordInputChange(e: ChangeEvent<HTMLInputElement>) {
        setPasswordField({
            value: e.target.value.replace(" ", "")
        })
    }

    const returnToEntry = () => navigate("/auth")

    useEffect(() => {
        if (!user)
            returnToEntry()
        return () => {
            setLoading(false)
        }
    }, []);

    useEffect(() => {
        if (rootError)
            setTimeout(() => setRootError(""), 5000)
    }, [rootError]);

    function handleResetPasswordButtonClick() {
        sendEmailVerificationCode(user!.email!, undefined, undefined, "/auth/reset-password", () => {
            SessionData.addItem(AuthSessionVars.NextUrl, "/auth/reset-password")
            navigate("/auth/reset-password")
        })
    }

    return (<>
        {!user ? <div><Spinner /></div> :
            <form className="login-form animate__animated animate__fadeIn" onSubmit={onSubmit}>
                {
                    !rootError ? null : <div className="field animate__animated animate__fadeIn">
                        <MessageBar intent="error">
                            <Caption1>{rootError}</Caption1>
                        </MessageBar>
                    </div>
                }
                <div className="field">
                    <Field validationState={passwordField.errorMessage ? "error" : "none"}
                        size="medium"
                        validationMessage={passwordField.errorMessage}>
                        <Input onChange={onPasswordInputChange}
                            disabled={loading}
                            value={passwordField.value}
                            name="password"
                            aria-label="Password Input"
                            appearance="outline" required
                            aria-disabled={!loading}
                            contentBefore={<MdPassword size={18} />}
                            type={showPassword ? "text" : "password"}
                            contentAfter={<Button appearance="subtle"
                                onClick={() => setShowPassword(prev => !prev)}
                                size="small" icon={showPassword ? <MdLockOpen size={18} /> : <MdLock size={18} />} />}
                            placeholder="Password" />
                    </Field>
                </div>
                <div className="field">
                    <Button appearance="primary"
                        type="submit"
                        disabled={loading}
                        aria-disabled={loading}
                        className="submit-button" >Login</Button>
                </div>
                <div className="field redirect-buttons">
                    <Toolbar size="small" style={{ display: "flex", justifyContent: "space-between", width: "100%", padding: "0" }} >
                        <ToolbarButton type="button" onClick={returnToEntry}>Change Email</ToolbarButton>
                        <ToolbarDivider />
                        <ToolbarButton onClick={handleResetPasswordButtonClick} type="button">Reset Password</ToolbarButton>
                    </Toolbar>
                </div>
            </form>
        }
    </>)
}
