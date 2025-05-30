import { createContext } from "react";
import { IClaim, IUser } from "../core/identity";

export interface IUserData {
    email: string
    firstName: string
    lastName: string
    photo?: string | null
}

export interface IUserManagerContext{
    user?: IUserData | null,
    roles?: string[] | null,
    claims?: IClaim[] | null,
    addClaims: (claims: IClaim[]) => void,
    addRoles: (roles: string[]) => void,
    addUser: (user: IUser) => void,
    removeClaims: () => void,
    removeRoles: ()=> void,
    removeUser: ()=> void,
    clearUserData: () => void
}

export const UserManagerContext = createContext<IUserManagerContext | null>(null)
