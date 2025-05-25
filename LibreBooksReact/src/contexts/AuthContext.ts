import { createContext } from "react";
import { IClaim, IUser } from "../core/identity";

export interface IUserManagerContext{
    user: IUser | null | undefined
    roles: string[],
    claims: string[],
    signOut?: () => void,
    signIn?: (user: IUser) => void,
    addClaims?: (claims: IClaim[]) => void,
    addRoles?: (roles: string[]) => void
}

const UserManagerContext = createContext<IUserManagerContext>({
    user: null,
    roles: [],
    claims: []
})

export default UserManagerContext