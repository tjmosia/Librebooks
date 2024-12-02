import { ChangeEvent, FormEvent, useEffect, useState } from 'react'
import { Input, Button, Tooltip, Spinner, makeStyles, tokens, Field, MessageBar, MessageBarBody } from '@fluentui/react-components'
import { BsLockFill, BsUnlockFill } from 'react-icons/bs'
import AppRoutes from '../../../strings/AppRoutes'
import { useNavigate } from 'react-router-dom'
import { useAuthContext } from '../../../contexts/AuthContext'
import { ajax, AjaxError } from 'rxjs/ajax'
import { StatusCodes } from 'http-status-codes'
import { ITransactionResult } from '../../../core/extensions/TransactionTypes'
import { IAppUser } from '../../../types/identity'
import useSessionData from '../../../core/extensions/SessionData'
import { AuthSessionVars, AuthVerificationReasons } from './AuthStrings'
import { from } from 'rxjs'
import { ISendVerificationCodeResponse } from './AuthTypes'
import { useAppSettings, useHttp, usePageTitle } from '../../../hooks'
import useIdentityManager from '../../../hooks/IdentityManager'
import { ApiRoutes } from '../../../strings'
import { IFormField } from '../../../core/forms'
import { intent } from '../../../strings/ui'

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

interface ILoginModel {
	[key: string]: IFormField<string | undefined>
	password: IFormField<string | undefined>
}

interface IBadRequestErrors {
	[key: string]: string[] | undefined
	Password?: string[]
}

export default function LoginPage() {

	/*********************************************************************************************************************************
	 * SERVICES
	 *********************************************************************************************************************************/
	usePageTitle("Login")
	const navigate = useNavigate()
	const styles = LoginStyles()
	const { setFormTitle, setFormMessage, username, user, loading, setLoading, setAlert } = useAuthContext()
	const { headers } = useHttp()
	const { createApiPath } = useAppSettings()
	const session = useSessionData()
	const [showPassword, setShowPassword] = useState(false)
	const [error, setError] = useState<string | undefined>()
	const [model, setModel] = useState<ILoginModel>({
		password: {
			value: ""
		}
	})
	const identityManager = useIdentityManager()



	/*********************************************************************************************************************************
	 * METHODS
	 *********************************************************************************************************************************/
	function TogglePasswordHandler() {
		setShowPassword(!showPassword)
	}

	function InputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
		setModel({
			password: {
				value: event.target.value.replace(/[\s]/, ""),
			}
		})
	}

	function onSubmit(event: FormEvent) {
		event.preventDefault()
		setLoading!(true)

		if (!model.password) {
			setModel({
				password: {
					value: "",
					error: "Password is required."
				}
			})

			setLoading!(false)
			return
		}

		ajax<ITransactionResult<IAppUser>>({
			url: createApiPath(ApiRoutes.Auth.Login),
			withCredentials: true,
			method: "POST",
			body: JSON.stringify({
				email: username,
				password: model.password.value
			}),
			headers: {
				[headers.contentType.name]: headers.contentType.values.Json
			}
		}).subscribe({
			next: (resp) => {
				const result = resp.response
				if (result.succeeded) {
					identityManager.signIn(result.model!)
					session.remove(AuthSessionVars.User)
					session.remove(AuthSessionVars.Username)
					setLoading!(false)
					navigate(session.get<string>(AuthSessionVars.ReturnUrl, false) ?? AppRoutes.Home)
				} else {
					console.log(result.errors)
					from(result.errors).subscribe({
						next: (error) => {
							if (error.code == "Email") {
								setLoading!(false)
								navigate(AppRoutes.Auth.Username)
							}
							if (error.code == "Password") {
								setModel(model => ({
									password: {
										...model.password,
										error: error.description ?? "Incorrect password."
									}
								}))
							}
							setLoading!(false)
						}
					})
				}
			},
			error: (error: AjaxError) => {
				if (error.status === StatusCodes.BAD_REQUEST) {
					const data = error.response
					const errors = data.errors as IBadRequestErrors
					const updatedModel = { ...model }
					const errorKeys = Object.keys(errors)

					if (errorKeys.includes("Code") || errorKeys.includes("CodeHashString")) {
						setLoading!(false)
						navigate(AppRoutes.Auth.Username)
					}

					from(errorKeys)
						.subscribe(key => {
							if (key === "Email") {
								setLoading!(false)
								setAlert!({
									intent: intent.error,
									message: errors[key]![0]
								})
								navigate(AppRoutes.Auth.Username)
							}

							const _key = key[0].toLocaleLowerCase() + key.slice(1)
							try {
								updatedModel[_key].error = errors[key]![0]
							} catch {
								console.log(`Exception Occured. Failed to retrieve object with key ${_key}`)
							}
						})
					setModel(updatedModel)
				}
				setLoading!(false)
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
				setError("Unable to send verification code.")
				setLoading!(false)
			}
		})
	}

	/*********************************************************************************************************************************
	 * EFFECTS
	 *********************************************************************************************************************************/
	useEffect(() => {
		if (!user || !username) {
			navigate(AppRoutes.Auth.Username)
			return
		}

		setFormTitle!(`Welcome Back, ${user.firstName}`)
		setFormMessage!("")
	}, [setFormTitle, setFormMessage, user, navigate, username])

	useEffect(() => {
		if (error)
			setTimeout(() => setError(undefined), 5000)

	}, [error])

	return (<>
		<form onSubmit={onSubmit}
			method="post"
			className="form">
			{error ?
				<MessageBar intent='error'>
					<MessageBarBody>{error}</MessageBarBody>
				</MessageBar>
				: ""}
			<Field label="Password"
				validationMessage={model.password.error}
				validationState={model.password.error ? "error" : "none"}>
				<Input appearance="outline"
					value={model.password.value}
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


