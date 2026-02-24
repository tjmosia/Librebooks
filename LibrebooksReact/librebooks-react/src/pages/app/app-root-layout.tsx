import { Outlet } from "react-router"
import { NavBarComponent } from "./components/navbar/navbar-component.tsx"
import type { ICompany } from "../../core/companies/index.ts"
import { useEffect, useState } from "react"
import { CompanyContext, type ICompanyContext } from "../../contexts/company-context.ts"
import { Spinner } from "@fluentui/react-components"
import { ajax } from "rxjs/ajax"
import { serverData } from "../../strings/serverData.ts"
import { current } from "@reduxjs/toolkit"

export function AppRootLayout() {
    const [company, setCompany] = useState<ICompany | undefined>()

    const context: ICompanyContext = {
        company: company,
        setCompany: (company) => setCompany(company),
        removeCompany: () => setCompany(undefined)
    }

    function loadCompany() {
        ajax<ICompany>({
            url: serverData.route("/companies/current"),
            method: "GET",
            withCredentials: true
        }).subscribe({
            next(response) {
                if (response.status == 200) {
                    setCompany(response.response)
                }
            }
        })
    }

    useEffect(() => {
        if (company)
            loadCompany()
    }, [current])

    return (
        <CompanyContext value={context}>
            <div className="app-layout">
                <NavBarComponent />
                {
                    company ? <Outlet /> : <div className="no-company-selected">
                        <p><Spinner label="Loading Company..." labelPosition="below"></Spinner></p>
                    </div>
                }
            </div>
        </CompanyContext >
    )
}