import { useIdentityService } from "../hooks";
import { useNavigate, Outlet } from "react-router";
import { useEffect } from "react";
import { pageWasReloaded } from "../utils";

export function AuthenticatedRoute() {
    const { isLoggedIn, confirmServerLogin } = useIdentityService();
    const navigate = useNavigate();

    useEffect(() => {
        if (!isLoggedIn()) {
            if (pageWasReloaded()) {
                confirmServerLogin({
                    error: (error) => {
                        if (error.status == 401 || error.status == 0)
                            navigate("/auth")
                    }
                })
            } else {
                navigate("/auth");
            }
        }
    }, [isLoggedIn]);

    if (isLoggedIn())
        return <Outlet />;

    return isLoggedIn() ? <Outlet /> : <div>Loading User...</div>;
}