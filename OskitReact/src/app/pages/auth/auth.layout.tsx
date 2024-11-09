import { GetIdentity, SignOut } from '../../../slices/identity.slice'
import { useSelector } from 'react-redux'
import { Outlet } from 'react-router-dom'
import { useAppDispatch } from '../../../hooks/store.hook'
import { createContext, useEffect, useState } from 'react'
import { Depths } from '@fluentui/react'
import './auth.layout.tsx.scss'
import { Body1, makeStyles, Subtitle1, tokens } from '@fluentui/react-components'

const AuthLayoutStyles = makeStyles({
	formWrapper: {
		boxShadow: Depths.depth4,
		width: "320px",
		height: "auto",
		padding: tokens.spacingVerticalXL,
		borderRadius: tokens.borderRadiusMedium,
		//border: Borders.thinNeutral
		//border: `1px solid ${tokens.colorNeutralStroke1}`
	},
	wrapper: {
		padding: tokens.spacingVerticalXXL,
		boxSizing: "border-box"
	},
	title: {
		marginBottom: tokens.spacingVerticalM
	}
})

const AuthContext = createContext({})

export default function AuthLayout() {
	const identity = useSelector(GetIdentity)
	const dispatch = useAppDispatch()

	const [loading, setLoading] = useState(false)
	const [formTitle, setFormTitle] = useState("Login or Register")
	const [formMessage, setFormMessage] = useState("")

	const styles = AuthLayoutStyles()
	useEffect(() => {
		if (identity.isAuthenticated)
			dispatch(SignOut())
	}, [identity, dispatch])

	const context = {
		loading,
		setLoading,
		setFormTitle,
		setFormMessage
	}

	return <>
		<AuthContext.Provider value={context}>
			<div className={"auth-root " + styles.wrapper}>
				<div className={"auth-root-content " + styles.formWrapper}>
					<Subtitle1
						className={styles.title}
						align='center'>
						{formTitle}
					</Subtitle1>
					{formMessage ? <Body1>{formMessage}</Body1> : ""}
					<Outlet />
				</div>
			</div>
		</AuthContext.Provider>
	</>
}
