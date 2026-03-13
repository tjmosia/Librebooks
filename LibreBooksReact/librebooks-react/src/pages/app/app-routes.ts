import type { RouteObject } from "react-router";
import { AppRootLayout } from "./app-root-layout.tsx";
import { AppDashboardHomePage } from "./dashboard/home/app-dashboard-home-page.tsx";
import { CompaniesLayout } from "./companies/companies-layout.tsx";
import { CompaniesListPage } from "./companies/list/companies-list-page.tsx";
import { LoadCompaniesAsync } from "./companies/companies-loader.ts";

export const appRoutes: RouteObject = {
    path: "app",
    Component: AppRootLayout,
    children: [{
        index: true,
        Component: AppDashboardHomePage,
    }, {
        path: "companies",
        Component: CompaniesLayout,
        children: [
            {
                index: true,
                Component: CompaniesListPage,
                loader: async () => ({ companies: await LoadCompaniesAsync() })
            }
        ]
    }]
}