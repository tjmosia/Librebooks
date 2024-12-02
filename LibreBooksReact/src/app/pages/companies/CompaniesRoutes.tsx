import { RouteObject } from "react-router"
import ViewCompanyPage from "./ViewCompanyPage"
import CreateCompanyPage from "./CreateCompanyPage"
import EditCompanyPage from "./EditCompanyPage"

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
        path: "companies/create",
        element: <EditCompanyPage />
    },
]

export default CompaniesRoutes