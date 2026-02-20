import { useIdentityService } from "../hooks/use-identity-service.ts";
import { useNavigate, Outlet } from "react-router";
import { useEffect, useState } from "react";

export function AuthenticatedRoute() {
    const { isSignedIn, confirmLogin } = useIdentityService();
    const navigate = useNavigate();
    const [tried, setTried] = useState(false)

    useEffect(() => {
        if (!isSignedIn()) {
            if (tried)
                navigate("/auth");
            else
                confirmLogin(() => setTried(true), (error) => {
                    () => setTried(true)
                    if (error.status == 401)
                        navigate("/auth")
                })
        }
    }, [isSignedIn, tried, navigate]);

    if (isSignedIn())
        return <Outlet />;

    return isSignedIn() ? <Outlet /> : <div>Loading User...</div>;
}