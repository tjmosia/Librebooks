import { createContext, Dispatch, SetStateAction, useContext } from "react"

export const AuthContext = createContext<IAuthContext>({})

export interface IAuthContext {
    loading?: boolean
    username?: string
    givenName?: string
    setGivenName?: Dispatch<SetStateAction<string>>
    returnUrl?: string | null
    setLoading?: Dispatch<SetStateAction<boolean>>
    setFormTitle?: Dispatch<SetStateAction<string>>
    setFormMessage?: Dispatch<SetStateAction<string>>
    setUsername?: (email: string) => void
    sendEmailVerificationCode?: () => void,
    setWiderFormWrapper?: Dispatch<SetStateAction<boolean>>
}

export const useAuthContext = () => useContext(AuthContext)