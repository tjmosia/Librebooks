import { useEffect } from "react";
import { Outlet, useNavigate } from "react-router";
import { useUserManager } from "../hooks";
import { routes } from "../values";

export default function SessionLayout() {
    const {isSignedIn} = useUserManager()
    const navigate = useNavigate()

    useEffect(() => {
        if(!isSignedIn())
            navigate(routes.auth.login)

    }, [isSignedIn, navigate])

    return (
        <>
            < Outlet />
        </>
    )
}