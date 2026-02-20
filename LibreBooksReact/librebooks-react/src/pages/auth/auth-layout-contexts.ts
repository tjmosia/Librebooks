import { createContext } from "react";
import type { IUser } from "../../core/identity";

export interface IAuthRootMessage {
    message?: string
    intent: "error" | "success" | "warning" | "info",
    targetRoute?: string
}

export interface IAuthLayoutContext {
    setFormTitle: (title: string) => void;
    setFormMessage: (message: string) => void;
    setLoading: (loading: boolean) => void;
    loading: boolean;
    email?: string;
    user?: IUser;
    setUser: (user: IUser | undefined) => void;
    setEmail: (email: string | undefined) => void;
    setRootMessage: (message?: IAuthRootMessage) => void;
}

export const AuthLayoutContext = createContext<IAuthLayoutContext>({
    setFormTitle: () => { },
    setFormMessage: () => { },
    setLoading: () => { },
    loading: false,
    setUser: () => { },
    setEmail: () => { },
    setRootMessage: () => { }
})
