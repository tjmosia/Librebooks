import { Spinner } from "@fluentui/react-components"
import { useEffect, useState } from "react"
import { Outlet, useLocation, useNavigate } from "react-router"
import { ajax, type AjaxError } from "rxjs/ajax"
import { CompanyContext, type ICompanyContext } from "../../contexts/company-context.ts"
import type { ICompany } from "../../core/companies/index.ts"
import { serverData } from "../../strings/serverData.ts"
import { SessionData } from "../../utils/session-data-utils.ts"
import { AppSessionKeys } from "./app-session-keys.ts"

export function AppRootLayout() {
    const navigate = useNavigate()
    const location = useLocation()
    const companyId = SessionData.getItem<string | undefined>(AppSessionKeys.CompanyId)
    const [company, setCompany] = useState<ICompany | undefined>()

    const context: ICompanyContext = {
        company: company,
        setCompany: (company) => setCompany(company),
        removeCompany: () => setCompany(undefined)
    }

    if(!companyId && !location.pathname.includes("/app/companies")) {
        navigate("/app/companies")
        return <></>
    }

    function loadCompany(companyId: string) {
        ajax<ICompany>({
            url: serverData.route(`/companies/${companyId}`),
            method: "GET",
            withCredentials: true
        }).subscribe({
            next(response) {
                if (response.status == 200) {
                    setCompany(response.response)
                    SessionData.addItem(AppSessionKeys.CompanyId, response.response.id)
                }
            },
            error(error: AjaxError) {
                if (error.status == 404) {
                    SessionData.removeItem(AppSessionKeys.CompanyId)
                    navigate("/app/companies")
                }
            }
        })
    }

    useEffect(() => {
        if(!company && !companyId && !location.pathname.includes("/app/companies")) {
            navigate("/app/companies")
        }

        if(!company && companyId)
            loadCompany(companyId)

    }, [])

    return (
        <CompanyContext value={context}>
            {
                !location.pathname.includes("/app/companies") && !company ?
                    <Spinner label="Loading Company..." labelPosition="below" /> :
                    <Outlet />
            }
        </CompanyContext >
    )
}