import useIdentityManager from "../../hooks/IdentityManager"

export interface IAuthorizeView {
    children: JSX.Element
}

export default function AuthorizedView({ children }: IAuthorizeView) {
    const identity = useIdentityManager()
    return identity.getUser() ? children : <></>
}