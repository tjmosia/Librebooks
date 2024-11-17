import { Outlet, useLocation, useNavigate, useSearchParams } from 'react-router-dom'
import { useAppDispatch } from '../../../hooks/store.hook'
import { useCallback, useEffect, useState } from 'react'
import { Depths } from '@fluentui/react'
import { Body1, Body1Strong, Button, makeStyles, MessageBar, MessageBarBody, ProgressBar, Subtitle1, tokens, Tooltip } from '@fluentui/react-components'
import { AuthContext, IAuthContext } from '../../../contexts/auth.context'
import AppRoutes from '../../../strings/AppRoutes'
import useSessionData from '../../../extensions/SessionData'
import Borders from '../../../strings/ui/Borders'
import { BsArrowLeft } from 'react-icons/bs'
import { AuthSessionVars } from './auth.strings'
import { IUserLoginDto } from '../account/LoginDto.type'
import { useIdentityManager } from '../../../hooks/userManager.hook'
import useTempData from '../../../extensions/TempData'
import { Intent } from '../../../strings/ui/Intent'

export interface IAuthModelAlert {
	message: string
	intent: keyof typeof Intent
}

export default function AuthLayout() {
	const identityManager = useIdentityManager()
	const dispatch = useAppDispatch()
	const search = useSearchParams()
	const session = useSessionData()
	const location = useLocation()
	const navigate = useNavigate()
	const tempData = useTempData()

	const [loading, setLoading] = useState(false)
	const [formTitle, setFormTitle] = useState("Login or Register")
	const [formMessage, setFormMessage] = useState("")
	const [user, setUser] = useState<IUserLoginDto | undefined>(session.get<IUserLoginDto>(AuthSessionVars.User) ?? undefined)
	const [username, setUsername] = useState<string>(session.get(AuthSessionVars.Username) ?? "")
	const [alert, setAlert] = useState<IAuthModelAlert | undefined>(tempData.get<IAuthModelAlert>('alert') ?? undefined)
	const styles = AuthLayoutStyles()

	useEffect(() => {
		if (location.pathname === AppRoutes.Auth.Username) {
			if (identityManager.isSignedIn()) {
				identityManager.signOut()
			}
		}
	}, [identityManager, dispatch, session, location])

	const context: IAuthContext = {
		loading,
		username,
		user,
		setUser: (user) => {
			setUser(user)
			if (user)
				session.add(AuthSessionVars.User, user)
			else
				session.remove(AuthSessionVars.User)
		},
		setLoading,
		setFormTitle,
		setFormMessage,
		setAlert: (alert: IAuthModelAlert | undefined) => setAlert(alert),
		setUsername: (value: string) => {
			setUsername(value)
			if (value)
				session.add(AuthSessionVars.Username, value)
			else
				session.remove(AuthSessionVars.Username)
		},
	}

	function RenderChangeUsernameBar() {
		if (location.pathname != AppRoutes.Auth.Username)
			return (
				<>
					<div className={styles.changeUsernameBar}>
						<Tooltip
							content={`Change username`}
							relationship="label">
							<Button appearance='primary'
								icon={<BsArrowLeft size={20} />}
								onClick={() => navigate(AppRoutes.Auth.Username)}></Button>
						</Tooltip>
						<div className={styles.usernameLabel}>
							<Body1Strong>{username}</Body1Strong>
						</div>
					</div>
				</>
			)

	}

	useEffect(() => {
		if (location.pathname === AppRoutes.Auth.Username)
			return

		if (!username) {
			const _username = session.get<string>(AuthSessionVars.Username)
			if (_username)
				setUsername(_username)
		}

		if (location.pathname === AppRoutes.Auth.Login) {
			if (!user) {
				const user = session.get<IUserLoginDto>(AuthSessionVars.User)
				if (user)
					setUser(user)
			}
		}

	}, [username, location, session, user])

	useEffect(() => {
		if (location.pathname === AppRoutes.Auth.Username) {
			session.remove(AuthSessionVars.User)
			session.remove(AuthSessionVars.VerificationReason)
			session.remove(AuthSessionVars.VerificationCode)
			session.remove(AuthSessionVars.VerificationHashString)
		}
	}, [session, location, search])

	const renderAlert = useCallback(() => alert ? (<MessageBar intent={alert.intent}>
		<MessageBarBody>{alert.message}</MessageBarBody>
	</MessageBar>) : "", [alert])

	useEffect(() => {
		if (alert)
			setTimeout(() => setAlert(undefined), 5000)
	}, [alert])

	return <>
		<AuthContext.Provider value={context}>
			<div className={"auth-root " + styles.wrapper}>
				<div className={`animate__animated animate__fadeIn ${styles.contentWrapper} ${location.pathname !== AppRoutes.Auth.Register ? styles.slimContentWrapper : styles.widerContentWrapper}`}>
					{loading ? <ProgressBar /> : ""}
					{RenderChangeUsernameBar()}
					<div className={styles.formWrapper}>
						<Subtitle1
							className={styles.formTitle}
							align='center'>
							{formTitle}
						</Subtitle1>
						{formMessage ? <Body1 className={styles.formMessage}>{formMessage}</Body1> : ""}
						{renderAlert()}
						<Outlet />
					</div>
				</div>
			</div>
		</AuthContext.Provider>
	</>
}

const AuthLayoutStyles = makeStyles({
	formWrapper: {
		//border: Borders.thinNeutral
		//border: `1px solid ${tokens.colorNeutralStroke1}`
		padding: tokens.spacingVerticalXXL,
		display: "flex",
		flexDirection: "column"
	},
	wrapper: {
		padding: tokens.spacingVerticalXXL,
		boxSizing: "border-box",
		width: "100%"
	},
	formTitle: {
		marginBottom: tokens.spacingVerticalL
	},
	formMessage: {
		marginBottom: tokens.spacingVerticalS,
		textAlign: "justify"
	},
	contentWrapper: {
		boxShadow: Depths.depth8,
		height: "auto",
		borderRadius: tokens.borderRadiusMedium,
		margin: "0 auto",
		overflow: "hidden",
		border: Borders.thinNeutral,
		backgroundColor: tokens.colorNeutralCardBackgroundHover
	},
	slimContentWrapper: {
		width: "380px"
	},
	usernamePill: {
		padding: `0`,
		display: "flex",
		justifyContent: "flex-start"
	},
	widerContentWrapper: {
		width: "480px"
	},
	changeUsernameBar: {
		width: "100%",
		borderBottom: Borders.thinNeutral,
		padding: tokens.spacingHorizontalS,
		display: "flex",
		flexDirection: "row",
		height: "auto",
		justifyContent: "flex-start",
		alignItems: "center",
		backgroundColor: tokens.colorNeutralBackground1Hover
	},
	usernameLabel: {
		height: "100%",
		paddingLeft: tokens.spacingHorizontalM,
		display: "flex",
		alignItems: "center"
	}
})
