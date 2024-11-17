import { ChangeEvent, FormEvent, useEffect, useState } from 'react'
import { Input, Button, Tooltip, Spinner, makeStyles, tokens, Field, MessageBar, MessageBarBody } from '@fluentui/react-components'
import { usePageTitle } from '../../../hooks/page.hook'
import { BsLockFill, BsUnlockFill } from 'react-icons/bs'
import AppRoutes from '../../../strings/AppRoutes'
import { useNavigate } from 'react-router-dom'
import { useAuthContext } from '../../../contexts/auth.context'
import { ajax, AjaxError } from 'rxjs/ajax'
import { useHttp } from '../../../hooks/http.hook'
import { useAppSettings } from '../../../strings/AppSettings'
import { StatusCodes } from 'http-status-codes'
import { ITransactionResult } from '../../../core/transactions.core'
import { IAppUser } from '../../../types/identity'
import useSessionData from '../../../extensions/SessionData'
import { AuthSessionVars, AuthVerificationReasons } from './auth.strings'
import { from } from 'rxjs'
import { useIdentityManager } from '../../../hooks/userManager.hook'
import { ISendVerificationCodeResponse } from './auth.types'
import { ApiRoutes } from '../../../strings/ApiRoutes'

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
	const navigate = useNavigate()
	const styles = LoginStyles()
	const { setFormTitle, setFormMessage, username, user, loading, setLoading } = useAuthContext()
	const { headers } = useHttp()
	const { createApiPath } = useAppSettings()
	const session = useSessionData()
	const [showPassword, setShowPassword] = useState(false)
	const [error, setError] = useState<string | undefined>()
	const [model, setModel] = useState<IModel>({
		password: ""
	})
	const identityManager = useIdentityManager()


	function TogglePasswordHandler() {
		setShowPassword(!showPassword)
	}

	function InputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
		setModel({
			password: event.target.value,
			error: ""
		})
	}

	function handleFormSubmitEvent(event: FormEvent) {
		event.preventDefault()
		setLoading!(true)

		if (!model.password) {
			setModel(state => ({ ...state, error: "Password is required." }))
			setLoading!(false)
			return
		}

		ajax<ITransactionResult<IAppUser>>({
			url: createApiPath(ApiRoutes.Auth.Login),
			method: "POST",
			body: JSON.stringify({
				email: username,
				password: model.password
			}),
			headers: {
				[headers.contentType.name]: headers.contentType.values.Json
			}
		}).subscribe({
			next: (resp) => {
				if (resp.status === StatusCodes.OK) {
					const result = resp.response

					console.log(resp)

					if (result.succeeded) {
						console.log(result.model!)
						identityManager.signIn(result.model!)
						setLoading!(false)
						const returnUrl = session.get<string>(AuthSessionVars.ReturnUrl)
						session.remove(AuthSessionVars.User)
						session.remove(AuthSessionVars.Username)
						navigate(returnUrl ?? '/')
					} else {
						console.log(result.errors)
						from(result.errors).subscribe({
							next: (error) => {
								if (error.code == "Email") {
									setLoading!(false)
									navigate(AppRoutes.Auth.Username)
								}
								if (error.code == "Password") {
									setModel({
										password: "",
										error: error.description ?? "Incorrect password."
									})
								}
							},
							complete() {
								setLoading!(false)
							}
						})
					}
				}
			},
			error: (error) => {
				console.log(error)
			}
		})
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
		if (!user || !username) {
			navigate(AppRoutes.Auth.Username)
			return
		}

		setFormTitle!(`Welcome Back, ${user.firstName}`)
		setFormMessage!("")
	}, [setFormTitle, setFormMessage, user, navigate, username])

	function sendPasswordResetVerificationCode() {
		ajax<ITransactionResult<ISendVerificationCodeResponse>>({
			url: createApiPath(ApiRoutes.Auth.SendVerificationCode),
			method: "POST",
			body: JSON.stringify({
				email: username,
				reason: AuthVerificationReasons.PasswordReset
			}),
			headers: {
				[headers.contentType.name]: headers.contentType.values.Json
			}
		}).subscribe({
			next: (response) => {
				console.log(response)
				const data = response.response
				if (data && data.succeeded) {
					session.add(AuthSessionVars.VerificationHashString, data.model!.codeHashString)
					session.add(AuthSessionVars.VerificationReason, AuthVerificationReasons.PasswordReset)
					setLoading!(false)
					navigate(AppRoutes.Auth.VerifyEmail)
				}
			},
			error: (error: AjaxError) => {
				console.log(error)
				setError("Unable to sned verification code.")
				setLoading!(false)
			}
		})
	}

	useEffect(() => {
		if (error)
			setTimeout(() => setError(undefined), 5000)

	}, [error])

	return (<>
		<form onSubmit={handleFormSubmitEvent}
			method="post"
			className="form">
			{error ?
				<MessageBar intent='error'>
					<MessageBarBody>{error}</MessageBarBody>
				</MessageBar>
				: ""}
			<Field label="Password"
				validationMessage={model.error}
				validationState={model.error ? "error" : "none"}>
				<Input appearance="outline"
					value={model.password}
					onChange={InputChangeHandler}
					name="password"
					disabled={loading}
					contentAfter={RenderPasswordSpyElement()}
					type={showPassword ? "text" : "password"}
					aria-label="Passwordd input" />
			</Field>
			<Field className={styles.buttonWrapper}>
				<Button appearance='primary'
					size='large'
					type='submit'
					disabled={loading}>
					{loading ? <Spinner size="tiny" /> : "Login"}
				</Button>
			</Field>
			<div className={styles.links}>
				<Button appearance='transparent'
					aria-label='Forggotten password link'
					onClick={() => sendPasswordResetVerificationCode()}>
					Reset Password
				</Button>
			</div>
		</form>
	</>)
}


