import { useEffect, } from 'react'
import { Outlet, useLocation, useNavigate } from 'react-router-dom'
import { Button, makeStyles, Menu, MenuDivider, MenuItem, MenuList, MenuPopover, MenuTrigger, Spinner, Toaster, tokens, Toolbar, ToolbarButton, ToolbarDivider, ToolbarGroup, Tooltip, useId } from '@fluentui/react-components'
import useIdentityManager from '../../hooks/IdentityManager'
import { AppContext, IAppContext } from '../../contexts'
import { TbApps, TbChevronDown, TbLogout, TbPlus, TbSettings, TbUserCircle } from 'react-icons/tb'
import { AppRoutes } from '../../strings'
import { breakpoints } from '../../strings/ui'
import { Depths } from '@fluentui/react'

const renderLoadingElement = (styles: ReturnType<typeof MakeEntryLayoutStyles>) => {
	return (
		<div className={styles.SessionSpinner}>
			<Spinner appearance='primary'
				labelPosition='below'
				label={"Establishing session..."} />
		</div>
	)
}

export default function MainLayout() {
	const identityManager = useIdentityManager()
	const styles = MakeEntryLayoutStyles()
	const user = identityManager.getUser()
	const toasterId = useId("main-toaster")
	const location = useLocation()
	const navigate = useNavigate()

	useEffect(() => {
		if (!user)
			identityManager.confirmSignIn()
	}, [identityManager, user, location])

	const appContextData: IAppContext = {
		toasterId
	}

	return <>
		{
			!user ? renderLoadingElement(styles) :
				<>
					<AppContext.Provider value={appContextData}>
						<Toaster toasterId={toasterId} />
						<div className={styles.EntryLayoutRoot}>
							<div className={styles.AppNavBar}>
								<Toolbar size='small' className={styles.NavBarToolbar}>
									<ToolbarGroup role='presentation' className={styles.ToolbarGroup}>
										<ToolbarButton tabIndex={0} onClick={() => navigate(AppRoutes.Home)} appearance="primary" icon={<TbApps />} />
										<ToolbarDivider />
										{/* <Menu>
											<MenuTrigger disableButtonEnhancement>
												<Button icon={<TbPlus size={76} />}
													iconPosition='after'
													appearance='primary' />
											</MenuTrigger>
											<MenuPopover>
												<MenuList>
													<MenuItem icon={<TbSettings />}>Settings</MenuItem>
												</MenuList>
											</MenuPopover>
										</Menu> */}
										<Menu>
											<MenuTrigger disableButtonEnhancement>
												<Button icon={<TbChevronDown size={76} />}
													iconPosition='after'
													appearance='subtle'>Sales</Button>
											</MenuTrigger>
											<MenuPopover>
												<MenuList>
													<Menu>
														<MenuTrigger disableButtonEnhancement>
															<MenuItem>Customers</MenuItem>
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
															<MenuItem>Reports</MenuItem>
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
												<Button icon={<TbChevronDown size={76} />}
													iconPosition='after'
													appearance='subtle'>Purchases</Button>
											</MenuTrigger>
											<MenuPopover>
												<MenuList>
													<Menu>
														<MenuTrigger disableButtonEnhancement>
															<MenuItem>Suppliers</MenuItem>
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
												<MenuItem>Purchase Orders</MenuItem>
												<MenuItem>Invoices</MenuItem>
												<MenuItem>Returns</MenuItem>
												<MenuItem>Payments</MenuItem>
												<MenuDivider />
												<MenuList>
													<Menu>
														<MenuTrigger disableButtonEnhancement>
															<MenuItem>Reports</MenuItem>
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
												<Button icon={<TbChevronDown size={76} />}
													iconPosition='after'
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
															<MenuItem>Reports</MenuItem>
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
									</ToolbarGroup>
									<ToolbarGroup role='presentation' className={styles.ToolbarGroup}>
										<Menu>
											<MenuTrigger disableButtonEnhancement>
												<Button appearance="secondary" icon={<TbSettings />}></Button>
											</MenuTrigger>
											<MenuPopover>
												<MenuList>
													<MenuItem icon={<TbSettings />}>Settings</MenuItem>
												</MenuList>
											</MenuPopover>
										</Menu>
										<Tooltip relationship="description" content="View Profile" appearance="inverted">
											<ToolbarButton content="View Profile" onClick={() => location.pathname != AppRoutes.Account.Profile ? navigate(AppRoutes.Account.Profile) : null}
												appearance="primary"
												icon={<TbUserCircle />} />
										</Tooltip>
										<Tooltip relationship="description" content="Logout" appearance="inverted">
											<Button appearance='secondary' onClick={() => identityManager.signOut()} icon={<TbLogout />} />
										</Tooltip>
									</ToolbarGroup>
								</Toolbar>
							</div>
							<div className={styles.RouteOutlet}>
								<Outlet />
							</div>
						</div>
					</AppContext.Provider>
				</>
		}
	</>
}

const MakeEntryLayoutStyles = makeStyles({
	SessionSpinner: {
		width: "100%",
		height: "100%",
		display: "flex",
		flexDirection: "row",
		alignItems: "center",
		justifyContent: "center"
	},
	EntryLayoutRoot: {
		width: "100%",
		minHeight: "100%",
		height: "auto",
		display: "flex",
		flexDirection: "column",
		alignItems: "center",
	},
	AppNavBar: {
		padding: tokens.spacingHorizontalXS,
		width: "100%",
		height: "auto",
		boxShadow: Depths.depth4,
		display: "flex",
		backgroundColor: tokens.colorNeutralBackground1,
		justifyContent: "space-between",
		flexDirection: "row"
	},
	RouteOutlet: {
		width: "100%",
		minHeight: "100%"
	},
	NavBarToolbar: {
		justifyContent: "space-between",
		width: "100%",
		display: "flex",
		padding: tokens.spacingHorizontalXXS,
		maxWidth: breakpoints.XxLarge,
		margin: "0 auto"
	},
	ToolbarGroup: {
		columnGap: tokens.spacingHorizontalXS,
		display: "flex"
	}
})