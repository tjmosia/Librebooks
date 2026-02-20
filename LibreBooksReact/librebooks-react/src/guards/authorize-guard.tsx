import type {JSXElement} from "@fluentui/react-components";

interface IAuthorizeGuardProps{
    children: JSXElement
}

export default function AuthorizeGuard({children}: IAuthorizeGuardProps){
    return(<div>
        {children}
    </div>)
}