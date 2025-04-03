import { IIdentityState, useAppDispatch, useAppSelector } from "../stores/identityStore";
import { identityActions } from '../reducers/identitySlice'
import IUser from "../core/identity";

export default function useIdentityManager() {
    const { login, logout } = identityActions
    const state = useAppSelector((state: IIdentityState) => state.identity)
    const dispatch = useAppDispatch()

    return {
        isAuthenticated: state.user !== null,
        getUser: () => state.user,
        signIn(user: IUser) {
            dispatch(login(user))
        },
        signOut: () => dispatch(logout()),
        updateUser(user: IUser) {
            dispatch(login(user))
        }
    }
}