import { createContext } from "react"

export interface ICompanyData{
        id: string
        name: string
        logo: string
}

export interface ICompanyManagerContext {
    company: ICompanyData | null,
    addCompany: (company: ICompanyData) => void,
    removeCompany: () => void
}

export const CompanyManagerContext = createContext<ICompanyManagerContext | null>(null)
