import { ChangeEvent, FormEvent, useContext, useEffect, useState } from 'react'
import { Input, useId, Button, Spinner, makeStyles, tokens, Field } from '@fluentui/react-components'
import { usePageTitle } from '../../../hooks/page.hook'
import { AuthContext } from './auth.context'
import { BsEnvelope } from 'react-icons/bs'
import { useNavigate } from 'react-router'
import AppRoutes from '../../../strings/AppRoutes'
import useSessionData from '../../../extensions/SessionData'
import { useValidators } from '../../../hooks/validators.hook'

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
    }
})

interface IModel {
    email: string
    error?: string
}

export default function UsernamePage() {
    usePageTitle!("Sign in or Sign up")
    const usernameId = useId("username")
    const styles = LoginStyles()
    const navigate = useNavigate()
    const { setFormTitle, loading, setLoading, returnUrl, setUsername, username, setFormMessage } = useContext(AuthContext)
    const session = useSessionData()
    const [model, setModel] = useState<IModel>({
        email: username ?? ""
    })
    const { emailvalidator } = useValidators()


    function modelValid() {
        if (!model.email)
            return false
        if (!emailvalidator.validate(model.email)) {
            setModel(model => ({
                email: model.email,
                error: "Please check your email."
            }))
            return false
        }
        return true
    }

    function InputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
        const { value } = event.target

        setModel({
            email: value,

        })
    }

    function FormSubmitHandler(event: FormEvent) {
        event.preventDefault()
        setLoading!(true)
        console.log(emailvalidator.validate(model.email))

        if (!modelValid()) {
            setLoading!(false)
            return
        }

        setTimeout(() => {
            setLoading!(false)
            setUsername!(model.email)

            navigate({
                pathname: AppRoutes.Auth.Login,
                search: `returnUrl=${returnUrl}`
            })
        }, 1000)
    }

    useEffect(() => {
        setFormTitle!("Sign in or Sign up")
        session.remove("USERNAME")
        setFormMessage!("Continue with your email address.")
    }, [setFormTitle, session, setFormMessage])

    return (<>
        <form onSubmit={FormSubmitHandler}
            method="post"
            className="form">
            <Field label="Username"
                validationMessage={model.error}
                validationState={model.error ? "error" : "none"}>
                <Input id={usernameId}
                    onChange={InputChangeHandler}
                    contentBefore={<BsEnvelope size={16} />}
                    disabled={loading}
                    size='large'
                    value={model.email}
                    appearance="outline"
                    aria-label="Username input" />
            </Field>
            <div className={styles.field}>
                <Button type='submit'
                    appearance='primary'
                    size='large'
                    disabled={loading}>
                    {loading ? <Spinner size="tiny" /> : "Continue"}
                </Button>
            </div>
        </form>
    </>)
}


