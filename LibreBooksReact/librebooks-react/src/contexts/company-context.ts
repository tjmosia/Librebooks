import type {ICompany} from "../core/companies";
import {createContext} from "react";

export interface ICompanyContext {
    company?: ICompany;
    setCompany: (company: ICompany) => void;
    removeCompany: () => void;
}

export const CompanyContext = createContext<ICompanyContext>({
    setCompany: () => {},
    removeCompany: () => {},
})


