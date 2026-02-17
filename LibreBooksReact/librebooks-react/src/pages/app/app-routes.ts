import type {RouteObject} from "react-router";
import {AppLayout} from "./AppLayout.tsx";
import {HomePage} from "./home/home-page.tsx";

export const appRoutes : RouteObject = {
    path: "app",
    Component: AppLayout,
    children: [{
        index: true,
        Component: HomePage
    }]
}