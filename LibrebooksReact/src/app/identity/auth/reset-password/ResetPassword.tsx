import { ChangeEvent, createRef, FormEvent, KeyboardEvent, RefObject, useCallback, useContext, useEffect, useState } from 'react'
import IFormField from '../../../../core/forms/IFormField'
import { Button, Callout, FormGroup, H6, Icon, InputGroup, Section, SectionCard } from '@blueprintjs/core'
import { AuthContext } from '../AuthContext'
import { feedbackMessages, routes } from '../../../../values'
import { useNavigate } from 'react-router'
import { usePasswordValidator } from '../../../../hooks'
import './ResetPassword.scss'
import useSessionData from '../../../../hooks/useSessionData'

const otpCodeInputElementRefs = [
    ...(function () {
        const inputs = []
        for (let i = 0; i < 6; i++) {
            inputs.push(createRef<HTMLInputElement>())
        }
        return inputs
    })()
]

export default function ResetPasswordPage() {
    const { setTitle, setSubTitle, loading, setLoading } = useContext(AuthContext)
    const [sending, setSending] = useState<boolean>(false)
    const [sent, setSent] = useState<boolean>(false)
    const passwordChecker = usePasswordValidator()
    const navigate = useNavigate()
    const session = useSessionData()

    const [password, setPassword] = useState<IFormField<string>>({
        value: ''
    })

    const [code, setCode] = useState<IFormField<string>>({ value: '' })
    const codeInputElements = otpCodeInputElementRefs

    const [showPassword, setShowPassword] = useState<boolean>(false)
    const passwordInput = createRef<HTMLInputElement>();

    const onFocus = useCallback(() => {
        codeInputElements[code.value!.length !== 0 ? code.value!.length : 0].current?.focus()
    }, [codeInputElements, code.value])

    const onChange = useCallback(() => {
        setCode(({ value: codeInputElements.map(input => input.current!.value).join('') }))
    }, [codeInputElements])

    const onKeyDown = useCallback((event: KeyboardEvent<HTMLInputElement>) => {
        const target = event.target as HTMLInputElement
        if (event.key === 'Backspace' || event.key === 'Delete') {
            if (target.value.length === 0) {
                setCode(code => ({ value: code.value!.substring(0, code.value!.length - 1) }))
            }
        }
    }, [])

    const renderOtpInputs = useCallback((otpInputRefs: RefObject<HTMLInputElement>[], modelValue: string, loading: boolean) => {
        return otpInputRefs.map((input, index) => {
            return (
                <InputGroup key={index}
                    value={modelValue![index] ?? ""}
                    type='code'
                    data-item={index}
                    maxLength={1}
                    inputClassName='otp-code-input'
                    inputRef={input}
                    size='large'
                    max={1}
                    onKeyDown={onKeyDown}
                    onFocus={onFocus}
                    disabled={loading}
                    onChange={onChange} />
            )
        })
    }, [onChange, onFocus, onKeyDown])


    const handleInputChange = useCallback((event: ChangeEvent<HTMLInputElement>) => {
        const { value, name } = event.target
        if (name === "password") {
            setPassword({
                value
            })
        }

        if (name === "code") {

            setCode({
                value: value.replace(/[^0-9]/, "")
            })
        }
    }, [])

    useEffect(() => {
        console.log(code.value?.length)
        if (code.value!.length < 7)
            codeInputElements[code.value!.length]?.current?.focus()
    }, [code.value, codeInputElements])

    useEffect(() => {
        if (code.value?.length === 6)
            passwordInput?.current?.focus()
    }, [code.value, passwordInput])

    const validateModel = useCallback(() => {
        return false
    }, [])

    const handleSubmit = useCallback((event: FormEvent) => {
        event.preventDefault()
        setLoading(true)
        if (!validateModel()) {
            setLoading(false)
            session.trySet<string>(feedbackMessages.success, "Your password was successfully reset.")
            navigate(routes.auth.login)
            return
        }
    }, [setLoading, validateModel, navigate, session])

    useEffect(() => {
        setTitle!("Reset Password")
        //setSubTitle!("Check your mailbox for the verification code.")
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

    // const resendVerificationCode = useCallback(() => {
    //     setSending(true)
    //     setTimeout(() => {
    //         setSending(false)
    //         setSent(true)
    //     }, 3000);
    // }, [])

    useEffect(() => {
        if (sent) {
            setTimeout(() => {
                setSent(false)
            }, 5000);
        }
    }, [sent])

    return (
        <div className='animate__animated animate__fadeIn'>
            <form onSubmit={handleSubmit}>
                <FormGroup>
                    <Callout compact>Check your inbox for the verification code and enter it below to reset your password.</Callout>
                </FormGroup>
                <FormGroup label='Verification Code'
                    contentClassName='optInputs'
                    helperText={code.error}
                    intent={code.error ? 'danger' : 'none'}>
                    {renderOtpInputs(codeInputElements, code.value!, loading)}
                </FormGroup>
                <FormGroup contentClassName='d-flex flex-row justify-content-end w-100 '>
                    <Button size='small' fill endIcon="refresh" variant='minimal'>Resend Code</Button>
                </FormGroup>
                <FormGroup label='Password'
                    intent={password.error ? 'danger' : 'none'}>
                    <InputGroup value={password.value}
                        type={showPassword ? 'text' : 'password'}
                        onChange={handleInputChange}
                        name='password'
                        disabled={loading}
                        rightElement={
                            <Button size='small'
                                onClick={() => { setShowPassword(!showPassword) }}
                                icon={showPassword ? "unlock" : "lock"} />}
                        placeholder='Password' />
                </FormGroup>
                <FormGroup>
                    {renderPasswordRequirementCheck(password.value ?? '')}
                </FormGroup>
                <FormGroup>
                    <Button fill type='submit' loading={loading} disabled={sending || loading} intent='primary'>Reset Password</Button>
                </FormGroup>
                <Button variant='minimal' fill onClick={() => navigate(routes.auth.login)} disabled={loading}>Return to Login</Button>
            </form>
        </div>
    )
}