import { Outlet, useLocation, useNavigate, useSearchParams } from 'react-router-dom'
import { useCallback, useEffect, useState } from 'react'
import { Depths } from '@fluentui/react'
import { Body1, Body1Strong, Button, makeStyles, MessageBar, MessageBarBody, ProgressBar, Subtitle1, ToastIntent, tokens, Tooltip } from '@fluentui/react-components'
import { AuthContext, IAuthContext } from '../../../contexts/AuthContext'
import AppRoutes from '../../../strings/AppRoutes'
import useSessionData from '../../../core/extensions/SessionData'
import Borders from '../../../strings/ui/Borders'
import { BsArrowLeft } from 'react-icons/bs'
import { AuthSessionVars } from './AuthStrings'
import { IUserLoginDto } from '../account/LoginDto.type'
import useIdentityManager from '../../../hooks/IdentityManager'
import useTempData from '../../../core/extensions/TempData'
import { Intent } from '../../../strings/ui/Intent'

export interface IAuthModelAlert {
	message: string
	intent: Intent
}

export interface IAuthToast {
	message: string
	intent: ToastIntent
}

export default function AuthLayout() {
	/*********************************************************************************************************************************
	 * SERVICES
	 *********************************************************************************************************************************/
	const identityManager = useIdentityManager()
	const search = useSearchParams()
	const session = useSessionData()
	const location = useLocation()
	const navigate = useNavigate()
	const tempData = useTempData()
	const styles = MakeAuthLayoutStyles()

	/*********************************************************************************************************************************
	 * STATE
	 *********************************************************************************************************************************/
	const [loading, setLoading] = useState(false)
	const [formTitle, setFormTitle] = useState("Login or Register")
	const [formMessage, setFormMessage] = useState("")
	const [user, setUser] = useState<IUserLoginDto | undefined>(session.get<IUserLoginDto>(AuthSessionVars.User) ?? undefined)
	const [username, setUsername] = useState<string>(session.get(AuthSessionVars.Username) ?? "")
	const [alert, setAlert] = useState<IAuthModelAlert | undefined>(tempData.get<IAuthModelAlert>('alert') ?? undefined)

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

	/*********************************************************************************************************************************
	 * METHODS
	 *********************************************************************************************************************************/
	const renderChangeUsernameBarElement = useCallback(() => {
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
	}, [location.pathname, username, styles, navigate])

	/*********************************************************************************************************************************
	 * EFFECTS
	 *********************************************************************************************************************************/
	useEffect(() => {
		if (location.pathname === AppRoutes.Auth.Username) {
			session.remove(AuthSessionVars.User)
			session.remove(AuthSessionVars.VerificationReason)
			session.remove(AuthSessionVars.VerificationCode)
			session.remove(AuthSessionVars.VerificationHashString)
		}
	}, [session, location, search])

	useEffect(() => {
		if (alert)
			setTimeout(() => setAlert(undefined), 5000)
	}, [alert])

	useEffect(() => {
		if (location.pathname === AppRoutes.Auth.Username && identityManager.getUser())
			identityManager.signOut()

	}, [location, identityManager])

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


	/*********************************************************************************************************************************
	 * RENDER
	 *********************************************************************************************************************************/
	return <>
		<AuthContext.Provider value={context}>
			<div className={"auth-root " + styles.wrapper}>
				<div className={`animate__animated animate__fadeIn ${styles.contentWrapper} ${location.pathname !== AppRoutes.Auth.Register ? styles.slimContentWrapper : styles.widerContentWrapper}`}>
					{loading ? <ProgressBar /> : ""}
					{renderChangeUsernameBarElement()}
					<div className={styles.formWrapper}>
						<Subtitle1
							className={styles.formTitle}
							align='center'>
							{formTitle}
						</Subtitle1>
						{formMessage ? <Body1 className={styles.formMessage}>{formMessage}</Body1> : ""}
						{alert ? (<MessageBar intent={alert.intent}>
							<MessageBarBody>{alert.message}</MessageBarBody>
						</MessageBar>) : ""}
						<Outlet />
					</div>
				</div>
			</div>
		</AuthContext.Provider>
	</>
}

const MakeAuthLayoutStyles = makeStyles({
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
