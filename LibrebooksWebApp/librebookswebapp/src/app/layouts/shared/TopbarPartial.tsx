import { Button, Menu, MenuDivider, MenuItem, MenuList, MenuPopover, MenuTrigger, ToolbarButton, ToolbarDivider } from "@fluentui/react-components";
import { BiChevronDown } from "react-icons/bi";
import { FaStoreAlt } from "react-icons/fa";
import { MdAccountBalance, MdBusiness, MdDashboard, MdInventory } from "react-icons/md";
import { TbPlus, TbReportAnalytics } from "react-icons/tb";



export default function TopbarComponent() {
    return (
        <>
            <ToolbarDivider />
            <ToolbarButton appearance='primary' icon={<MdDashboard />}>Dashboard</ToolbarButton>
            <Menu>
                <MenuTrigger disableButtonEnhancement>
                    <Button tabIndex={0} icon={<BiChevronDown />}
                        iconPosition='after'
                        appearance='subtle'>Customers</Button>
                </MenuTrigger>
                <MenuPopover>
                    <MenuList>
                        <Menu>
                            <MenuTrigger disableButtonEnhancement>
                                <MenuItem icon={<MdBusiness />}>Customers</MenuItem>
                            </MenuTrigger>
                            <MenuPopover>
                                <MenuList>
                                    <MenuItem icon={<TbPlus />}>New Customer</MenuItem>
                                    <MenuItem>Customers</MenuItem>
                                    <MenuItem>Customer Categories</MenuItem>
                                </MenuList>
                            </MenuPopover>
                        </Menu>
                    </MenuList>
                    <MenuDivider />
                    <MenuItem>Quotes</MenuItem>
                    <MenuItem>Sales Orders</MenuItem>
                    <MenuItem>Invoices</MenuItem>
                    <MenuItem>Credit Notes</MenuItem>
                    <MenuItem>Receipts</MenuItem>
                    <MenuDivider />
                    <MenuList>
                        <Menu>
                            <MenuTrigger disableButtonEnhancement>
                                <MenuItem icon={<TbReportAnalytics />}>Reports</MenuItem>
                            </MenuTrigger>
                            <MenuPopover>
                                <MenuList>
                                    <MenuItem>Customer Statement</MenuItem>
                                    <MenuItem>Quote Report</MenuItem>
                                    <MenuItem>Invoice Report</MenuItem>
                                    <MenuItem>Credit Note Report</MenuItem>
                                </MenuList>
                            </MenuPopover>
                        </Menu>
                    </MenuList>
                </MenuPopover>
            </Menu>
            <Menu>
                <MenuTrigger disableButtonEnhancement>
                    <Button tabIndex={0} icon={<BiChevronDown />}
                        iconPosition='after'
                        appearance='subtle'>Suppliers</Button>
                </MenuTrigger>
                <MenuPopover>
                    <MenuList>
                        <Menu>
                            <MenuTrigger disableButtonEnhancement>
                                <MenuItem icon={<FaStoreAlt />}>Suppliers</MenuItem>
                            </MenuTrigger>
                            <MenuPopover>
                                <MenuList>
                                    <MenuItem icon={<TbPlus />}>New Supplier</MenuItem>
                                    <MenuItem>List of Suppliers</MenuItem>
                                    <MenuItem>Supplier Categories</MenuItem>
                                </MenuList>
                            </MenuPopover>
                        </Menu>
                    </MenuList>
                    <MenuDivider />
                    <MenuItem>Purchase Orders</MenuItem>
                    <MenuItem>Invoices</MenuItem>
                    <MenuItem>Returns</MenuItem>
                    <MenuItem>Payments</MenuItem>
                    <MenuDivider />
                    <MenuList>
                        <Menu>
                            <MenuTrigger disableButtonEnhancement>
                                <MenuItem icon={<TbReportAnalytics />}>Reports</MenuItem>
                            </MenuTrigger>
                            <MenuPopover>
                                <MenuList>
                                    <MenuItem>Customer Statement</MenuItem>
                                    <MenuItem>Order Report</MenuItem>
                                    <MenuItem>Invoice Report</MenuItem>
                                    <MenuItem>Payment Report</MenuItem>
                                </MenuList>
                            </MenuPopover>
                        </Menu>
                    </MenuList>
                </MenuPopover>
            </Menu>
            <Menu>
                <MenuTrigger disableButtonEnhancement>
                    <Button tabIndex={0} icon={<MdInventory />}
                        iconPosition='before'
                        appearance='subtle'>Inventory</Button>
                </MenuTrigger>
                <MenuPopover>
                    <MenuItem icon={<TbPlus />}>New Item</MenuItem>
                    <MenuDivider />
                    <MenuItem>List of Items</MenuItem>
                    <MenuItem>Item Categories</MenuItem>
                    <MenuItem>Item Adjustments</MenuItem>
                    <MenuDivider />
                    <MenuList>
                        <Menu>
                            <MenuTrigger disableButtonEnhancement>
                                <MenuItem icon={<TbReportAnalytics />}>Reports</MenuItem>
                            </MenuTrigger>
                            <MenuPopover>
                                <MenuList>
                                    <MenuItem>Customer Statement</MenuItem>
                                    <MenuItem>Order Report</MenuItem>
                                    <MenuItem>Invoice Report</MenuItem>
                                    <MenuItem>Payment Report</MenuItem>
                                </MenuList>
                            </MenuPopover>
                        </Menu>
                    </MenuList>
                </MenuPopover>
            </Menu>
            <Menu>
                <MenuTrigger disableButtonEnhancement>
                    <Button tabIndex={0} icon={<MdAccountBalance />}
                        iconPosition='before'
                        appearance='subtle'>Banking</Button>
                </MenuTrigger>
                <MenuPopover>
                    <MenuItem>Bank Accounts</MenuItem>
                    <MenuItem>Journal</MenuItem>
                    <MenuItem>Ledger</MenuItem>
                    <MenuDivider />
                    <MenuList>
                        <Menu>
                            <MenuTrigger disableButtonEnhancement>
                                <MenuItem icon={<TbReportAnalytics />}>Reports</MenuItem>
                            </MenuTrigger>
                            <MenuPopover>
                                <MenuList>
                                    <MenuItem>Customer Statement</MenuItem>
                                    <MenuItem>Order Report</MenuItem>
                                    <MenuItem>Invoice Report</MenuItem>
                                    <MenuItem>Payment Report</MenuItem>
                                </MenuList>
                            </MenuPopover>
                        </Menu>
                    </MenuList>
                </MenuPopover>
            </Menu>
        </>
    )
}