import {useIdentityService} from "../hooks/use-identity-service.ts";
import {useNavigate, Outlet} from "react-router";
import {useEffect} from "react";

export function AuthenticatedRoute() {
    const { isSignedIn } = useIdentityService();
    const navigate = useNavigate();

    useEffect(() => {
        if(!isSignedIn())
            navigate("/auth");
    })

    if(isSignedIn())
        return <Outlet />;

    return isSignedIn() ? <Outlet /> : <div>Loading User...</div>;
}