import type {JSXElement} from "@fluentui/react-components";

interface IAuthorizeGuardProps{
    children: JSXElement
    roles?: string[],
    claims?: string[]
}

export default function AuthorizeViewGuard({children}: IAuthorizeGuardProps){
    //const [authorized, setAuthorized] = useState<boolean | undefined>();

    return(<div>
        {children}
    </div>)
}