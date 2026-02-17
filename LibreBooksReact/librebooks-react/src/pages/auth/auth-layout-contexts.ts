import {createContext} from "react";

export interface IAuthLayoutContext {
    setFormTitle: (title: string) => void;
    setFormMessage: (message: string) => void;
    setLoading: (loading: boolean) => void;
    isLoading: boolean;
}

export const AuthLayoutContext = createContext<IAuthLayoutContext>({
    setFormTitle: () => {},
    setFormMessage: () => {},
    setLoading: () => {},
    isLoading: false
})