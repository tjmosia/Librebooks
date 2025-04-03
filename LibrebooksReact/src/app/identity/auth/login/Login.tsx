import { ChangeEvent, createRef, FormEvent, useCallback, useContext, useEffect, useState } from 'react'
import IFormField from '../../../../core/forms/IFormField'
import './Login.scss'
import { Button, ButtonGroup, Callout, FormGroup, InputGroup } from '@blueprintjs/core'
import { AuthContext } from '../AuthContext'
import { apiRoutes, feedbackMessages, routes } from '../../../../values'
import { useNavigate } from 'react-router'
import useSessionData from '../../../../hooks/useSessionData'
import { useAppSettings } from '../../../../hooks'
import { ajax, AjaxError } from 'rxjs/ajax'
import { http } from '../../../../core/html'
import { ITransactionResult, ITransactionError } from '../../../../core/transactions'
import { IUser } from '../../../../core/identity'
import { StatusCodes } from 'http-status-codes'
import { AuthSessionVars } from '../AuthSessionVars'

interface ILoginModel {
    [key: string]: IFormField<string>
    email: IFormField<string>
    password: IFormField<string>
}

const initialModel: ILoginModel = {
    email: { value: '' },
    password: { value: '' }
}

export default function LoginPage() {
    const { setTitle, setSubTitle, loading, setLoading } = useContext(AuthContext)
    const navigate = useNavigate()
    const [model, setModel] = useState<ILoginModel>(initialModel)
    const [showPassword, setShowPassword] = useState<boolean>(false)
    const session = useSessionData()
    const [successMessage, setSuccessMessage] = useState(session.tryGet<string>(feedbackMessages.success))
    const emailInput = createRef<HTMLInputElement>()
    const passwordInput = createRef<HTMLInputElement>()
    const { getApiUrl } = useAppSettings()

    function onChange(event: ChangeEvent<HTMLInputElement>) {

        const { name, value } = event.target
        setModel({
            ...model,
            [name]: {
                value: value
            }
        })
    }

    const onSubmit = useCallback(async (event: FormEvent) => {
        event.preventDefault()

        if (!model.email.value)
            return emailInput.current?.focus()

        if (!model.password.value || model.password.value.length < 8)
            return passwordInput.current?.focus()

        ajax<ITransactionResult<IUser>>({
            url: getApiUrl(apiRoutes.auth.login),
            method: 'POST',
            body: JSON.stringify({
                email: model.email.value,
                password: model.password.value
            }),
            headers: {
                [http.headers.contentType]: http.mimeTypes.json,
            },
            responseType: 'json'
        }).subscribe({
            next: (response) => {
                if (response.status === 200) {
                    const data = response.response
                    if (!data.succeeded) {
                        console.log(data.errors)
                        data.errors.forEach(error => {
                            if (error.code == "Email" || error.code == "Password") {
                                setModel(model => ({ ...model, [error.code.toLocaleLowerCase()]: { ...model[error.code.toLocaleLowerCase()], error: error.description } }))
                            }
                        })
                    }
                }
            },
            error(error: AjaxError) {
                if (error.status === StatusCodes.BAD_REQUEST) {
                    if (error.response.length < 1)
                        return
                    let stateChanged = false
                    const _model = { ...model }
                    const errors = [...error.response]

                    errors.forEach((error: ITransactionError) => {
                        if (error.code.toLocaleLowerCase() == "email") {
                            stateChanged = true
                            _model.email.error = error.description
                        }
                    })

                    if (stateChanged)
                        setModel(_model)
                }
            }
        }).add(() => { setLoading(false) });

    }, [setLoading, emailInput, passwordInput, model, getApiUrl])

    useEffect(() => {
        setTitle!("Login")
        setSubTitle!("Login with your email and password.")
        session.remove(AuthSessionVars.verification)
    }, [setTitle, setSubTitle, session])

    useEffect(() => {
        const successMessage = session.tryGet<string>(feedbackMessages.success)
        if (successMessage) {
            setTimeout(() => {
                session.remove(feedbackMessages.success)
                setSuccessMessage(null)
            }, 5000);
        }
    }, [session])

    const renderSuccessMessage = useCallback((message: string) => {
        return <FormGroup>
            <Callout compact icon={false} intent='success'>
                {message}
            </Callout>
        </FormGroup>
    }, [])

    const handlePasswordFocus = useCallback(() => {
        if (!model.email.value)
            emailInput.current?.focus()
    }, [model, emailInput])

    return (
        <div className='animate__animated animate__fadeIn login-form-container'>
            <form onSubmit={onSubmit}>
                {
                    successMessage ? renderSuccessMessage(successMessage) : null
                }
                <FormGroup label='Email'
                    helperText={model.email.error}
                    intent={model.email.error ? 'danger' : 'none'}>
                    <InputGroup value={model.email.value}
                        type='email'
                        name='email'
                        leftIcon="envelope"
                        inputRef={emailInput}
                        disabled={loading}
                        onChange={onChange} />
                </FormGroup>
                <FormGroup label='Password'
                    helperText={model.password.error}
                    intent={model.password.error ? 'danger' : 'none'}>
                    <InputGroup value={model.password.value}
                        type={showPassword ? 'text' : 'password'}
                        onChange={onChange}
                        disabled={loading}
                        name='password'
                        leftIcon="key"
                        inputRef={passwordInput}
                        onFocus={handlePasswordFocus}
                        rightElement={
                            <Button size='small'
                                onClick={() => { setShowPassword(!showPassword) }}
                                icon={showPassword ? "unlock" : "lock"} />} />
                </FormGroup>
                <FormGroup>
                    <Button type='submit'
                        title='Login'
                        loading={loading} intent='primary' fill
                        text="Login" />
                </FormGroup>
                <FormGroup>
                    <ButtonGroup variant='outlined' fill>
                        <Button title='Reset your password.'
                            disabled={loading}
                            onClick={() => navigate(routes.auth.forgotPassword)}
                            text="Reset Password" />
                        <Button title='Create a new account.'
                            disabled={loading}
                            onClick={() => navigate(routes.auth.register)}
                            text="Create Account" />
                    </ButtonGroup>
                </FormGroup>
            </form>
        </div >
    )
}