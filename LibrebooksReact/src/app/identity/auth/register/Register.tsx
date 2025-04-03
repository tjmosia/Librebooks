import { ChangeEvent, FormEvent, useCallback, useContext, useEffect, useState } from 'react'
import IFormField from '../../../../core/forms/IFormField'
import './Register.scss'
import { Button, FormGroup, H6, Icon, InputGroup, Section, SectionCard } from '@blueprintjs/core'
import { AuthContext } from '../AuthContext'
import { routes } from '../../../../values'
import { useNavigate } from 'react-router'
import { useAppSettings, usePasswordValidator } from '../../../../hooks'
import { ajax, AjaxError } from 'rxjs/ajax'
import { http } from '../../../../core/html'
import { StatusCodes } from 'http-status-codes'
import { ITransactionResult } from '../../../../core/transactions'
import { useLocation } from 'react-router'
import useSessionData from '../../../../hooks/useSessionData'
import { AuthSessionVars } from '../AuthSessionVars'

interface ILoginModel {
    [key: string]: IFormField<string>
    firstName: IFormField<string>
    lastName: IFormField<string>
    email: IFormField<string>
    password: IFormField<string>
    phoneNumber: IFormField<string>
}
const initialModel: ILoginModel = {
    email: { value: 'tjmosia@outlook.com' },
    firstName: { value: 'Thabo' },
    lastName: { value: 'Mosia' },
    password: { value: 'Th@bs365' },
    phoneNumber: { value: '0737748589' }
}

export default function RegisterPage() {
    const navigate = useNavigate()
    const location = useLocation()
    const session = useSessionData()
    const passwordChecker = usePasswordValidator()
    const { setTitle, setSubTitle, loading, setLoading } = useContext(AuthContext)
    const [model, setModel] = useState<ILoginModel>(initialModel)
    const [showPassword, setShowPassword] = useState<boolean>(false)
    const [showPasswordRequirements, setShowPasswordReuirements] = useState(false)
    const { getApiUrl } = useAppSettings()
    const updateModel = useCallback((name: string, value: string) => {
        setModel(model => ({
            ...model,
            [name]: {
                value: value
            }
        }))
    }, [])

    const onChange = useCallback((event: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = event.target
        updateModel(name, value)
    }, [updateModel])

    useEffect(() => {
        setTitle!("Register")
        setSubTitle!("Complete the form below to create your account.")
    }, [setTitle, setSubTitle])

    const renderPasswordRequirementCheck = useCallback((value: string) => {
        const _passwordRequirementItemMark = (value: boolean) => {
            return !value ? <Icon icon="small-cross" intent='danger' /> : <Icon icon="tick" intent='success' className='mr-3' />
        }

        return (
            <Section compact>
                <SectionCard >
                    <H6 className='mb-1'>Password Requirements</H6>
                    <p className='m-0 p-0 mb-1'>Your password must meet the following requirements:</p>
                    <ul className='list-unstyled m-0 p-0'>
                        <li>{_passwordRequirementItemMark(passwordChecker.hasMinLength(value, 8))} Has min 8 characters.</li>
                        <li>{_passwordRequirementItemMark(passwordChecker.hasUpperCase(value))} Has an uppercase letter.</li>
                        <li>{_passwordRequirementItemMark(passwordChecker.hasLowerCase(value))} Has a lower case letter.</li>
                        <li>{_passwordRequirementItemMark(passwordChecker.hasDigit(value))} Has a digit.</li>
                        <li>{_passwordRequirementItemMark(passwordChecker.hasNonAlphaNumeric(value))} Has a symbol.</li>
                    </ul>
                </SectionCard>
            </Section>
        )
    }, [passwordChecker])

    const validateModel = useCallback(() => {
        const _model = { ...model }
        if (!model.firstName.value)
            _model.firstName.error = 'First name is required.'

        if (!model.lastName.value)
            _model.lastName.error = 'Last name is required.'

        if (!model.email.value)
            _model.email.error = 'Email is required.'

        if (!model.phoneNumber.value)
            _model.phoneNumber.error = 'Mobile is required.'

        if (!model.password.value)
            _model.password.error = 'Password is required.'

        if (model.password.value && !passwordChecker.isValid(model.password.value))
            _model.password.error = 'Password does not meet the requirements.'

        setModel(_model)

        return !(_model.email.error ||
            _model.phoneNumber.error ||
            _model.firstName.error ||
            _model.lastName.error ||
            _model.password.error)
    }, [model, passwordChecker])

    const sendEmailVerificationCode = useCallback(() => {
        ajax<ITransactionResult<null>>({
            url: getApiUrl("/verifications/send"),
            method: "POST",
            body: JSON.stringify({
                email: model.email.value,
                requestUri: location.pathname
            }),
            headers: {
                [http.headers.contentType]: http.mimeTypes.json
            }
        }).subscribe({
            next(response) {
                if (response.status !== StatusCodes.OK)
                    return

                if (response.response.succeeded) {
                    setLoading(false)
                    session.trySet<{ subject: string, requestUri: string }>(AuthSessionVars.verificationSource, {
                        subject: model.email.value!,
                        requestUri: location.pathname
                    })
                    navigate(routes.auth.verifyEmail)
                }
            },
        })
    }, [getApiUrl, location.pathname, model.email.value, navigate, setLoading, session])

    const onSubmit = useCallback((event: FormEvent) => {
        event.preventDefault()

        setLoading(true)

        if (!validateModel()) {
            return setLoading(false)
        }

        ajax<ITransactionResult<null>>({
            url: getApiUrl(routes.auth.register),
            method: "POST",
            body: JSON.stringify({
                firstName: model.firstName.value,
                lastName: model.lastName.value,
                email: model.email.value,
                password: model.password.value,
                mobile: model.phoneNumber.value
            }),
            headers: {
                [http.headers.contentType]: http.mimeTypes.json
            }
        }).subscribe({
            next(response) {
                if (response.status != StatusCodes.OK) {
                    return
                }
                const data = response.response

                if (data.succeeded) {
                    setLoading(false)
                    sendEmailVerificationCode()
                } else if (data.errors.length > 0) {
                    const _model = { ...model }
                    data.errors.forEach(error => {
                        switch (error.code) {
                            case "Email":
                            case "Password":
                            case "Mobile":
                                _model[error.code.toLocaleLowerCase()].error = error.description
                                break;
                        }
                    })
                    setModel(_model)
                }
            }, error(error: AjaxError) {
                console.log(error.status)
            },
        }).add(() => setLoading(false))
    }, [setLoading, validateModel, getApiUrl, model, sendEmailVerificationCode]);

    return (
        <div className='animate__animated animate__fadeIn register-form-container'>
            <form onSubmit={onSubmit}>
                <FormGroup label='First Name'
                    helperText={model.firstName.error}
                    intent={model.firstName.error ? 'danger' : 'none'}>
                    <InputGroup value={model.firstName.value}
                        intent={model.firstName.error ? 'danger' : 'none'}
                        type='text'
                        placeholder='First Name'
                        name='firstName'
                        disabled={loading}
                        onChange={onChange} />
                </FormGroup>
                <FormGroup label='Last Name'
                    helperText={model.lastName.error}
                    intent={model.lastName.error ? 'danger' : 'none'}>
                    <InputGroup value={model.lastName.value}
                        type='text'
                        intent={model.lastName.error ? 'danger' : 'none'}
                        name='lastName'
                        placeholder='Last Name'
                        disabled={loading}
                        onChange={onChange} />
                </FormGroup>
                <FormGroup label='Email'
                    helperText={model.email.error}
                    intent={model.email.error ? 'danger' : 'none'}>
                    <InputGroup value={model.email.value}
                        intent={model.email.error ? 'danger' : 'none'}
                        type='email'
                        placeholder='Email Address'
                        name='email'
                        disabled={loading}
                        onChange={onChange} />
                </FormGroup>
                <FormGroup label='Mobile'
                    helperText={model.phoneNumber.error}
                    intent={model.phoneNumber.error ? 'danger' : 'none'}>
                    <InputGroup value={model.phoneNumber.value}
                        intent={model.phoneNumber.error ? 'danger' : 'none'}
                        type='tel'
                        name='phoneNumber'
                        placeholder='Mobile Number'
                        disabled={loading}
                        onChange={onChange} />
                </FormGroup>
                <FormGroup label='Password'
                    helperText={model.password.error}
                    intent={model.password.error ? 'danger' : 'none'}>
                    <InputGroup value={model.password.value}
                        type={showPassword ? 'text' : 'password'}
                        intent={model.password.error ? 'danger' : 'none'}
                        onChange={onChange}
                        name='password'
                        onFocus={() => setShowPasswordReuirements(true)}
                        onBlur={() => setShowPasswordReuirements(false)}
                        disabled={loading}
                        rightElement={<Button size='small' onClick={() => { setShowPassword(!showPassword) }} icon={showPassword ? "unlock" : "lock"} />}
                        placeholder='Password' />
                </FormGroup>
                {showPasswordRequirements ? <FormGroup className='animate__animated animate__fadeIn' >
                    {renderPasswordRequirementCheck(model.password.value ?? '')}
                </FormGroup> : ""}
                <FormGroup>
                    <Button loading={loading} type='submit' intent='primary' fill>Register</Button>
                </FormGroup>
                <FormGroup>
                    <Button fill variant='outlined' disabled={loading} onClick={() => navigate(routes.auth.login)}>Login</Button>
                </FormGroup>
            </form>
        </div>
    )
}