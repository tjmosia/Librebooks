import type {RouteObject} from "react-router";
import {RootLayout} from "./layouts/root/root-layout.tsx";
import {AuthenticatedRoute} from "./guards/authenticated-route.tsx";
import {appRoutes} from "./pages/app/app-routes.ts";
import {authRoutes} from "./pages/auth/auth-routes.ts";


const routes: RouteObject[] = [
    {
        path: "",
        Component: RootLayout,
        children: [
            {
              Component: AuthenticatedRoute,
              children: [
                  appRoutes,
              ]
            },
            authRoutes
        ]
    }
]

export {
    routes
}