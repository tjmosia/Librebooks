import { useEffect, } from 'react'
import { Outlet, useLocation, useNavigate } from 'react-router-dom'
import { Button, makeStyles, Menu, MenuDivider, MenuGroupHeader, MenuItem, MenuList, MenuPopover, MenuTrigger, Spinner, Toaster, tokens, Toolbar, ToolbarButton, ToolbarDivider, ToolbarGroup, Tooltip, useId } from '@fluentui/react-components'
import useIdentityManager from '../../hooks/IdentityManager'
import { AppContext, IAppContext } from '../../contexts'
import { TbHome, TbLogout, TbPlus, TbReportAnalytics, TbSettings, TbUserCircle } from 'react-icons/tb'
import { AppRoutes } from '../../strings'
import { breakpoints } from '../../strings/ui'
import { Depths } from '@fluentui/react'
import AuthorizedCompanyView from './shared/AuthorizedCompanyView'
import { useCompanyManager } from '../../hooks'
import { BiChevronDown } from 'react-icons/bi'

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
	const companyManager = useCompanyManager()
	const toasterId = useId("main-toaster")
	const location = useLocation()
	const navigate = useNavigate()

	useEffect(() => {
		if (!user && !location.pathname.startsWith(AppRoutes.Auth.Username))
			identityManager.confirmSignIn()
	}, [identityManager, user, location])

	const appContextData: IAppContext = {
		toasterId
	}

	function onHomeButtonClick() {
		companyManager.deleteCompany()
		navigate(AppRoutes.Home)
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
									<ToolbarGroup role='presentation' className={`${styles.ToolbarGroupLeft}`}>
										<Button tabIndex={0} appearance='primary' onClick={onHomeButtonClick} icon={<TbHome />} />
										<ToolbarDivider />
										<AuthorizedCompanyView>
											<>
												<Menu>
													<MenuTrigger disableButtonEnhancement>
														<Button tabIndex={0} icon={<TbPlus />}
															appearance='primary' />
													</MenuTrigger>
													<MenuPopover>
														<MenuList>
															<Menu>
																<MenuTrigger disableButtonEnhancement>
																	<MenuItem>Sales</MenuItem>
																</MenuTrigger>
																<MenuPopover>
																	<MenuGroupHeader>Sales Management</MenuGroupHeader>
																	<MenuDivider />
																	<MenuList>
																		<MenuItem>Quote</MenuItem>
																		<MenuItem>Sales Order</MenuItem>
																		<MenuItem>Invoice</MenuItem>
																		<MenuItem>Credit Note</MenuItem>
																		<MenuItem>Receipt</MenuItem>
																	</MenuList>
																</MenuPopover>
															</Menu>
														</MenuList>
														<MenuList>
															<Menu>
																<MenuTrigger disableButtonEnhancement>
																	<MenuItem>Purchases</MenuItem>
																</MenuTrigger>
																<MenuPopover>
																	<MenuGroupHeader>Purchases Management</MenuGroupHeader>
																	<MenuDivider />
																	<MenuList>
																		<MenuItem>Order</MenuItem>
																		<MenuItem>Invoice</MenuItem>
																		<MenuItem>Return</MenuItem>
																		<MenuItem>Payment</MenuItem>
																	</MenuList>
																</MenuPopover>
															</Menu>
														</MenuList>
														<MenuList>
															<Menu>
																<MenuTrigger disableButtonEnhancement>
																	<MenuItem>Inventory</MenuItem>
																</MenuTrigger>
																<MenuPopover>
																	<MenuGroupHeader>Inventory Management</MenuGroupHeader>
																	<MenuDivider />
																	<MenuList>
																		<MenuItem>Item</MenuItem>
																		<MenuItem>Adjustment</MenuItem>
																	</MenuList>
																</MenuPopover>
															</Menu>
														</MenuList>
														<MenuList>
															<Menu>
																<MenuTrigger disableButtonEnhancement>
																	<MenuItem>Accounting</MenuItem>
																</MenuTrigger>
																<MenuPopover>
																	<MenuGroupHeader>Accounting Management</MenuGroupHeader>
																	<MenuDivider />
																	<MenuList>
																		<MenuItem>Bank Account</MenuItem>
																		<MenuItem>Journal Account</MenuItem>
																	</MenuList>
																</MenuPopover>
															</Menu>
														</MenuList>
													</MenuPopover>
												</Menu>
												<ToolbarButton appearance='subtle'>Dashboard</ToolbarButton>
												<Menu>
													<MenuTrigger disableButtonEnhancement>
														<Button tabIndex={0} icon={<BiChevronDown />}
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
																		<MenuDivider />
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
																	<MenuGroupHeader>Reports</MenuGroupHeader>
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
																		<MenuDivider />
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
																	<MenuGroupHeader>Reports</MenuGroupHeader>
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
														<Button tabIndex={0} icon={<BiChevronDown />}
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
																	<MenuItem icon={<TbReportAnalytics />}>Reports</MenuItem>
																</MenuTrigger>
																<MenuPopover>
																	<MenuGroupHeader>Reports</MenuGroupHeader>
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
														<Button tabIndex={0} icon={<BiChevronDown />}
															iconPosition='after'
															appearance='subtle'>Accounting</Button>
													</MenuTrigger>
													<MenuPopover>
														<MenuItem>Chart of Accounts</MenuItem>
														<MenuItem>Bank Accounts</MenuItem>
														<MenuList>
															<Menu>
																<MenuTrigger disableButtonEnhancement>
																	<MenuItem>Journals</MenuItem>
																</MenuTrigger>
																<MenuPopover>
																	<MenuGroupHeader>Journals</MenuGroupHeader>
																	<MenuList>
																		<MenuItem>General Journal</MenuItem>
																		<MenuItem>Cash Receipts Journal</MenuItem>
																		<MenuItem>Cash Payments Journal</MenuItem>
																	</MenuList>
																</MenuPopover>
															</Menu>
														</MenuList>
														<MenuDivider />
														<MenuList>
															<Menu>
																<MenuTrigger disableButtonEnhancement>
																	<MenuItem icon={<TbReportAnalytics />}>Reports</MenuItem>
																</MenuTrigger>
																<MenuPopover>
																	<MenuList>
																		<MenuItem>Income Statement</MenuItem>
																		<MenuItem>Balance Sheet</MenuItem>
																		<MenuItem>Cashflow Statement</MenuItem>
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
															appearance='subtle'>Reports</Button>
													</MenuTrigger>
													<MenuPopover>
														<MenuList>
															<Menu>
																<MenuTrigger disableButtonEnhancement>
																	<MenuItem>Sales</MenuItem>
																</MenuTrigger>
																<MenuPopover>
																	<MenuGroupHeader>Sales Management</MenuGroupHeader>
																	<MenuDivider />
																	<MenuList>
																		<MenuItem>Quote</MenuItem>
																		<MenuItem>Sales Order</MenuItem>
																		<MenuItem>Invoice</MenuItem>
																		<MenuItem>Credit Note</MenuItem>
																		<MenuItem>Receipt</MenuItem>
																	</MenuList>
																</MenuPopover>
															</Menu>
														</MenuList>
														<MenuList>
															<Menu>
																<MenuTrigger disableButtonEnhancement>
																	<MenuItem>Purchases</MenuItem>
																</MenuTrigger>
																<MenuPopover>
																	<MenuGroupHeader>Purchases Management</MenuGroupHeader>
																	<MenuDivider />
																	<MenuList>
																		<MenuItem>Order</MenuItem>
																		<MenuItem>Invoice</MenuItem>
																		<MenuItem>Return</MenuItem>
																		<MenuItem>Payment</MenuItem>
																	</MenuList>
																</MenuPopover>
															</Menu>
														</MenuList>
														<MenuList>
															<Menu>
																<MenuTrigger disableButtonEnhancement>
																	<MenuItem>Inventory</MenuItem>
																</MenuTrigger>
																<MenuPopover>
																	<MenuGroupHeader>Inventory Management</MenuGroupHeader>
																	<MenuDivider />
																	<MenuList>
																		<MenuItem>Item</MenuItem>
																		<MenuItem>Adjustment</MenuItem>
																	</MenuList>
																</MenuPopover>
															</Menu>
														</MenuList>
														<MenuList>
															<Menu>
																<MenuTrigger disableButtonEnhancement>
																	<MenuItem>Accounting</MenuItem>
																</MenuTrigger>
																<MenuPopover>
																	<MenuGroupHeader>Accounting Management</MenuGroupHeader>
																	<MenuDivider />
																	<MenuList>
																		<MenuItem>Bank Account</MenuItem>
																		<MenuItem>Journal Account</MenuItem>
																	</MenuList>
																</MenuPopover>
															</Menu>
														</MenuList>
													</MenuPopover>
												</Menu>
											</>
										</AuthorizedCompanyView>
									</ToolbarGroup>
									<ToolbarGroup role='presentation' className={styles.ToolbarGroupRight}>
										<Menu>
											<MenuTrigger disableButtonEnhancement>
												<Button appearance="subtle" icon={<TbSettings />}></Button>
											</MenuTrigger>
											<MenuPopover>
												<MenuList>
													<MenuItem icon={<TbSettings />}>Settings</MenuItem>
												</MenuList>
											</MenuPopover>
										</Menu>
										<Tooltip relationship="description" content="View Profile" appearance="inverted">
											<ToolbarButton content="View Profile" onClick={() => location.pathname != AppRoutes.Account.Profile ? navigate(AppRoutes.Account.Profile) : null}
												appearance="subtle"
												icon={<TbUserCircle />} />
										</Tooltip>
										<Tooltip relationship="description" content="Logout" appearance="inverted">
											<Button appearance='subtle' onClick={() => identityManager.signOut()} icon={<TbLogout />} />
										</Tooltip>
									</ToolbarGroup>
								</Toolbar>
								{/* {renderRouteToolbar()} */}
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

// const MakeRoteToolarComponentStyles = makeStyles({
// 	toolbarWrapper: {
// 		padding: tokens.spacingHorizontalXS,
// 		borderRadius: tokens.borderRadiusMedium,
// 		maxWidth: breakpoints.XxLarge,
// 		width: "100%",
// 		margin: "0 auto",
// 		//border: borders.thinNeutral,
// 		boxShadow: Depths.depth16,
// 		//backgroundColor: tokens.colorNeutralBackground1Hover,
// 	},
// 	toolbar: {

// 	}
// })

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
		paddingTop: "48px"
	},
	AppNavBar: {
		width: "100%",
		height: "48px",
		boxShadow: Depths.depth4,
		display: "flex",
		backgroundColor: tokens.colorNeutralBackground1,
		justifyContent: "space-between",
		flexDirection: "column",
		padding: tokens.spacingVerticalS,
		position: "fixed",
		top: "0"
	},
	RouteOutlet: {
		width: "100%",
		minHeight: "100%"
	},
	NavBarToolbar: {
		justifyContent: "space-between",
		width: "100%",
		display: "flex",
		maxWidth: breakpoints.XxLarge,
		margin: "0 auto",
	},
	ToolbarGroupRight: {
		columnGap: tokens.spacingHorizontalXS,
		display: "flex",
		marginLeft: "auto"
	},
	ToolbarGroupLeft: {
		display: "flex"
	}
})