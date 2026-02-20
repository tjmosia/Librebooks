import { Menu, MenuItem, MenuList, MenuPopover, MenuTrigger, Toolbar, ToolbarButton, ToolbarGroup } from '@fluentui/react-components'
import { useEffect } from 'react'
import { MdAccountBox, MdLogout, MdPerson } from 'react-icons/md'
import { useIdentityService } from '../../../../hooks/use-identity-service'
import './navbar-component.css'

export function NavBarComponent() {
    const { user, logout } = useIdentityService()

    useEffect(() => {
        console.log(user)
    }, [user])
    return (
        <header className="navbar">
            <div className='navbar-content'>
                <div className='navbar-brand' title={'Librebooks'}></div>
                <Toolbar className='navbar-session'>
                    <ToolbarGroup>
                        <Menu>
                            <MenuTrigger>
                                <ToolbarButton appearance='primary' icon={<MdAccountBox />} />
                            </MenuTrigger>
                            <MenuPopover>
                                <MenuList>
                                    <MenuItem icon={<MdPerson />}>Account Profile</MenuItem>
                                    <MenuItem onClick={() => logout()} icon={<MdLogout />}>Logout</MenuItem>
                                </MenuList>
                            </MenuPopover>
                        </Menu>
                    </ToolbarGroup>
                </Toolbar>
            </div>
        </header>
    )
}