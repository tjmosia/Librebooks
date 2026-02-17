import type {IUser} from "../core/identity";
import {createContext} from "react";

interface IIdentityContext {
    getUser: () => IUser | undefined;
    setUser: (user: IUser) => void;
    removeUser: () => void;
}

const IdentityContext = createContext<IIdentityContext>({
    getUser: () => undefined,
    setUser: () => {},
    removeUser: () => {},
})



export {
    IdentityContext,
    type IIdentityContext
}