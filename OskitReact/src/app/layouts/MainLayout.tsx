import { useEffect } from 'react'
import { Outlet } from 'react-router-dom'
import { makeStyles, Spinner, tokens } from '@fluentui/react-components'
import useIdentityManager from '../../hooks/IdentityManager'
import TopbarAccountComponent from './shared/TopbarAccountComponent'

export default function MainLayout() {
	const identityManager = useIdentityManager()
	const user = identityManager.getUser()
	const styles = MakeEntryLayoutStyles()

	function renderLoading() {
		return (
			<div className={styles.SessionSpinner}>
				<div><Spinner appearance='primary' labelPosition='below' label={"Establishing session..."} /></div>
			</div>
		)
	}

	useEffect(() => {
		if (!user)
			identityManager.confirmSignIn()

	}, [identityManager, user])

	return <>
		{
			!user ? renderLoading() :
				<div className={styles.EntryLayoutRoot}>
					<div className={styles.AppNavBar}>
						<TopbarAccountComponent />
					</div>
					<div className={styles.RouteOutlet}>
						<Outlet />
					</div>
				</div>
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
		alignItems: "center"
	},
	AppNavBar: {
		padding: tokens.spacingHorizontalS,
		width: "100%",
		height: "auto",
		//boxShadow: Depths.depth4,
		display: "flex",
		backgroundColor: tokens.colorNeutralBackground1
	},
	RouteOutlet: {
		width: "100%",
		minHeight: "100%"
	}
})