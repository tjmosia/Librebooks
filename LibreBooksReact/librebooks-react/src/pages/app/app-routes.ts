import type {RouteObject} from "react-router";
import {AppLayout} from "./app-layout.tsx";
import {AppHomePage} from "./home/app-home-page.tsx";

export const appRoutes : RouteObject = {
    path: "app",
    Component: AppLayout,
    children: [{
        index: true,
        Component: AppHomePage,
    },{
        path: "home",

    }]
}