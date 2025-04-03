import { ChangeEvent, createRef, FormEvent, useCallback, useContext, useEffect, useState } from 'react'
import IFormField from '../../../../core/forms/IFormField'
import { Button, ButtonGroup, FormGroup, InputGroup } from '@blueprintjs/core'
import { AuthContext } from '../AuthContext'
import { routes } from '../../../../values'
import { useNavigate } from 'react-router'
import useSessionData from '../../../../hooks/useSessionData'
import './VerifyEmail.scss'
import { AuthSessionVars } from '../AuthSessionVars'
import { ajax } from 'rxjs/ajax'
import { useAppSettings } from '../../../../hooks'


export interface IVerificationData {
    subject: string
    requestUri: string
}

export default function VerifyEmailPage() {
    const { setTitle, setSubTitle, loading, setLoading } = useContext(AuthContext)
    const [sending, setSending] = useState<boolean>(false)
    const [sent, setSent] = useState<boolean>(false)
    const navigate = useNavigate()
    const { getApiUrl } = useAppSettings()
    const session = useSessionData()
    const [code, setCode] = useState<IFormField<string>>({ value: '' })
    const [sourcePath] = useState(session.tryGet<string>(AuthSessionVars.verificationSource));
    const [verification] = useState(session.tryGet<IVerificationData>(AuthSessionVars.verification));
    const codeInputElement = createRef<HTMLInputElement>()

    const onChange = useCallback((event: ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value
        setCode(({ value: value.replace(/[^0-9]/g, "") }))
    }, [])

    useEffect(() => {
        setTitle!("Verify your email")
        setSubTitle!("Enter the verification code sent to your email.")
    }, [setTitle, setSubTitle])

    useEffect(() => {
        if (sent) {
            setTimeout(() => {
                setSent(false)
            }, 5000);
        }
    }, [sent])

    const validateModel = useCallback(() => {
        if (!code.value || code.value.length !== 6) {
            setCode(currentCode => ({
                ...currentCode,
                error: "Please check your code."
            }))
            codeInputElement.current?.focus()
            return false
        }
        return true
    }, [code, codeInputElement])

    const onSubmit = useCallback((event: FormEvent) => {
        event.preventDefault()
        setLoading(true)


        if (!validateModel())
            return setLoading(false)

        ajax({
            url: getApiUrl(routes.auth.verifyEmail),
            method: "POST",
            body: JSON.stringify({
                email: verification?.subject,
                code: code.value,
                requestUri: verification?.requestUri
            })
        })


    }, [setLoading, validateModel, getApiUrl, verification, code])


    const resendVerificationCode = useCallback(() => {
        setSending(true)
        setTimeout(() => {
            setSending(false)
            setSent(true)
        }, 2000);
    }, [])

    const renderFormMessage = useCallback(() => {
        console.log(sourcePath)
        switch (sourcePath) {
            case routes.auth.register:
                return "Your account was successfully created. Verify your email to proceed to login."
            case routes.auth.login:
                return "Verify your email to proceed to login."
        }
    }, [sourcePath])

    useEffect(() => {
        if (!verification || !verification.requestUri || !verification.subject) {
            session.remove(AuthSessionVars.verification)
            navigate(routes.auth.login)
        }
    }, [verification, navigate, session])

    return (
        <div className='animate__animated animate__fadeIn'>
            <form onSubmit={onSubmit}>
                <FormGroup>
                    {renderFormMessage()}
                </FormGroup>
                <FormGroup label='Verification Code'
                    contentClassName='optInputs'
                    helperText={code.error}
                    intent={code.error ? 'danger' : 'none'}>
                    <InputGroup value={code.value}
                        type='code'
                        maxLength={6}
                        inputClassName='otp-code-input'
                        size='large'
                        placeholder='------'
                        fill
                        inputRef={codeInputElement}
                        disabled={loading}
                        onChange={onChange} />
                </FormGroup>
                <FormGroup>
                    <Button fill intent='primary' type="submit" disabled={code.value!.length < 6}>Verify</Button>
                </FormGroup>
                <FormGroup>
                    <ButtonGroup fill variant='outlined'>
                        <Button alignText='start' icon="arrow-left"
                            fill
                            onClick={() =>
                                navigate(sourcePath ?? routes.auth.login)}
                            disabled={loading}
                            text="Back" />
                        <Button fill
                            loading={sending}
                            disabled={sending || loading}
                            intent={sent ? "success" : "none"}
                            onClick={resendVerificationCode}
                            endIcon={sent ? "tick-circle" : "refresh"}
                            text="Resend Code" />
                    </ButtonGroup>
                </FormGroup>
            </form>
        </div >
    )
}