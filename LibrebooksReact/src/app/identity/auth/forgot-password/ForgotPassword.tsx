import { ChangeEvent, FormEvent, useCallback, useContext, useEffect, useState } from 'react'
import IFormField from '../../../../core/forms/IFormField'
import { Button, FormGroup, Icon, InputGroup } from '@blueprintjs/core'
import { AuthContext } from '../AuthContext'
import { routes } from '../../../../values'
import { useNavigate } from 'react-router'

export default function ForgotPasswordPage() {
    const { setTitle, setSubTitle, loading, setLoading } = useContext(AuthContext)
    const navigate = useNavigate()
    const [email, setEmail] = useState<IFormField<string>>({
        value: ''
    })

    const handleInputChange = useCallback((event: ChangeEvent<HTMLInputElement>) => {
        setEmail({
            value: event.target.value
        })
    }, [])

    const validateModel = useCallback(() => {
        return false
    }, [])

    const handleSubmit = useCallback((event: FormEvent) => {
        event.preventDefault()
        setLoading(true)
        if (validateModel()) {
            setLoading(false)
            return
        } else {
            setLoading(false)
            navigate(routes.auth.resetPassword)
        }
    }, [setLoading, validateModel, navigate])

    useEffect(() => {
        setTitle!("Forgot Password")
        setSubTitle!("Enter your registered email address to continue.")
    }, [setTitle, setSubTitle])

    return (
        <div className='animate__animated animate__fadeIn'>
            <form onSubmit={handleSubmit}>
                <FormGroup label='Email'
                    helperText={email.error}
                    intent={email.error ? 'danger' : 'none'}>
                    <InputGroup value={email.value}
                        type='email'
                        leftElement={<Icon icon='envelope' />}
                        name='email'
                        disabled={loading}
                        onChange={handleInputChange}
                        placeholder='Email' />
                </FormGroup>
                <FormGroup>
                    <Button fill type='submit' loading={loading} intent='primary'>Send Verification Code</Button>
                </FormGroup>
                <FormGroup>
                    <Button variant='minimal' fill onClick={() => navigate(routes.auth.login)} disabled={loading}>Return to Login</Button>
                </FormGroup>
            </form>
        </div>
    )
}