import { createContext } from "react"

export interface IAuthContext {
    setTitle?: (title: string) => void,
    setSubTitle?: (subTitle: string) => void
    loading: boolean
    setLoading: (loading: boolean) => void
}

export const AuthContext = createContext<IAuthContext>({
    loading: false,
    setLoading: () => { }
})
