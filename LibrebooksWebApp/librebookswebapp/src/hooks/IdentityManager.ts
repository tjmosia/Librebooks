import { IAppUser } from "../types/identity";
import { ITransactionResult } from "../core/extensions/TransactionTypes";
import { ajax, AjaxError } from "rxjs/ajax";
import { ApiRoutes, AppRoutes } from "../strings";
import { StatusCodes } from "http-status-codes";
import { useNavigate } from "react-router";
import useAppSettings from "./AppSettings";
import { getUser, removeUser, setUser } from '../reducers/IdentitySlice'
import { useAppDispatch } from "./Store";
import { useSelector } from "react-redux";

export default function useIdentityManager() {
    const { createApiPath } = useAppSettings()
    const navigate = useNavigate()
    const user = useSelector(getUser)
    const dispatch = useAppDispatch()

    function confirmSignIn() {
        ajax<ITransactionResult<IAppUser>>({
            url: createApiPath(ApiRoutes.Auth.ConfirmLogin),
            method: "POST",
            withCredentials: true
        }).subscribe({
            next: (response) => {
                dispatch(setUser(response.response.model!))
            },
            error: (error: AjaxError) => {
                if (error.status === StatusCodes.UNAUTHORIZED) {
                    console.log("USER NOT AUTHORIZED.")
                    dispatch(removeUser())
                    navigate(AppRoutes.Auth.Username)
                }
            }
        })
    }

    function updateUser(user: IAppUser) {
        dispatch(setUser(user))
    }

    function signIn(user: IAppUser) {
        updateUser(user)
    }

    function signOut() {
        if (!user)
            return navigate(AppRoutes.Auth.Username)

        ajax<ITransactionResult<IAppUser>>({
            url: createApiPath(ApiRoutes.Account.Logout),
            method: "POST",
            withCredentials: true
        }).subscribe({
            next: (response) => {
                if (response.status === StatusCodes.OK)
                    dispatch(removeUser())
            },
            error: () => {
                dispatch(removeUser())
                navigate(AppRoutes.Auth.Username)
            }
        })
    }

    return {
        updateUser,
        signIn,
        signOut,
        confirmSignIn,
        getUser: () => user
    }
}