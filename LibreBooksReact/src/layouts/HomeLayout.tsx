import { Navbar, NavbarGroup, NavbarHeading } from "@blueprintjs/core";
import { Outlet, useNavigate } from "react-router";
import { NavbarAccountComponent } from "../components/navbar-account";
import './HomeLayout.scss'
import { useState } from "react";
import { ICompanyData } from "../contexts";

export default function HomeLayout() {
    return (
        <>
            <Navbar>
                <NavbarGroup>
                    <NavbarHeading>Home</NavbarHeading>
                </NavbarGroup>
                <NavbarAccountComponent />
            </Navbar>
            <div className="homeLayout-body">
                <h1>Home Layout</h1>
                <Outlet />
            </div>
        </>
    )
}