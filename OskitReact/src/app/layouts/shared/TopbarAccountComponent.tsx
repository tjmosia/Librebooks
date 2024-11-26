import { Button, makeStyles, Menu, MenuItem, MenuList, MenuPopover, MenuTrigger, tokens } from "@fluentui/react-components";
import { BsPersonCircle, BsGear } from "react-icons/bs";
import { VscSignOut } from "react-icons/vsc";
import useIdentityManager from "../../../hooks/IdentityManager";
import { useNavigate } from "react-router";
import { AppRoutes } from "../../../strings";

const MakeTopbarAccountComponentStyles = makeStyles({
    TopbarAccountWrapper: {
        width: "auto",
        height: "auto",
        display: "inline-block",
        marginLeft: "auto",
    },
    TopbarSessionControlButtons: {
        display: "flex",
        flexDirection: "row",
        columnGap: tokens.spacingHorizontalXS
    }
})

export default function TopbarAccountComponent() {
    const styles = MakeTopbarAccountComponentStyles()
    const identityManager = useIdentityManager()
    const navigate = useNavigate()

    return (
        <div className={styles.TopbarAccountWrapper}>
            <div className={styles.TopbarSessionControlButtons}>
                <Menu>
                    <MenuTrigger disableButtonEnhancement>
                        <Button appearance="secondary" icon={<BsGear />}></Button>
                    </MenuTrigger>
                    <MenuPopover>
                        <MenuList>
                            <MenuItem icon={<BsGear />}>Settings</MenuItem>
                        </MenuList>
                    </MenuPopover>
                </Menu>
                <Menu>
                    <MenuTrigger disableButtonEnhancement>
                        <Button appearance="primary" icon={<BsPersonCircle />}></Button>
                    </MenuTrigger>
                    <MenuPopover>
                        <MenuList>
                            <MenuItem onClick={() => navigate(AppRoutes.Account.Profile)} icon={<BsPersonCircle />}>Profile</MenuItem>
                            <MenuItem onClick={() => identityManager.signOut()} icon={<VscSignOut />}>Logout</MenuItem>
                        </MenuList>
                    </MenuPopover>
                </Menu>
            </div>
        </div>
    )
}