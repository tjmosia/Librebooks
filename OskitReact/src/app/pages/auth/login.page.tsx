import { ChangeEvent, FormEvent, useEffect, useState } from 'react'
import { Input, useId, Button, Tooltip, Spinner, makeStyles, tokens, Field } from '@fluentui/react-components'
import { usePageTitle } from '../../../hooks/page.hook'
import { BsLockFill, BsUnlockFill } from 'react-icons/bs'
import AppRoutes from '../../../strings/AppRoutes'
import { useNavigate, useSearchParams } from 'react-router-dom'
import { useAuthContext } from './auth.context'

const LoginStyles = makeStyles({
	wrapper: {
		display: "flex",
		flexDirection: "column"
	},
	links: {
		display: "flex",
		flexDirection: "row",
		justifyContent: "center",
	},
	buttonWrapper: {
		margin: `${tokens.spacingVerticalM} 0`
	}
})

interface IModel {
	password: string
	error?: string
}

export default function LoginPage() {
	usePageTitle("Login")
	const passwordId = useId("password")
	const navigate = useNavigate()
	const search = useSearchParams()
	const [loading, setLoading] = useState(false)
	const [showPassword, setShowPassword] = useState(false)
	const styles = LoginStyles()
	const { setFormTitle, givenName, setFormMessage } = useAuthContext()
	const [model, setModel] = useState<IModel>({
		password: ""
	})

	function TogglePasswordHandler() {
		setShowPassword(!showPassword)
	}

	function InputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
		setModel({
			password: event.target.value,
			error: ""
		})
	}

	function FormSubmitHandler(event: FormEvent) {
		event.preventDefault()
		setLoading(true)
	}

	function RenderPasswordSpyElement() {

		return (
			<Tooltip
				content={`Click to ${showPassword ? "hide" : "show"} password`}
				relationship="label">
				<Button appearance="secondary"
					size="small"
					onClick={TogglePasswordHandler}
					icon={!showPassword ? <BsLockFill size={16} /> : <BsUnlockFill size={16} />} />
			</Tooltip>
		)
	}

	useEffect(() => {
		setFormTitle!(`Welcome Back ${givenName}`)
		setFormMessage!("Enter your password to your access account.")
	}, [setFormTitle, givenName, setFormMessage])

	return (<>
		<form onSubmit={FormSubmitHandler}
			method="post"
			className="form">
			<Field label="Password"
				validationMessage={model.error}
				validationState={model.error ? "error" : "none"}>
				<Input id={passwordId}
					appearance="outline"
					value={model.password}
					onChange={InputChangeHandler}
					name="password"
					size='large'
					disabled={loading}
					contentAfter={RenderPasswordSpyElement()}
					type={showPassword ? "text" : "password"}
					aria-label="Passwordd input" />
			</Field>
			<Field className={styles.buttonWrapper}>
				<Button appearance='primary'
					size='large'
					disabled={loading}>
					{loading ? <Spinner size="tiny" /> : "Login"}
				</Button>
			</Field>
			<div className={styles.links}>
				<Button appearance='transparent'
					aria-label='Forggotten password link'
					onClick={() => navigate({
						pathname: AppRoutes.Auth.VerifyEmail,
						search: search[0]?.toString()
					})}>
					Reset Password
				</Button>
			</div>
		</form>
	</>)
}


