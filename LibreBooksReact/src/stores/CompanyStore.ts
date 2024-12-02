import { create } from "zustand"
import ICompany from "../types/ICompany"


export interface ICompanyStore {
    company?: ICompany
    setCompany: (company: ICompany) => void
    removeCompany: () => void
}

const useCompanyStore = create<ICompanyStore>(set => ({
    company: undefined,
    setCompany: (company: ICompany) => {
        set(({ company }))
    },
    removeCompany: () => {

    }
}))


export default useCompanyStore