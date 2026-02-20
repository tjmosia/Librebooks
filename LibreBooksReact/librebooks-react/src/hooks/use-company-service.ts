import {useContext} from "react";
import {CompanyContext} from "../contexts/company-context.ts";


export function useCompanyService() {
    const {company, setCompany, removeCompany} = useContext(CompanyContext);


    return {
        company: Object.freeze(company),
        setCompany,
        removeCompany,
    }
}