import { Outlet } from "react-router";
import { NavbarComponent } from "../../components/navbar/navbar-component";
import { UserToolbarComponent } from "../../components/user-toolbar/user-toolbar-component";

export function CompaniesLayout() {
    return (<div>
        <NavbarComponent>
            <>
                <div></div>
                <UserToolbarComponent />
            </>
        </NavbarComponent>
        <div className="companies-layout-content">
            <Outlet />
        </div>
    </div >)
}