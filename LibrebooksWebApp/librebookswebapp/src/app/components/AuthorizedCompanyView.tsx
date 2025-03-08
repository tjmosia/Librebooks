import { useCompanyManager } from "../../hooks"


export interface IAuthorizedCompanyViewComponentProps {
    children: JSX.Element
}

export default function AuthorizedCompanyViewComponent(props: IAuthorizedCompanyViewComponentProps) {
    const companyManager = useCompanyManager()
    const company = companyManager.getCompany()
    return !company ? props.children : ""
}