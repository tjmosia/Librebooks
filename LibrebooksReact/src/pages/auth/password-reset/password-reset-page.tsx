import { Button, Caption1, Caption2, Divider, Field, Input, MessageBar, MessageBarBody, Spinner, tokens, Toolbar, ToolbarDivider } from "@fluentui/react-components"
import { useContext, useEffect, useState, type ChangeEvent } from "react"
import { MdCancel, MdCheckBox } from "react-icons/md"
import { useLocation, useNavigate } from "react-router"
import { ajax, type AjaxError } from "rxjs/ajax"
import { FormValidators, type IFormField } from "../../../core/forms"
import { type ITransactionError, type ITransactionResult } from "../../../core/http"
import { serverData } from "../../../strings"
import { lowerFirstLetter, SessionData } from "../../../utils"
import { sendEmailVerificationCode } from "../auth-funcs"
import { AuthLayoutContext } from "../auth-layout-contexts"
import { AuthSessionVars } from "../auth-session-vars"
import './password-reset-page.css'

type setRootMessageType = (message: string, intent: "error" | "success" | "warning" | "info") => void

interface IPasswordResetModel {
    password: IFormField<string>,
    confirmPassword: IFormField<string>,
    code: IFormField<string>
}

const initialPasswordResetModel: IPasswordResetModel = {
    password: {
        value: "",
    },
    code: {
        value: "",
    },
    confirmPassword: {
        value: "",
    }
}

export default function PasswordResetPage() {
    const navigate = useNavigate()
    const location = useLocation()
    const [model, setModel] = useState<IPasswordResetModel>(initialPasswordResetModel)
    const { loading, setLoading, setFormTitle, setFormMessage, setRootMessage, user } = useContext(AuthLayoutContext)
    const [showPassword, setShowPassword] = useState(false)
    const [resendingCode, setResendingCode] = useState(false)
    const [activated] = useState(SessionData.getItem(AuthSessionVars.NextUrl) === location.pathname ? true : false)

    function onInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { name, value } = event?.target
        setModel(prev => ({
            ...prev,
            [name]: {
                value
            }
        }))
    }

    function comparePasswordsOnConfirmInputBlur() {
        if (model.confirmPassword.value && model.password.value) {
            if (model.confirmPassword.value !== model.password.value) {
                setModel(prev => ({
                    ...prev,
                    confirmPassword: {
                        ...prev.confirmPassword,
                        erred: true,
                        errorMessage: "Passwords do not match."
                    }
                }))
            }
        }
    }

    function onCodeChangeHandler(event: ChangeEvent<HTMLInputElement>) {
        const { value } = event.target
        const _value = value.replace(/\s/g, '')
        if (isNaN(Number(_value)))
            return

        setModel((prev) => ({
            ...prev,
            code: {
                value: _value
            }
        }))
    }

    function onSubmitHandler(e: React.SubmitEvent<HTMLFormElement>) {
        e.preventDefault()
        if (!validateModel(model, setModel, (message, intent) => setRootMessage({ message, intent })))
            return
        setLoading(true)
        ajax<ITransactionResult<null>>({
            url: serverData.route("/auth/reset-password "),
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                email: user?.email,
                reason: serverData.verificationReasons.passwordReset,
                password: model.password.value,
                code: model.code.value
            }),
            withCredentials: true
        }).subscribe({
            next(response) {
                setLoading(false)
                if (response.response.succeeded) {
                    setRootMessage({
                        message: "Password successfully reset. Please log in with your new password.",
                        intent: "success"
                    })
                    navigate("/auth/login")
                } else {
                    for (const error of response.response.errors) {
                        if (!error.code) {
                            setRootMessage({
                                message: "Failed to reset password. Please try again later.",
                                intent: "error"
                            })
                        }
                        if (error.code == "Email")
                            navigate("/auth")
                        else {
                            const key = lowerFirstLetter(error.code) as keyof IPasswordResetModel
                            setModel(prev => ({
                                ...prev,
                                [key]: {
                                    ...prev[key],
                                    erred: true,
                                    errorMessage: error.description
                                }
                            }))
                        }
                    }
                }
            },
            error(err: AjaxError) {
                setLoading(false)
                if (err.status === 0) {
                    setRootMessage({
                        message: "Network error. Please check your connection and try again.",
                        intent: "error"
                    })
                } else if (err.status === 400) {
                    const _model = { ...model }
                    var errors = err.response as ITransactionError[]
                    for (const error of errors) {
                        if (error.code == "Code")
                            _model.code = {
                                ..._model.code,
                                erred: true,
                                errorMessage: error.description
                            }
                    }
                    setModel(_model)
                    setRootMessage({
                        message: "Please check the form for errors and try again.",
                        intent: "error"
                    })

                } else {
                    setRootMessage({
                        message: "An error occurred while resetting the password. Please try again later.",
                        intent: "error"
                    })
                }
            },
        })
    }


    function returnToEntry() {
        return navigate("/auth")
    }

    useEffect(() => {
        if (user == undefined)
            returnToEntry()

        if (!activated)
            navigate("/auth/login")

        setFormTitle("Reset Your Password")
        setFormMessage("Create a new password for your account.")

        return () => {
            setLoading(false)
            SessionData.removeItem(AuthSessionVars.NextUrl)
        }
    }, [setFormTitle, setFormMessage])

    return (
        <form className="password-reset-form animate__animated animate__fadeIn" onSubmit={onSubmitHandler}>
            <div className="field">
                <Divider>Password</Divider>
            </div>
            {/* <div className="field">
                <MessageBar intent="info" icon={null} className="password-hint">
                    <Caption1>Password must be at least 8 characters long and include a mix of uppercase letters,
                        lowercase letters, numbers, and special characters.</Caption1>
                </MessageBar>
            </div> */}
            <div className="field">
                <Field label="Password" size="small"
                    validationState={model.password.erred ? "error" : "none"}
                    validationMessage={model.password.errorMessage}>
                    <Input name="password" disabled={loading}
                        value={model.password.value}
                        type={showPassword ? "text" : "password"}
                        placeholder="Your new Password"
                        required
                        onChange={onInputChange}
                        contentAfter={
                            <Button appearance="subtle" size="small" onClick={() => setShowPassword(!showPassword)}>
                                {showPassword ? "Hide" : "Show"}
                            </Button>
                        }
                    />
                </Field>
            </div>
            <div className="field">
                {RenderPasswordRequirementList(model.password.value ?? "")}
            </div>
            <div className="field">
                <Field label="Confirm Password" size="small"
                    validationState={model.confirmPassword.erred ? "error" : "none"}
                    validationMessage={model.confirmPassword.errorMessage}>
                    <Input name="confirmPassword" disabled={loading}
                        value={model.confirmPassword.value}
                        required
                        onBlur={comparePasswordsOnConfirmInputBlur}
                        type={showPassword ? "text" : "password"}
                        placeholder="Confirm your new Password"
                        onChange={onInputChange}
                    />
                </Field>
            </div>
            <div className="field">
                <Divider>Verification</Divider>
            </div>
            <div className="field">
                <Field label="Verification Code" size="small" required
                    validationMessage={model.code.errorMessage}
                    hint={<Caption2>NB: Enter the verification code sent to your mailbox.</Caption2>}
                    validationState={model.code.erred ? "error" : "none"}>
                    <Input disabled={loading} onChange={onCodeChangeHandler}
                        placeholder="Verification Code"
                        maxLength={6}
                        value={model.code.value}
                        contentAfter={<Button onClick={() => sendEmailVerificationCode(user?.email!, setRootMessage, setResendingCode)} appearance="subtle" size="small">
                            {resendingCode ? <Spinner size="extra-tiny" /> : "Request Code"}
                        </Button>}
                        name="code" />
                </Field>
            </div>
            <div className="field">
                <Button size="small" className="submit-button" type="submit" appearance="primary">Reset Password</Button>
            </div>
            <div className="field">
                <Toolbar size="small" style={{ display: "flex", justifyContent: "space-between", width: "100%", padding: "0" }} >
                    <Button size="small" type="button" onClick={returnToEntry} appearance="subtle">Change Email</Button>
                    <ToolbarDivider vertical />
                    <Button size="small" appearance="subtle" onClick={() => navigate("/auth/login")}>Back to Login</Button>
                </Toolbar>
            </div>
        </form>
    )
}


function validateModel(model: IPasswordResetModel, setModel: React.Dispatch<React.SetStateAction<IPasswordResetModel>>, setRootMessage: setRootMessageType): boolean {
    const _model = { ...model }
    if (!model.password.value)
        _model.password = {
            ...model.password,
            erred: true,
            errorMessage: "Password is required."
        }
    else if (!FormValidators.validatePassword(model.password.value)) {
        _model.password = {
            ..._model.password,
            erred: true,
            errorMessage: "Password does not meet the required criteria."
        }
    }


    if (!_model.confirmPassword.value)
        _model.confirmPassword = {
            ...model.confirmPassword,
            erred: true,
            errorMessage: "Please confirm your password."
        }

    if (!model.code.value)
        _model.code = {
            ...model.code,
            erred: true,
            errorMessage: "Verification code is required."
        }

    if (model.password.value && model.confirmPassword.value && model.password.value !== model.confirmPassword.value)
        _model.confirmPassword = {
            ..._model.confirmPassword,
            erred: true,
            errorMessage: "Passwords do not match."
        }

    if (_model.password.erred || _model.confirmPassword.erred || _model.code.erred) {
        setModel(_model)
        setRootMessage("Ensure all field errors are corrected.", "error")
        return false
    }

    return true
}

const RenderPasswordRequirementList = (password: string) => {
    const hasDigit = /[0-9]/.test(password)
    const hasUpper = /[A-Z]/.test(password)
    const hasLower = /[a-z]/.test(password)
    const hasSpecial = /[!@#$%^&*(),.?":{}|<>]/.test(password)
    const hasMinLength = /.{8,}/.test(password)

    function getColorName(passed: boolean) {
        if (password)
            return passed ? tokens.colorStatusSuccessForeground1 : tokens.colorStatusDangerForeground1
        else
            return "inherit"
    }

    return (
        <MessageBar icon={null} className="password-requirements">
            <MessageBarBody>
                <Divider suppressHydrationWarning className="password-requirements-divier">Password Requirements</Divider>
                <ul className="password-criteria-list">
                    <li><Caption1 style={{ color: getColorName(hasMinLength) }}>{hasMinLength ? <MdCheckBox size={16} /> : <MdCancel size={16} />} Contains 8 characters or more.</Caption1></li>
                    <li><Caption1 style={{ color: getColorName(hasUpper) }}>{hasUpper ? <MdCheckBox size={16} /> : <MdCancel size={16} />} Contains an uppercase letter.</Caption1></li>
                    <li><Caption1 style={{ color: getColorName(hasLower) }}>{hasLower ? <MdCheckBox size={16} /> : <MdCancel size={16} />} Contains a lowercase letter.</Caption1></li>
                    <li><Caption1 style={{ color: getColorName(hasDigit) }}>{hasDigit ? <MdCheckBox size={16} /> : <MdCancel size={16} />} Contains a number.</Caption1></li>
                    <li><Caption1 style={{ color: getColorName(hasSpecial) }}>{hasSpecial ? <MdCheckBox size={16} /> : <MdCancel size={16} />} Contains a special character.</Caption1></li>
                </ul>
            </MessageBarBody>
        </MessageBar>
    )
}
