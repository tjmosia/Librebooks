import { Button, Menu, NavbarGroup, Popover, MenuItem, MenuDivider } from "@blueprintjs/core";


export default function NavbarAccountComponent() {
    const accountDropdownMenu = (<Menu size="small">
        <MenuItem icon="user" text="Profile" />
        <MenuItem icon="help" text="Support" />
        <MenuDivider />
        <MenuItem icon="log-out" text="Logout" />
    </Menu>)
    return (<>
        <NavbarGroup align="end">
            <Popover placement="bottom-end" content={accountDropdownMenu}>
                <Button size="large" icon="user" intent="primary" variant="minimal" />
            </Popover>
        </NavbarGroup>
    </>)
}