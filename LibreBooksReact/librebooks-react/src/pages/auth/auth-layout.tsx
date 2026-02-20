import { Outlet, useLocation } from "react-router";
import { useEffect, useState } from "react";
import { AuthLayoutContext, type IAuthLayoutContext, type IAuthRootMessage } from "./auth-layout-contexts.ts";
import './auth-layout.css'
import { Caption1, MessageBar, ProgressBar, Subtitle2 } from "@fluentui/react-components";
import type { IUser } from "../../core/identity";
import { AuthSessionVars } from "./auth-session-vars.ts";
import { SessionData } from "../../utils";
import { useIdentityService } from "../../hooks/use-identity-service.ts";

export function AuthLayout() {
    const [formTitle, setFormTitle] = useState<string>('');
    const [formMessage, setFormMessage] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(false);
    const [email, setEmail] = useState<string | undefined>(SessionData.getItem(AuthSessionVars.Email) ?? undefined);
    const [user, setUser] = useState<IUser | undefined>(SessionData.getItem(AuthSessionVars.User) ?? undefined);
    const { logout, isSignedIn } = useIdentityService()
    const [rootMessage, setRootMessage] = useState<IAuthRootMessage | undefined>();
    const location = useLocation()
    const context: IAuthLayoutContext = {
        setFormTitle: (title) => setFormTitle(title),
        setFormMessage: (message) => setFormMessage(message),
        loading: loading,
        setLoading: (loading) => setLoading(loading),
        user,
        email,
        setUser: (user) => setUser(user),
        setEmail: (email) => setEmail(email),
        setRootMessage: (message) => setRootMessage(message),
    }

    useEffect(() => {
        if (isSignedIn())
            logout()

        if (location.pathname === "/auth")
            SessionData.clear()

    }, [])

    useEffect(() => {
        if (rootMessage)
            setTimeout(() => {
                setRootMessage(undefined)
            }, 5000);
    }, [rootMessage]);

    function renderRootMessage() {
        if (!rootMessage)
            return null
        if (rootMessage.targetRoute && rootMessage.targetRoute !== location.pathname)
            return null

        return <div className="field">
            <MessageBar intent={rootMessage.intent} className="root-error-message">
                <Caption1>{rootMessage.message} </Caption1>
            </MessageBar>
        </div>
    }

    return <>
        <AuthLayoutContext.Provider value={context}>
            <div className="auth-layout animate__animated animate__fadeIn">
                <div className="authLayout__content">
                    <div className="authForm-container">
                        <div className="authForm__TitleBar">
                            <Subtitle2 className="authForm__title">{formTitle}</Subtitle2>
                            {formMessage ? <Caption1 className="authForm__message">{formMessage}</Caption1> : null}
                        </div>
                        {loading ? <ProgressBar /> : null}
                        <div className="authForm--wrapper">
                            {renderRootMessage()}
                            <Outlet />
                        </div>
                    </div>
                </div>
            </div>
        </AuthLayoutContext.Provider>
    </>
}