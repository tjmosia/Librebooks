import { useIdentityService } from "../hooks";
import { useNavigate, Outlet } from "react-router";
import { useEffect, useState } from "react";

export function AuthenticatedRoute() {
    const { isLoggedIn, confirmServerLogin } = useIdentityService();
    const navigate = useNavigate();
    const [tried, setTried] = useState(false)

    useEffect(() => {
        if (!isLoggedIn()) {
            if (tried)
                navigate("/auth");
            else {
                confirmServerLogin({
                    error: (error) => {
                        () => setTried(true)
                        if (error.status == 401 || error.status == 0)
                            navigate("/auth")
                    }
                })
            }
        }
    }, [tried]);

    if (isLoggedIn())
        return <Outlet />;

    return isLoggedIn() ? <Outlet /> : <div>Loading User...</div>;
}