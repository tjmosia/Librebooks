import { Outlet } from "react-router";

export default function AccountLayout() {
    return (
        <div>
            <h1>Account Profile Page</h1>
            <Outlet />
        </div>
    )
}