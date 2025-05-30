import { useContext } from "react";
import { CompanyManagerContext } from "../contexts";


export default function useCompanyManager(){
    const companyContext = useContext(CompanyManagerContext)!

    return {
        getCompany: () => companyContext.company,
        addCompany: companyContext.addCompany,
        removeCompany: companyContext.removeCompany
    }
}