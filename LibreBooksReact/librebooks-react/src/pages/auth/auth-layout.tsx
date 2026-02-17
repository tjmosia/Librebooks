import {Outlet} from "react-router";
import {useState} from "react";
import {AuthLayoutContext, type IAuthLayoutContext} from "./auth-layout-contexts.ts";

export function AuthLayout(){
    const [formTitle, setFormTitle] = useState<string>('');
    const [formMessage, setFormMessage] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(false);


    const context: IAuthLayoutContext = {
        setFormTitle: (title: string) => setFormTitle(title),
        setFormMessage: (message: string) => setFormMessage(message),
        isLoading: loading,
        setLoading: (loading: boolean) => setLoading(loading),
    }

    return <>
        <AuthLayoutContext.Provider value={context}>
            <div className="auth-layout">
                <div className="auth-layout-content">
                    <div className="authForm-container">
                        <div className="authForm__titleBar">
                            <h1>{formTitle}</h1>
                            <p>{formMessage}</p>
                        </div>
                        <div className="authFormwrapper">
                            <Outlet />
                        </div>
                    </div>
                </div>
            </div>
        </AuthLayoutContext.Provider>
    </>
}