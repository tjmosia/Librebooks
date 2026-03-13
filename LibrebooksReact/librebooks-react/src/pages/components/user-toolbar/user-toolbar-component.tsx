import { Menu, MenuItem, MenuList, MenuPopover, MenuTrigger, Toolbar, ToolbarButton } from "@fluentui/react-components";
import { MdAccountCircle, MdLogout } from "react-icons/md";
import { useIdentityService } from "../../../hooks";

export function UserToolbarComponent() {
    const { logout } = useIdentityService()
    return (
        <Toolbar>
            <Menu>
                <MenuTrigger>
                    <ToolbarButton icon={<MdAccountCircle />}></ToolbarButton>
                </MenuTrigger>
                <MenuPopover>
                    <MenuList>
                        <MenuItem icon={<MdAccountCircle />}>Profile</MenuItem>
                        <MenuItem icon={<MdLogout />} onClick={logout}>Logout</MenuItem>
                    </MenuList>
                </MenuPopover>
            </Menu>
        </Toolbar>
    )
}