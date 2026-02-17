import {Outlet} from "react-router";

export function AuthLayout(){
    return <>
        <div>
            Auth Works
            <Outlet />
        </div>
    </>
}