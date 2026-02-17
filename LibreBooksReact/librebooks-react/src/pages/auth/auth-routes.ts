import type {RouteObject} from "react-router";
import {AuthLayout} from "./auth-layout.tsx";
import {AuthEntryPage} from "./entry/auth-entry-page.tsx";

export const authRoutes : RouteObject = {
    path: "auth",
    Component: AuthLayout,
    children: [{
        index: true,
        Component: AuthEntryPage
    }]
}