import { useEffect } from "react";
import { Outlet, useNavigate } from "react-router";

export default function MainLayout() {
    const navigate = useNavigate()

    return (
        <>
            < Outlet />
        </>
    )
}