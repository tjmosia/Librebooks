import { createContext, Dispatch, SetStateAction, useContext } from "react"
import { IUserLoginDto } from "../app/pages/account/LoginDto.type"
import { IAuthModelAlert } from "../app/pages/auth/auth.layout"

export const AuthContext = createContext<IAuthContext>({})

export interface IAuthContext {
    loading?: boolean
    username?: string
    setLoading?: Dispatch<SetStateAction<boolean>>
    setFormTitle?: Dispatch<SetStateAction<string>>
    setFormMessage?: Dispatch<SetStateAction<string>>
    setUsername?: (email: string) => void
    setWiderFormWrapper?: Dispatch<SetStateAction<boolean>>
    user?: IUserLoginDto
    setUser?: (user: IUserLoginDto | undefined) => void
    setAlert?: (alert: IAuthModelAlert | undefined) => void
}

export const useAuthContext = () => useContext(AuthContext)