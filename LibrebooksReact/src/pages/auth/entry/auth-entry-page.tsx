import { Button, Field, Input, Spinner } from "@fluentui/react-components";
import type { IFormField } from "../../../core/forms";
import { type ChangeEvent, useState, type SubmitEvent, useEffect, useContext } from "react";
import { AuthLayoutContext } from "../auth-layout-contexts.ts";
import { MdEmail } from "react-icons/md";
import { serverData } from "../../../strings/serverData.ts";
import { ajax } from "rxjs/ajax";
import type { IUser } from "../../../core/identity";
import { SessionData } from "../../../utils";
import { AuthSessionVars } from "../auth-session-vars.ts";
import { useNavigate } from "react-router";

const initialModel: IFormField<string> = {
    value: ""
}

export function AuthEntryPage() {
    const { setLoading, setFormTitle, loading, setFormMessage, setEmail, setUser } = useContext(AuthLayoutContext)
    const [emailModel, setEmailModel] = useState(initialModel)
    const navigate = useNavigate()

    function onEmailInputChange(e: ChangeEvent<HTMLInputElement>) {
        setEmailModel({
            value: e.target.value.toLowerCase(),
        });
    }

    function validateEmail() {
        const _email = { ...emailModel }

        if (_email.errorMessage)
            return false;

        if (!emailModel.value)
            _email.errorMessage = "Email is required";

        if (_email.errorMessage) {
            setEmailModel(_email)
            return false
        }

        return true
    }

    async function onSubmit(e: SubmitEvent<HTMLFormElement>) {
        e.preventDefault();
        if (!validateEmail())
            return

        setLoading(true);

        ajax<IUser>({
            method: "POST",
            url: serverData.route("/auth"),
            body: JSON.stringify({ email: emailModel.value }),
            headers: { "Content-Type": "application/json" },
        }).subscribe({
            next: (response) => {
                if (response.status === 200) {
                    const data = response.response
                    setUser(data)
                    SessionData.addItem(AuthSessionVars.User, data)
                    SessionData.addItem(AuthSessionVars.Email, emailModel.value!)
                    setEmail(emailModel.value)
                    navigate("/auth/login")
                    return
                }
            },
            error: (error) => {
                if (error.status === 400) {
                    setEmailModel(prev => ({
                        ...prev,
                        errorMessage: "Please check your email."
                    }))
                }
                if (error.status === 404) {
                    setEmail(emailModel.value)
                    SessionData.addItem(AuthSessionVars.Email, emailModel.value!)
                    navigate("/auth/register")
                }
                setLoading(false);
            }
        })
    }
    useEffect(() => {
        setFormTitle("Sign in or Sign Up")
        setFormMessage("Continue with your emailModel address.")
        return () => {
            setLoading(false)
        }
    }, [setFormTitle, setLoading])

    useEffect(() => {
        setUser(undefined)
        setEmail(undefined)
        SessionData.removeItem(AuthSessionVars.User)
        SessionData.removeItem(AuthSessionVars.Email)
    }, []);

    return (<>
        <form onSubmit={onSubmit} className="fields authForm animate__animated animate__fadeIn">
            <div className="field">
                <Field
                    validationState={emailModel.errorMessage ? "error" : "none"}
                    size="medium"
                    validationMessage={emailModel.errorMessage}>
                    <Input onChange={onEmailInputChange}
                        placeholder="Email Address"
                        disabled={loading}
                        aria-label="Email Address Input"
                        appearance="outline"
                        required
                        type="email"
                        contentBefore={<MdEmail size={18} />}
                        value={emailModel.value} />
                </Field>
            </div>
            <div className="field">
                <Button disabled={loading}
                    size="medium"
                    className="submit-button"
                    appearance="primary"
                    type="submit">
                    {loading ? <Spinner size="tiny" /> : "Continue"}
                </Button>
            </div>
        </form>
    </>)
}