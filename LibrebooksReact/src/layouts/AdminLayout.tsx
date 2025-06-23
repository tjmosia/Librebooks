import { Button, MenuItem, Navbar, NavbarGroup, Menu } from "@blueprintjs/core";
import { Outlet, useNavigate } from "react-router";
import {NavbarAccountComponent} from "../components/navbar-account";
import './AdminLayout.scss'
import { routes } from "../values";

export default function AdminLayout () {
    const navigate = useNavigate()

    return (
        <>
            <div className="admin-layout">
                <Navbar>
                    <NavbarGroup align="left">
                        <Button icon="home" intent="primary" onClick={ () => navigate(routes.home) } />
                    </NavbarGroup>
                    <NavbarAccountComponent />
                </Navbar>
                <div className="adminBody">
                    <aside className="adminSideMenuContainer">
                        <Menu>
                            <MenuItem icon="dashboard" text="Dashboard" />
                            <MenuItem icon="user" text="Users" />
                            <MenuItem icon="shop" text="Companies" />
                            <MenuItem icon="cog" text="System Setups">
                                <MenuItem text="Value Added Tax" />
                                <MenuItem text="Business Sectors" />
                                <MenuItem text="Chart of Accounts" />
                            </MenuItem>
                        </Menu>
                    </aside>
                    <div className="adminOutlet">
                        <Outlet />
                    </div>
                </div>
            </div>
        </>
    )
}