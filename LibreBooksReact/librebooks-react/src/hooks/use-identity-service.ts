import {useContext} from "react";
import {IdentityContext} from "../contexts/identity-context.ts";

export function useIdentityService() {
    const {setUser, getUser, removeUser} = useContext(IdentityContext)

    return {
        user: getUser(),
        isSignedIn: () => !!getUser(),
        login: setUser,
        logout: removeUser,
    }
}