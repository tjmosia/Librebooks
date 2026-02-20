import { Outlet } from "react-router"
import { NavBarComponent } from "./components/navbar/navbar-component"
import type {ICompany} from "../../core/companies"
import {useEffect, useState} from "react"
import {CompanyContext, type ICompanyContext} from "../../contexts/company-context.ts"

export function AppLayout() {
    const [company, setCompany] = useState<ICompany | undefined >()



    const context: ICompanyContext  = {
        company: company,
        setCompany: (company) => setCompany(company),
        removeCompany:() => setCompany(undefined)
    }



    useEffect(() => {

    })

    return (
        <CompanyContext value={context}>
            <div className="app-layout">
                <NavBarComponent />
                <Outlet />
            </div>
        </CompanyContext>
    )
}