import { ChangeEvent, FormEvent, useState } from 'react'
import { Input, Label, useId, Button, Tooltip, Divider, Subtitle1, Spinner, makeStyles, tokens } from '@fluentui/react-components'
import { usePageTitle } from '../../../hooks/page.hook'
import { TbLock, TbLockOpen } from 'react-icons/tb'
import AppRoutes from '../../../strings/AppRoutes'
import { useNavigate, useSearchParams } from 'react-router-dom'

const LoginStyles = makeStyles({
	wrapper: {
		display: "flex",
		flexDirection: "column"
	},
	field: {
		display: "flex",
		flexDirection: "column",
		margin: `${tokens.spacingVerticalL} 0`
	},
	links: {
		display: "flex",
		flexDirection: "row",
		justifyContent: "center",
		margin: `${tokens.spacingVerticalL} 0`
	},
	title: {
		marginBottom: tokens.spacingVerticalM
	}
})

export default function LoginPage() {
	usePageTitle("Login")
	const usernameId = useId("username")
	const passwordId = useId("password")
	const navigate = useNavigate()
	const search = useSearchParams()
	const [loading, setLoading] = useState(false)
	const [showPassword, setShowPassword] = useState(false)
	const styles = LoginStyles()

	function TogglePasswordHandler() {
		setShowPassword(!showPassword)
	}

	function InputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
		const { value, name } = event.target

		setModel(state => ({
			...state,
			[name]: {
				value
			}
		}))
	}

	function FormSubmitHandler(event: FormEvent) {
		event.preventDefault()
		setLoading(true)
	}

	const [model, setModel] = useState({
		username: {
			value: "",
			error: ""
		},
		password: {
			value: "",
			error: ""
		}
	})

	function RenderPasswordSpyElement() {

		return (
			<Tooltip
				content={`Click to ${showPassword ? "hide" : "show"} password`}
				relationship="label">
				<Button appearance="subtle"
					size="small"
					onClick={TogglePasswordHandler}
					icon={!showPassword ? <TbLock /> : <TbLockOpen />} />
			</Tooltip>
		)
	}

	return (<>
		<div className={styles.wrapper}>
			<Subtitle1
				className={styles.title}
				align='center'>
				Sign in
			</Subtitle1>
			<form onSubmit={FormSubmitHandler}
				method="post"
				className="form">
				<div className={styles.field}>
					<Label htmlFor={usernameId}>Username</Label>
					<Input id={usernameId}
						onChange={InputChangeHandler}
						name="username"
						disabled={loading}
						value={model.username.value}
						appearance="outline"
						aria-label="Username input" />
				</div>
				<div className={styles.field}>
					<Label htmlFor={passwordId}>Password</Label>
					<Input id={passwordId}
						appearance="outline"
						value={model.password.value}
						onChange={InputChangeHandler}
						name="password"
						disabled={loading}
						contentAfter={RenderPasswordSpyElement()}
						type={showPassword ? "text" : "password"}
						aria-label="Passwordd input" />
				</div>
				<div className={styles.field}>
					<Button appearance='primary' disabled={loading}>
						{loading ? <Spinner size="tiny" /> : "Login"}
					</Button>
				</div>
				<div className={styles.links}>
					<Button appearance='transparent'
						aria-label='Register link'
						onClick={() => navigate({
							pathname: AppRoutes.Auth.Register,
							search: search[0]?.toString()
						})}>
						Create Account
					</Button>
					<Divider vertical />
					<Button appearance='transparent'
						aria-label='Forggotten password link'
						onClick={() => navigate({
							pathname: AppRoutes.Auth.ForgottenPassword,
							search: search[0]?.toString()
						})}>
						Reset Password
					</Button>
				</div>
			</form>
		</div>
	</>)
}


