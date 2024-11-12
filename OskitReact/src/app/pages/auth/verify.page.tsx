import { ChangeEvent, createRef, FormEvent, useEffect, useState } from 'react'
import { Input, useId, Button, makeStyles, tokens, Field } from '@fluentui/react-components'
import { usePageTitle } from '../../../hooks/page.hook'
import AppRoutes from '../../../strings/AppRoutes'
import { useNavigate, useSearchParams } from 'react-router-dom'
import { useAuthContext } from './auth.context'

const VerifyEmailPageStyles = makeStyles({
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
    otpInput: {
        letterSpacing: tokens.spacingHorizontalXXL,
        textAlign: "center",
    }
})

interface IModel {
    otp: string
    error?: string
}

export default function VerifyEmailPage() {
    usePageTitle("Login")
    const tokenId = useId("username")
    const navigate = useNavigate()
    const search = useSearchParams()
    const styles = VerifyEmailPageStyles()
    const { setFormTitle, setFormMessage, loading, setLoading } = useAuthContext()
    const form = createRef<HTMLFormElement>()
    const [model, setModel] = useState<IModel>({
        otp: ""
    })

    function InputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
        setModel({
            otp: event.target.value
        })
    }

    function FormSubmitHandler(event: FormEvent) {
        event.preventDefault()
        setLoading!(true)

        setTimeout(() => {
            setLoading!(false)
            setModel({ otp: "" })
            ContinueNextStep()
        }, 2000)
    }

    function ResendVerificationCode() {

    }

    useEffect(() => {
        setFormTitle!("Verify Your Email")
        setFormMessage!("We've sent a verification OTP to your email. Please enter the OTP below:")
    })

    useEffect(() => {
        if (model.otp.length == 8)
            form.current?.requestSubmit()
    }, [model.otp, form])

    function ContinueNextStep() {
        navigate({
            pathname: AppRoutes.Auth.ForgottenPassword,
            search: search[0]?.toString()
        })
    }

    return (<>
        <div className={styles.wrapper}>
            <form method="post"
                onSubmit={FormSubmitHandler}
                ref={form}
                className="form">
                <Field className={styles.field}
                    aria-label="Verification OTP Input"
                    validationState={model.error ? "error" : "none"}
                    validationMessage={model.error}>
                    <Input id={tokenId}
                        as="input"
                        maxLength={8}
                        placeholder='--------'
                        input={{
                            style: {
                                textAlign: "center",
                                letterSpacing: tokens.spacingHorizontalM
                            }
                        }}
                        className={styles.otpInput}
                        onChange={InputChangeHandler}
                        name="username"
                        size='large'
                        disabled={loading}
                        value={model.otp}
                        appearance="outline"
                        aria-label="Username input" />
                </Field>
                {/* <div className={styles.field}>
                    <Button appearance='primary' disabled={loading}>
                        {loading ? <Spinner size="tiny" /> : "Continue"}
                    </Button>
                </div> */}
                <div className={styles.links}>
                    <Button appearance='transparent'
                        aria-label='Forggotten password link'
                        onClick={ResendVerificationCode}>
                        Resend Code
                    </Button>
                </div>
            </form>
        </div>
    </>)
}


