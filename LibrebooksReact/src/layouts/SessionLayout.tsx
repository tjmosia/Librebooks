import { useEffect } from "react";
import { Outlet, useNavigate } from "react-router";
import useIdentityManager from "../hooks/useIdentityManager";

export default function SessionLayout() {
    const identityManager = useIdentityManager()
    const navigate = useNavigate()

    useEffect(() => {
        if (!identityManager.isAuthenticated)
            navigate("/auth/login")
    }, [identityManager, navigate])

    return (
        <>
            < Outlet />
        </>
    )
}