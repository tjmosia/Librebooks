import {type FC, useEffect} from "react";
import {Outlet, useLocation, useNavigate} from "react-router";

export const RootLayout: FC = () => {
    const location = useLocation()
    const navigate = useNavigate()

    useEffect(() => {
        if(location.pathname == "/")
            navigate("/app")
    }, []);

    return(
        <div className="root-layout">
            <Outlet />
        </div>
    )
}