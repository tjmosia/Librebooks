import { useCompanyManager } from "../../../hooks"


export interface IAuthorizedCompanyViewProps {
    children: JSX.Element
}

export default function AuthorizedCompanyView(props: IAuthorizedCompanyViewProps) {
    const companyManager = useCompanyManager()
    const company = companyManager.getCompany()
    return !company ? props.children : ""
}