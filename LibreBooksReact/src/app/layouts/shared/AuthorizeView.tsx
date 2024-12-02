export interface IAuthorizeViewProps {
    children: JSX.Element
    permissions: [string]
}

export default function AuthorizeView(props: IAuthorizeViewProps) {


    return props.children
}