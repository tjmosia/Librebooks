import useIdentityManager from "../../hooks/IdentityManager"

export interface IAuthorizeViewComponent {
    children: JSX.Element
}

export default function AuthorizedViewComponent({ children }: IAuthorizeViewComponent) {
    const identity = useIdentityManager()
    return identity.getUser() ? children : <></>
}