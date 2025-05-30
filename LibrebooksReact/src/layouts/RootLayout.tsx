import { Outlet, useNavigate } from "react-router";
import { useLocation } from "react-router";
import { useEffect } from "react";

export default function RootLayout() {
    const navigate = useNavigate()
    const location = useLocation()

    useEffect(() => {
        if (location.pathname === "/")
            navigate("/home/")
    }, [location, navigate])

    return (
        <>
            <div className="root layout">
                < Outlet />
            </div>
        </>
    )
}