import { ChangeEvent, FormEvent, useState } from 'react'
import { Input, Label, useId, Button, Spinner, makeStyles, tokens } from '@fluentui/react-components'
import { usePageTitle } from '../../../hooks/page.hook'
import AppRoutes from '../../../strings/AppRoutes'
import { useNavigate, useSearchParams } from 'react-router-dom'

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
})

export default function VerifyEmailPage() {
    usePageTitle("Login")
    const tokenId = useId("username")
    const navigate = useNavigate()
    const search = useSearchParams()
    const [loading, setLoading] = useState(false)
    const styles = VerifyEmailPageStyles()

    function InputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
        const { value, name } = event.target

        setModel(state => ({
            ...state,
            [name]: {
                value
            }
        }))
    }

    function FormSubmitHandler(event: FormEvent) {
        event.preventDefault()
        setLoading(true)
    }

    const [model, setModel] = useState({
        username: {
            value: "",
            error: ""
        },
        password: {
            value: "",
            error: ""
        }
    })

    return (<>
        <div className={styles.wrapper}>
            <form onSubmit={FormSubmitHandler}
                method="post"
                className="form">
                <div className={styles.field}>
                    <Label htmlFor={tokenId}>Verification Code</Label>
                    <Input id={tokenId}
                        onChange={InputChangeHandler}
                        name="username"
                        disabled={loading}
                        value={model.username.value}
                        appearance="outline"
                        aria-label="Username input" />
                </div>
                <div className={styles.field}>
                    <Button appearance='primary' disabled={loading}>
                        {loading ? <Spinner size="tiny" /> : "Login"}
                    </Button>
                </div>
                <div className={styles.links}>
                    <Button appearance='transparent'
                        aria-label='Forggotten password link'
                        onClick={() => navigate({
                            pathname: AppRoutes.Auth.ForgottenPassword,
                            search: search[0]?.toString()
                        })}>
                        Resend Code
                    </Button>
                </div>
            </form>
        </div>
    </>)
}


