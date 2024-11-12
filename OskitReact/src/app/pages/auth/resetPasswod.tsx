import { ChangeEvent, FormEvent, useEffect, useState } from 'react'
import { Button, Spinner, makeStyles, tokens, Field } from '@fluentui/react-components'
import { usePageTitle } from '../../../hooks/page.hook'
import { useAuthContext } from './auth.context'
import { useValidators } from '../../../hooks/validators.hook'
import CreatePasswordComponent from './components/password.components'

const ResetPasswordPageStyles = makeStyles({
    field: {
        display: "flex",
        flexDirection: "column",
        marginTop: `${tokens.spacingVerticalM}`
    },
})

interface IModel {
    [key: string]: { value: string }
    password: {
        value: string
        error?: string
    },
    confirmPassword: {
        value: string
        error?: string
    }
}

export default function ResetPasswordPage() {
    const [loading, setLoading] = useState(false)
    usePageTitle("Reset Password")
    const styles = ResetPasswordPageStyles()
    const { setFormTitle, setFormMessage, givenName } = useAuthContext()
    const { passwordValidator } = useValidators()
    const [model, setModel] = useState<IModel>({
        password: {
            value: ""
        },
        confirmPassword: {
            value: ""
        },
    })

    function InputChangeHandler(event: ChangeEvent<HTMLInputElement>) {
        const { value, name } = event.currentTarget

        const newVal = value.replace(/[\s]/, "")
        console.log(event.currentTarget.value)

        if (newVal != model[name].value) {
            setModel(state => {
                const newModel = {
                    ...state,
                    [name]: {
                        value: newVal
                    }
                }

                if (name == "password" && model.confirmPassword.value)
                    newModel.confirmPassword = { value: "" }

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
    }

    function FormSubmitHandler(event: FormEvent) {
        event.preventDefault()
        setLoading(true)

        if (validateModel()) {
            return
        }
    }

    function validateModel() {
        return passwordValidator.validate(model.password.value)
    }

    useEffect(() => {
        setFormTitle!("Reser your password")
        setFormMessage!("")
    }, [setFormTitle, setFormMessage, givenName])


    return (<>
        <form onSubmit={FormSubmitHandler}
            method="post"
            className="form">
            <CreatePasswordComponent
                inputChangeHandler={InputChangeHandler}
                password={model.password}
                confirmPassword={model.confirmPassword} />
            <Field className={styles.field}>
                <Button size='large' appearance='primary' disabled={loading}>
                    {loading ? <Spinner size="tiny" /> : "Continue"}
                </Button>
            </Field>
        </form>
    </>)
}


