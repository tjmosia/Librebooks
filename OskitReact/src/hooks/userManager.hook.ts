import { useDispatch, useSelector } from "react-redux";
import { getIdentity, getUser, signIn, signOut } from "../slices/identity.slice";
import { IAppUser } from "../types/identity";
import { ajax } from "rxjs/ajax";
import { useAppSettings } from "./app-settings.hook";
import { useHttp } from "./http.hook";
import { ITransactionResult } from "../core/transactions.core";
import useSessionData from "../extensions/SessionData";
import { SessionVariables } from "../strings/SessionVars";

export function useIdentityManager() {
    const { createApiPath } = useAppSettings()
    const { headers } = useHttp()
    const dispatch = useDispatch()
    const identity = useSelector(getIdentity)
    const user = useSelector(getUser)
    const session = useSessionData()

    function confirmSignIn() {
        if (!identity.isAuthenticated)
            return false

        ajax<ITransactionResult<IAppUser>>({
            url: createApiPath("/auth/confirm-login"),
            headers: [
                [headers.Auth.name] = `Bearer ${identity.user?.accessToken}`
            ],
            method: "POST"
        }).subscribe({
            next: (response) => {
                console.log(response)
            }
        })
    }

    return {
        isSignedIn: () => identity.isAuthenticated,
        signIn(user: IAppUser) {
            dispatch(signIn(user))
            session.add(SessionVariables.IdentityUser, user)
        },
        signOut() {
            dispatch(signOut)
            session.remove(SessionVariables.IdentityUser)
        },
        confirmSignIn: confirmSignIn,
        getUser: () => user
    }
}