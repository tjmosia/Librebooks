import { Button, NavbarGroup, Popover, Menu, MenuItem, MenuDivider } from "@blueprintjs/core";
import { useNavigate } from "react-router";
import { routes } from "../../values";
import { useState } from "react";

export default function NavbarAccountComponent()
{
    const [isOpen, setIsOpen] = useState(false)
    const navigate = useNavigate()

    return (<>
        <NavbarGroup align="right">
            <Popover position="bottom-right" 
                hoverCloseDelay={1 }
                content={
                    <Menu size="small">
                        <MenuItem icon="user" text="Account Profile" />
                        <MenuDivider />
                        <MenuItem icon="help" text="Support" />
                        <MenuItem icon="log-out" text="Logout" />
                    </Menu>
                }>
                <Button size="large" icon="user" intent="primary" variant="minimal" />
            </Popover>
            <Button icon="control" size="large" intent="primary" variant="minimal" onClick={() => navigate(routes.admin.home)} />
        </NavbarGroup>
    </>)
}