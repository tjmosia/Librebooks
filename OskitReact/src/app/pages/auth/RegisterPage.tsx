import { ChangeEvent, FormEvent, useEffect, useMemo, useState } from 'react'
import { Button, Caption1Strong, Divider, Field, Input, Link, Select, SelectOnChangeData, Spinner, makeStyles, tokens } from '@fluentui/react-components'
import CreatePasswordComponent from './components/CreatePasswordComponent'
import { useAuthContext } from '../../../contexts/auth.context'
import { DatePicker } from '@fluentui/react-datepicker-compat'
import { BsCalendarPlus } from 'react-icons/bs'
import { IFormField } from '../../../core/forms'
import { useFormUtils } from '../../../core/forms/forms.hook'
import useSessionData from '../../../extensions/SessionData'
import { AuthSessionVars } from './AuthStrings'
import { useNavigate } from 'react-router'

import AppRoutes from '../../../strings/AppRoutes'
import { IAppUser } from '../../../types/identity'
import { ITransactionResult } from '../../../core/Transactions'
import { StatusCodes } from 'http-status-codes'
import { ajax, AjaxError, AjaxResponse } from 'rxjs/ajax'
import { from } from 'rxjs'
import { useAppSettings, useHttp, usePageTitle, useValidators } from '../../../hooks'
import useIdentityManager from '../../../hooks/IdentityManager'
import { Intent } from '../../../strings/ui'

interface IBadRequestErrors {
	[key: string]: string[] | undefined
	Birthday?: string[]
	Code?: string[]
	CodeHashString?: string[]
	Email?: string[]
	FirstName?: string[]
	Gender?: string[]
	LastName?: string[]
	Password?: string[]
}

interface IRegisterPageModel {
	[key: string]: IFormField<string | undefined> | IFormField<Date | undefined | null>
	firstName: IFormField<string>,
	lastName: IFormField<string | undefined>,
	confirmPassword: IFormField<string | undefined>,
	gender: IFormField<string | undefined>,
	password: IFormField<string | undefined>,
	birthday: IFormField<Date | undefined | null>
}

export default function RegisterPage() {
	usePageTitle("Register")
	const styles = LoginStyles()
	const navigate = useNavigate()
	const { loading, setLoading, setFormTitle, setFormMessage, username, setAlert } = useAuthContext()
	const { passwordValidator } = useValidators()
	const { fieldErrors } = useFormUtils()
	const { createApiPath } = useAppSettings()
	const session = useSessionData()
	const { headers } = useHttp()
	const identityManager = useIdentityManager()

	const verification = useMemo(() => ({
		code: session.get<string>(AuthSessionVars.VerificationCode),
		codeHashString: session.get<string>(AuthSessionVars.VerificationHashString),
		loaded() {
			return this.code && this.codeHashString
		}
	}), [session])

	const [model, setModel] = useState<IRegisterPageModel>(emptyModel)

	function handleFormSubmitEvent(event: FormEvent) {
		event.preventDefault()
		setLoading!(true)

		if (!validateModel()) {
			setLoading!(false)
			return;
		}

		ajax<ITransactionResult<IAppUser>>({
			url: createApiPath("/auth/register"),
			method: "POST",
			body: JSON.stringify({
				email: username,
				code: verification.code,
				codeHashString: verification.codeHashString,
				firstName: model.firstName.value,
				lastName: model.lastName.value,
				birthday: model.birthday?.value,
				gender: model.gender.value,
				password: model.password.value
			}),
			headers: {
				[headers.contentType.name]: headers.contentType.values.Json
			}
		}).subscribe({
			next: (response: AjaxResponse<ITransactionResult<IAppUser>>) => {
				console.log(response)
				if (response.status === StatusCodes.OK) {
					const data = response.response
					if (data.succeeded) {
						setLoading!(false)
						session.remove(AuthSessionVars.VerificationCode)
						session.remove(AuthSessionVars.VerificationReason)
						session.remove(AuthSessionVars.VerificationHashString)
						identityManager.signIn(data.model!)
						navigate(session.get<string>(AuthSessionVars.ReturnUrl) ?? '/')
					}
				}
				setLoading!(false)
			},
			error: (error: AjaxError) => {
				if (error.status === StatusCodes.BAD_REQUEST) {
					const data = error.response()
					const errors = data.errors as IBadRequestErrors
					const updatedModel = { ...model }
					const errorKeys = Object.keys(errors)

					if (errorKeys.includes("Code") || errorKeys.includes("CodeHashString")) {
						setLoading!(false)
						navigate(AppRoutes.Auth.Username)
					}

					from(errorKeys)
						.subscribe(key => {
							if (!Object.keys(updatedModel).includes(key))
								console.log("incorrect key")
							const _key = key[0].toLocaleLowerCase() + key.slice(1)
							try {
								updatedModel[_key].error = errors[key]![0]
							} catch {
								console.log(`Exception Occured. Failed to retrieve object with key ${_key}`)
							}
						})
					setModel(updatedModel)
				}

				setAlert!({
					intent: Intent.error,
					message: "Unable to register user. Please try again."
				})

				setLoading!(false)
			}
		})
	}

	function inputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
		const { value, name } = event.target

		const newVal = value.replace(/[\s]/, "")

		if (name.endsWith("word")) {
			if (newVal != model[name].value) {
				setModel(state => {
					const newModel = {
						...state,
						[name]: {
							value: newVal
						} as IFormField<string>
					}
					if (name == "password" && model.confirmPassword.value)
						newModel.confirmPassword = { value: "" } as IFormField<string>

					if (name == "confirmPassword") {
						if (!model.password.value) {
							newModel.password.error = "Password is requred."
							newModel.confirmPassword.value = ""
						}
						else {
							if (newVal !== model.password.value)
								newModel.confirmPassword.error = "Password do not match."
							else if (value == model.password.value && model.confirmPassword.error)
								newModel.confirmPassword.error = undefined
						}
					}

					return newModel
				})
			}
		} else {
			if (model[name].value != newVal)
				setModel(state => ({
					...state,
					[name]: {
						value: newVal
					} as IFormField<string>
				}))
		}
	}

	function validateModel() {
		let valid = true;
		const validatedModel = { ...model }

		if (!model.firstName.value) {
			validatedModel.firstName.error = fieldErrors.required("First name")
			valid = false;
		}
		if (!model.lastName.value) {
			validatedModel.lastName.error = fieldErrors.required("Last name")
			valid = false;
		}
		if (!model.password.value) {
			validatedModel.password.error = fieldErrors.required("Password")
			valid = false;
		} else if (!passwordValidator.validate(model.password.value)) {
			validatedModel.password.error = fieldErrors.passwordWeak
			valid = false;
		}
		if (!model.birthday.value) {
			validatedModel.birthday.error = fieldErrors.required("Birthday")
			valid = false;
		}
		if (!model.gender.value) {
			validatedModel.gender.error = fieldErrors.required("Gender")
			valid = false;
		}

		if (model.password.value && model.confirmPassword)
			if (model.password.value !== model.confirmPassword.value) {
				validatedModel.confirmPassword.error = fieldErrors.passwordMismatch
			}

		if (!valid)
			setModel(validatedModel)

		return valid
	}

	function SelectChangeEventHandler(event: ChangeEvent<HTMLSelectElement>, data: SelectOnChangeData) {
		setModel(state => ({
			...state,
			gender: {
				value: data.value,
				error: ""
			}
		}))
	}

	function DateSelectEventHandler(date: typeof model.birthday.value) {
		setModel(state => ({
			...state,
			birthday: {
				value: date,
				error: ""
			}
		}))
	}

	function onFormatDate(date?: Date) {
		return !date ? "" : (date.getFullYear()) + "/" + (date.getMonth() + 1) + "/" + date.getDate()
	};

	useEffect(() => {
		setFormTitle!("Create your account")
		setFormMessage!("")
		if (!verification.loaded.call(verification) || !username)
			navigate(AppRoutes.Auth.Username)
	}, [setFormTitle, setFormMessage, username, verification, navigate])

	return (<>
		<form onSubmit={handleFormSubmitEvent}
			method="post">
			<Divider appearance='subtle' className={styles.divider}>Personal Information</Divider>
			<div className='row'>
				<div className='col-6 pe-1'>
					<Field label="First name"
						className={styles.field}
						validationState={model.firstName.error ? "error" : "none"}
						validationMessage={model.firstName.error}>
						<Input
							value={model.firstName.value}
							name="firstName"
							disabled={loading}
							onChange={inputChangeHandler}
						/>
					</Field>
				</div>
				<div className='col-6 ps-1'>
					<Field label="Last name"
						className={styles.field}
						validationState={model.lastName.error ? "error" : "none"}
						validationMessage={model.lastName.error}>
						<Input
							value={model.lastName.value}
							name="lastName"
							disabled={loading}
							onChange={inputChangeHandler}
						/>
					</Field>
				</div>
			</div>
			<div className='row'>
				<div className='col-6 pe-1'>
					<Field label="Birthday"
						className={styles.field}
						validationState={model.birthday.error ? "error" : "none"}
						validationMessage={model.birthday.error}>
						<DatePicker
							className={styles.dateControl}
							required
							onSelectDate={DateSelectEventHandler}
							formatDate={onFormatDate}
							type='text'
							name='birthday'
							disabled={loading}
							contentAfter={<BsCalendarPlus size={16} />}
							value={model.birthday.value ? new Date(model.birthday.value) : null}
						/>
					</Field>
				</div>
				<div className='col-6 ps-1'>
					<Field label="Gender"
						className={styles.field}
						validationState={model.gender.error ? "error" : "none"}
						validationMessage={model.gender.error}>
						<Select name='gender'
							disabled={loading}
							value={model.gender.value}
							onChange={SelectChangeEventHandler}>
							<option value="" hidden></option>
							<option value="Male">Male</option>
							<option value="Female">Female</option>
						</Select>
					</Field>
				</div>
			</div>
			<Divider appearance='subtle' className={styles.divider}>Create your Password</Divider>
			<CreatePasswordComponent
				inputChangeHandler={inputChangeHandler}
				password={model.password}
				confirmPassword={model.confirmPassword} />
			<div className={styles.terms}>
				<Caption1Strong>By clicking join below, you accept our <Link href='asdasd' target='_blank'>terms of use, privary, and cookie policy.</Link></Caption1Strong>
			</div>

			<Button appearance='primary'
				type='submit'
				disabled={loading}
				className={styles.submitButton}
				size='large'>
				{loading ? <Spinner size="tiny" /> : "Join"}
			</Button>
		</form>
	</>)
}

const emptyModel: IRegisterPageModel = {
	confirmPassword: { value: "" },
	password: { value: "" },
	firstName: { value: "" },
	lastName: { value: "" },
	birthday: { value: undefined },
	gender: { value: "" },
}

const LoginStyles = makeStyles({
	wrapper: {
		display: "flex",
		flexDirection: "column"
	},
	field: {
		display: "flex",
		flexDirection: "column",
		marginBottom: `${tokens.spacingVerticalM}`,
		width: "100%"
	},
	title: {
		marginBottom: tokens.spacingVerticalM
	},
	dateControl: {
		width: "100%"
	},
	submitButton: {
		marginTop: tokens.spacingVerticalL,
		width: "100%"
	},
	divider: {
		margin: `${tokens.spacingHorizontalL} 0`
	},
	terms: {
		margin: `${tokens.spacingVerticalL} 0`
	}
})
