import { Menu, MenuItem, MenuList, MenuPopover, MenuTrigger, Tab, TabList, Toolbar, ToolbarButton, ToolbarGroup } from '@fluentui/react-components'
import { useEffect } from 'react'
import { MdAccountBox, MdDocumentScanner, MdLogout, MdPerson, MdSettings } from 'react-icons/md'
import { useIdentityService } from '../../../../hooks'
import './navbar-component.css'
import type { ICompany } from '../../../../core/companies'

interface INavBarComponentProps {
    company: ICompany | undefined
}

export function NavBarComponent({ company }: INavBarComponentProps) {
    const { getUser, logout } = useIdentityService()
    const user = getUser()

    useEffect(() => {

    }, [user])
    return (
        <header className="navbar">
            <div className='navbar-content'>
                <div className='navbar-brand' title={'Librebooks'}></div>
                {
                    !company ? <TabList size='small'>
                        <Tab value="dashboard" className='company-name'>Dashboard</Tab>
                        <Tab value="quick-wizards" className='company-name'>Quick Wizards</Tab>
                        <Tab value="customers" className='company-name'>Sales</Tab>
                        <Tab value="suppliers" className='company-name'>Purchasing</Tab>
                        <Tab value="inventory" className='company-name'>Inventory</Tab>
                        <Tab value="banking" className='company-name'>Banking</Tab>
                        <Tab value="accounting" className='company-name'>Accounting</Tab>
                    </TabList> : <></>
                }

                <Toolbar className='navbar-session'>
                    <ToolbarGroup>
                        <ToolbarButton icon={<MdSettings />}></ToolbarButton>
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