import { Outlet } from "react-router";
import { NavBarComponent } from "./components/navbar/navbar-component";


export function AppLayout() {



    return (<div className="app-layout">
        <NavBarComponent />
        <Outlet />
    </div>)
}