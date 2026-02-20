import type { RouteObject } from "react-router";
import { AuthLayout } from "./auth-layout.tsx";
import { AuthEntryPage } from "./entry/auth-entry-page.tsx";
import { lazy } from "react";
const LoginPage = lazy(() => import("./login/login-page.tsx"))
const RegisterPage = lazy(() => import("./register/register-page.tsx"))
const PasswordResetPage = lazy(() => import("./password-reset/password-reset-page.tsx"))

export const authRoutes: RouteObject = {
    path: "auth",
    Component: AuthLayout,
    children: [{
        index: true,
        Component: AuthEntryPage,
    },
    {
        path: "login",
        Component: LoginPage
    },
    {
        path: "register",
        Component: RegisterPage
    },
    {
        path: "reset-password",
        Component: PasswordResetPage
    }]
}