import { RouteObject } from "react-router"
import ViewCompanyPage from "./ViewCompanyPage"
import CreateCompanyPage from "./CreateCompanyPage"
import EditCompanyPage from "./EditCompanyPage"
import CompanyPage from "./CompanyPage"

const CompaniesRoutes: RouteObject[] = [
    {
        path: "companies/:id",
        element: <ViewCompanyPage />
    },
    {
        path: "companies/create",
        element: <CreateCompanyPage />
    },
    {
        path: "companies/:id/edit",
        element: <EditCompanyPage />
    },
    {
        path: "companies/:id",
        element: <CompanyPage />
    },
]

export default CompaniesRoutes