import { useContext } from "react"
import { IdentityContext } from "../contexts/identity-context.ts"
import { ajax, AjaxError } from "rxjs/ajax"
import type { IUser } from "../core/identity/index.ts"
import { serverData } from "../strings/serverData.ts"

type onSuccessFunc = (response: IUser) => void
type onFailureFunc = (error: AjaxError) => void

export function useIdentityService() {
    const { setUser, getUser, removeUser } = useContext(IdentityContext)

    function confirmLogin(onSuccessHandler?: onSuccessFunc, onFailureHandler?: onFailureFunc) {
        ajax<IUser>({
            url: serverData.route("/auth/confirm-login"),
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            withCredentials: true
        }).subscribe({
            next(response) {
                if (response.status == 200) {
                    setUser(response.response)
                    console.log("User login confirmed:", response.response)
                    if (onSuccessHandler)
                        onSuccessHandler(response.response)
                }
            },
            error(error: AjaxError) {
                if (onFailureHandler)
                    onFailureHandler(error)
            }
        })
    }

    function logout() {
        ajax<IUser>({
            url: serverData.route("/auth/logout"),
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            withCredentials: true
        }).subscribe({
            next(response) {
                if (response.status == 200) {
                    removeUser()
                }
            },
            error(error: AjaxError) {
                console.log(error)
            }
        })
    }

    return {
        user: getUser(),
        isSignedIn: () => !!getUser(),
        login: setUser,
        logout,
        confirmLogin
    }
}