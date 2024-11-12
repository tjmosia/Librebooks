import { ChangeEvent, FormEvent, useEffect, useState } from 'react'
import { Button, Divider, Field, Input, Select, SelectOnChangeData, Spinner, makeStyles, tokens } from '@fluentui/react-components'
import { usePageTitle } from '../../../hooks/page.hook'
import CreatePasswordComponent from './components/password.components'
import { useAuthContext } from './auth.context'
import { DatePicker } from '@fluentui/react-datepicker-compat'
import { BsCalendarPlus } from 'react-icons/bs'
import { useValidators } from '../../../hooks/validators.hook'
import { IFormField } from '../../../core/forms'
import { useFormUtils } from '../../../core/forms/forms.hook'

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
	const { loading, setLoading, setFormTitle } = useAuthContext()
	const [model, setModel] = useState<IRegisterPageModel>(emptyModel)
	const { passwordValidator } = useValidators()
	const { fieldErrors } = useFormUtils()

	function InputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
		const { value, name } = event.target

		if (name.endsWith("word")) {
			const newVal = value.replace(/[\s]/, "")

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
			setModel(state => ({
				...state,
				[name]: {
					value
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

	function FormSubmitHandler(event: FormEvent) {
		event.preventDefault()
		setLoading!(true)
		alert("yes")
		setTimeout(() => {
			if (!validateModel()) {
				setLoading!(false)
				return;
			}
			setLoading!(false)
		}, 1000)
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
	}, [setFormTitle])

	return (<>
		<form onSubmit={FormSubmitHandler}
			method="post">
			<Divider appearance='strong' className={styles.divider}>Personal Information</Divider>
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
							onChange={InputChangeHandler}
						/>
					</Field>
				</div>
				<div className='col-6 ps-1'>
					<Field label="First name"
						className={styles.field}
						validationState={model.lastName.error ? "error" : "none"}
						validationMessage={model.lastName.error}>
						<Input
							value={model.lastName.value}
							name="lastName"
							disabled={loading}
							onChange={InputChangeHandler}
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
			<Divider appearance='strong' className={styles.divider}>Create your Password</Divider>
			<CreatePasswordComponent
				inputChangeHandler={InputChangeHandler}
				password={model.password}
				confirmPassword={model.confirmPassword} />

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
	}
})
