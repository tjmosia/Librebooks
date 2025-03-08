import { RouteObject } from "react-router"
import CustomersPage from "./CustomersPage"
import CustomerQuotesPage from "./CustomerQuotesPage"
import CustomerOrdersPage from "./CustomerOrdersPage"

const CustomersRoutes: RouteObject[] = [
    {
        path: "customers",
        element: <CustomersPage />,
        children: [
            {
                path: "quotes",
                element: <CustomerQuotesPage />
            },
            {
                path: "orders",
                element: <CustomerOrdersPage />
            },
            {
                path: "invoices",
                element: <CustomerOrdersPage />
            },
            {
                path: "credits",
                element: <CustomerOrdersPage />
            },
            {
                path: "receipts",
                element: <CustomerOrdersPage />
            },
        ]
    },
]

export default CustomersRoutes