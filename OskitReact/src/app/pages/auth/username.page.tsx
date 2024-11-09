import { ChangeEvent, FormEvent, useState } from 'react'
import { Input, Label, useId, Button, Tooltip, Divider, Subtitle1, Spinner, makeStyles, tokens } from '@fluentui/react-components'
import { usePageTitle } from '../../../hooks/page.hook'
import { TbLock, TbLockOpen } from 'react-icons/tb'
import AppRoutes from '../../../strings/AppRoutes'
import { useNavigate } from 'react-router-dom'

const LoginStyles = makeStyles({
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
    title: {
        marginBottom: tokens.spacingVerticalM
    }
})

export default function UsernamePage() {
    usePageTitle("Login")
    const usernameId = useId("username")
    const navigate = useNavigate()
    const [loading, setLoading] = useState(false)
    const [showPassword, setShowPassword] = useState(false)
    const styles = LoginStyles()

    function TogglePasswordHandler() {
        setShowPassword(!showPassword)
    }

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
            <Subtitle1
                className={styles.title}
                align='center'>
                Sign in
            </Subtitle1>
            <form onSubmit={FormSubmitHandler}
                method="post"
                className="form">
                <div className={styles.field}>
                    <Label htmlFor={usernameId}>Username</Label>
                    <Input id={usernameId}
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
            </form>
        </div>
    </>)
}


