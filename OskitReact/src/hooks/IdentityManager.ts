import { useSelector } from "react-redux";
import { getUser, removeUser, setUser } from "../slices/IdentitySlice";
import { IAppUser } from "../types/identity";
import useAppSettings from "./AppSettings";
import { ITransactionResult } from "../core/Transactions";
import { ajax } from "rxjs/ajax";
import { ApiRoutes, AppRoutes } from "../strings";
import { useAppDispatch } from "./Store";
import { StatusCodes } from "http-status-codes";
import { useLocation, useNavigate } from "react-router";

export default function useIdentityManager() {
    const { createApiPath } = useAppSettings()
    const dispatch = useAppDispatch()
    const user = useSelector(getUser)
    const navigate = useNavigate()
    const location = useLocation()

    function confirmSignIn() {
        ajax<ITransactionResult<IAppUser>>({
            url: createApiPath(ApiRoutes.Auth.ConfirmLogin),
            method: "POST",
            withCredentials: true,
            crossDomain: true
        }).subscribe({
            next: (response) => {
                if (response.status === StatusCodes.OK) {
                    dispatch(setUser(response.response.model!))
                    console.log("User Successfully Authenticated .")
                }
            },
            error: () => {
                navigate({
                    pathname: AppRoutes.Auth.Login,
                    search: `returnUrl=${location.pathname + location.search}`
                })
            }
        })
    }

    async function signOut() {
        ajax<ITransactionResult<IAppUser>>({
            url: createApiPath(ApiRoutes.Account.Logout),
            method: "POST",
            withCredentials: true,
            crossDomain: true
        }).subscribe({
            next: (response) => {
                if (response.status === StatusCodes.OK) {
                    dispatch(removeUser())
                    console.log("User Successfully Logged Out.")
                }
            },
            error: () => {
                navigate(AppRoutes.Auth.Login)
            }
        })
    }

    return {
        signIn(user: IAppUser) {
            dispatch(setUser(user))
        },
        signOut,
        confirmSignIn,
        getUser: () => user
    }
}