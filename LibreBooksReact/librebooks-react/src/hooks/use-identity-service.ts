import { useContext } from "react"
import { IdentityContext } from "../contexts/identity-context"
import type { IClaim, IClaimsPrincipal, IRole, IUser } from "../core/identity"
import { ajax, type AjaxError } from "rxjs/ajax"
import { serverData } from "../strings"

type onSuccessFunc = (response: IClaimsPrincipal) => void
type onFailureFunc = (error: AjaxError) => void

export function useIdentityService() {
    const { claimsPrincipal, setClaimsPrincipal } = useContext(IdentityContext)

    function confirmLogin(onSuccessHandler?: onSuccessFunc, onFailureHandler?: onFailureFunc) {
        ajax<IClaimsPrincipal>({
            url: serverData.route("/auth/confirm-login"),
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            withCredentials: true
        }).subscribe({
            next(response) {
                if (response.status == 200) {
                    setClaimsPrincipal(response.response)
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
                    setClaimsPrincipal(undefined)
                }
            },
            error(error: AjaxError) {
                console.log(error)
            }
        })
    }

    return {
        isLoggedIn: () => !!claimsPrincipal,
        logout,
        login: (claimsPrincipal: IClaimsPrincipal) => setClaimsPrincipal(claimsPrincipal),
        confirmServerLogin: confirmLogin,
        getUser: () => claimsPrincipal?.user,
        getRoles: () => claimsPrincipal?.roles,
        getClaims: () => claimsPrincipal?.claims,
        isInRole: (role: IRole) => {
            claimsPrincipal?.roles.includes(role) ?? false
        },
        hasClaim: (claimType: string, claimValue?: string) => {
            if (claimsPrincipal?.claims) {
                return claimsPrincipal.claims.some(claim => {
                    if (claim.type === claimType) {
                        if (claimValue) {
                            return claim.value === claimValue
                        }
                        return true
                    }
                    return false
                })
            }
        },
        updateUser: (user: IUser) => {
            if (claimsPrincipal) {
                setClaimsPrincipal({
                    ...claimsPrincipal,
                    user: user
                })
            }
        },
        setClaims: (claims: IClaim[]) => {
            if (claimsPrincipal) {
                setClaimsPrincipal({
                    ...claimsPrincipal,
                    claims: claims
                })
            }
        },
        setRoles: (roles: IRole[]) => {
            if (claimsPrincipal) {
                setClaimsPrincipal({
                    ...claimsPrincipal,
                    roles: roles
                })
            }
        }
    }
}