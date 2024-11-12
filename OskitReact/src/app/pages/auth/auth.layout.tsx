import { GetIdentity, SignOut } from '../../../slices/identity.slice'
import { useSelector } from 'react-redux'
import { Outlet, useLocation, useNavigate, useSearchParams } from 'react-router-dom'
import { useAppDispatch } from '../../../hooks/store.hook'
import { useEffect, useState } from 'react'
import { Depths } from '@fluentui/react'
import { Body1, CompoundButton, makeStyles, ProgressBar, Subtitle1, tokens, Tooltip } from '@fluentui/react-components'
import { AuthContext, IAuthContext } from './auth.context'
import AppRoutes from '../../../strings/AppRoutes'
import useSessionData from '../../../extensions/SessionData'
import Borders from '../../../strings/ui/Borders'
import { BsEnvelopeAt } from 'react-icons/bs'

const AuthSessionVars = {
	Username: "USERNAME",
	VerificationReason: "VERIFICATION_REASON",
	VerificationOTP: "VERIFICATION_OTP",
	VerificationHashString: "VERIFICATION_HASH_STRING"
}

export default function AuthLayout() {
	const identity = useSelector(GetIdentity)
	const dispatch = useAppDispatch()
	const search = useSearchParams()
	const returnUrl = search[0]?.get("returnUrl")
	const [loading, setLoading] = useState(false)
	const [formTitle, setFormTitle] = useState("Login or Register")
	const [formMessage, setFormMessage] = useState("")
	const [username, setUsername] = useState("")
	const [givenName, setGivenName] = useState("Thabo")
	const location = useLocation()
	const session = useSessionData()
	const navigate = useNavigate()

	const styles = AuthLayoutStyles()
	useEffect(() => {
		if (identity.isAuthenticated)
			dispatch(SignOut())
	}, [identity, dispatch])

	const context: IAuthContext = {
		givenName,
		loading,
		username,
		returnUrl,
		setLoading,
		setFormTitle,
		setFormMessage,
		setGivenName,
		setUsername: (email: string) => {
			setUsername(email)
			if (email)
				session.add(AuthSessionVars.Username, email)
		}
	}

	function RenderChangeUsernamePill() {
		if (location.pathname != AppRoutes.Auth.Username) {
			return (
				<>
					<Tooltip
						content={`Change username`}
						relationship="label">
						<CompoundButton
							className={styles.usernamePill}
							shape='rounded'
							icon={<BsEnvelopeAt fontSize={24} />}
							iconPosition='before'
							appearance='secondary'
							secondaryContent="Change your Email"
							onClick={() => navigate({
								pathname: AppRoutes.Auth.Username,
								search: search[0]?.toString()
							})}>
							{username}
						</CompoundButton>
					</Tooltip>
				</>
			)
		}
	}

	useEffect(() => {
		if (!username) {
			const s = session.get<string>(AuthSessionVars.Username)
			if (s) setUsername(s)
		}
	}, [session, username])

	return <>
		<AuthContext.Provider value={context}>
			<div className={"auth-root " + styles.wrapper}>
				<div className={`animate__animated animate__fadeIn ${styles.contentWrapper} ${location.pathname !== AppRoutes.Auth.Register ? styles.slimContentWrapper : styles.widerContentWrapper}`}>
					{loading ? <ProgressBar /> : ""}
					<div className={styles.formWrapper}>
						{RenderChangeUsernamePill()}
						<Subtitle1
							className={styles.formTitle}
							align='center'>
							{formTitle}
						</Subtitle1>
						{formMessage ? <Body1 className={styles.formMessage}>{formMessage}</Body1> : ""}
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
		marginBottom: tokens.spacingVerticalS
	},
	contentWrapper: {
		boxShadow: Depths.depth4,
		height: "auto",
		borderRadius: tokens.borderRadiusMedium,
		margin: "0 auto",
		overflow: "hidden",
		border: Borders.thinNeutral
	},
	slimContentWrapper: {
		width: "380px"
	},
	usernamePill: {
		marginBottom: tokens.spacingVerticalL,
		padding: `${tokens.spacingHorizontalS}`,
		display: "flex",
		justifyContent: "flex-start"
	},
	widerContentWrapper: {
		width: "480px"
	}
})
