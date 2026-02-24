import { type FC, useEffect } from "react";
import { Outlet, useLocation, useNavigate } from "react-router";
import './root-layout.css'
import { Caption2Strong } from "@fluentui/react-components";

export const RootLayout: FC = () => {
    const location = useLocation()
    const navigate = useNavigate()

    useEffect(() => {
        if (location.pathname == "/")
            navigate("/app")
    }, []);

    return (
        <div className="root-layout animate__animated animate__fadeIn">
            <div className="rootLayout__content">
                <Outlet />
            </div>
            <footer className="rootLayout__footer">
                <Caption2Strong className="footer-disclaimer">
                    &copy;{(new Date().getFullYear())} Librebooks SA, all rights reserved.
                </Caption2Strong>
            </footer>
        </div>
    )
}