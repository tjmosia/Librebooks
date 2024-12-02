import { ChangeEvent, FC, useCallback, useState } from "react"
import { Button, Caption1, Caption1Stronger, Field, Input, makeStyles, tokens, Tooltip } from "@fluentui/react-components"
import Borders from "../../../../strings/ui/Borders"
import { BsCheckCircle, BsLock, BsUnlock, BsXCircle } from "react-icons/bs"
import { useAuthContext } from "../../../../contexts/AuthContext"
import { useValidators } from "../../../../core/extensions"

export interface ICreatePasswordComponentProps {
    password: {
        value?: string
        error?: string
    },
    confirmPassword: {
        value?: string
        error?: string
    },
    inputChangeHandler: (event: ChangeEvent<HTMLInputElement>) => void
}

const CreatePasswordComponentStyles = makeStyles({
    field: {
        display: "flex",
        flexDirection: "column",
        marginBottom: `${tokens.spacingVerticalS}`
    },
    passwordStateDiv: {
        display: "flex",
        flexDirection: "column",
        justifyContent: "flex-start",
        margin: `${tokens.spacingVerticalM} 0`,
        border: Borders.thinNeutral,
        padding: tokens.spacingHorizontalM,
        borderRadius: tokens.borderRadiusMedium,
        backgroundColor: tokens.colorNeutralCardBackground
    },
    passwordStateIcon: {
        position: "relative",
        top: "-1px",
        marginRight: tokens.spacingHorizontalXS
    },
    requirementsTitle: {
        marginBottom: tokens.spacingHorizontalXS
    }
})

const CreatePasswordComponent: FC<ICreatePasswordComponentProps> = ({ password, confirmPassword, inputChangeHandler }) => {
    /*********************************************************************************************************************************
     * SERVICES
     *********************************************************************************************************************************/
    const { passwordValidator } = useValidators()
    const styles = CreatePasswordComponentStyles()
    const { loading } = useAuthContext()

    /*********************************************************************************************************************************
     * STATE
     *********************************************************************************************************************************/
    const [showPassword, setShowPassword] = useState(false)

    /*********************************************************************************************************************************
     * METHODS
     *********************************************************************************************************************************/
    function TogglePasswordHandler() {
        setShowPassword(!showPassword)
    }

    const renderRequirementsList = useCallback(function RenderPasswordStateList(passwordValue: string) {

        const requirements = [
            {
                met: passwordValidator.hasMinLength(passwordValue, 8),
                description: "Contains at least 10 characters"
            },
            {
                met: passwordValidator.hasUpperCase(passwordValue),
                description: "Contains upper case letter"
            },
            {
                met: passwordValidator.hasLowerCase(passwordValue),
                description: "Contains lower case letter"
            },
            {
                met: passwordValidator.hasDigit(passwordValue),
                description: "Contains a number"
            },
            {
                met: passwordValidator.hasSymbol(passwordValue),
                description: "Contains a symbol"
            },
        ]

        return requirements.map((requirement, key) => (
            <Caption1 key={key}>
                {
                    !requirement.met
                        ? <BsXCircle color={tokens.colorStatusDangerBorderActive} className={styles.passwordStateIcon} size={14} />
                        : <BsCheckCircle color={tokens.colorStatusSuccessBorderActive} className={styles.passwordStateIcon} size={14} />
                }
                {requirement.description}
            </Caption1>)
        )
    }, [passwordValidator, styles.passwordStateIcon])

    function RenderPasswordSpyElement() {
        return (
            <Tooltip
                content={`Click to ${showPassword ? "hide" : "show"} password`}
                relationship="label">
                <Button appearance="subtle"
                    size="small"
                    onClick={TogglePasswordHandler}
                    icon={!showPassword ? <BsLock size={16} /> : <BsUnlock size={16} />} />
            </Tooltip>
        )
    }


    /*********************************************************************************************************************************
     * RENDER
     *********************************************************************************************************************************/
    return (
        <>
            <Field className={styles.field} label={"Password"}
                validationMessage={password.error}
                validationState={
                    (
                        (password.value ? !passwordValidator.validate(password.value) : false) || password.error
                            ? "error"
                            : (password.value && passwordValidator.validate(password.value) ? "success" : "none")
                    )
                }>
                <Input appearance="outline"
                    value={password.value}
                    onChange={(event) => inputChangeHandler(event)}
                    name="password"
                    disabled={loading}
                    contentAfter={RenderPasswordSpyElement()}
                    type={showPassword ? "text" : "password"}
                    aria-label="Passwordd input" />
            </Field>
            <div className={styles.passwordStateDiv}>
                <Caption1Stronger className={styles.requirementsTitle}>Requirements:</Caption1Stronger>
                {renderRequirementsList(password.value ?? "")}
            </div>
            <Field label={"Confirm Password"}
                validationMessage={confirmPassword.error}
                validationState={
                    confirmPassword.error ? "error" :
                        (confirmPassword.value && password.value === confirmPassword.value ? "success" : "none")
                }>
                <Input appearance="outline"
                    value={confirmPassword.value}
                    onChange={inputChangeHandler}
                    name="confirmPassword"
                    disabled={loading}
                    contentAfter={RenderPasswordSpyElement()}
                    type={showPassword ? "text" : "password"}
                    aria-label="Passwordd input" />
            </Field>
        </>
    )
}

export default CreatePasswordComponent