import { ChangeEvent, useCallback } from "react"
import { Caption1, Caption1Stronger, Field, Input, makeStyles, tokens } from "@fluentui/react-components"
import Borders from "../../../../strings/ui/Borders"
import { BsCheckCircle, BsXCircle } from "react-icons/bs"
import { useValidators } from "../../../../hooks"

export interface ICreatePasswordComponentProps {
    password: {
        value?: string
        error?: string
    },
    confirmPassword: {
        value?: string
        error?: string
    },
    disabled: boolean,
    showPassword: boolean,
    onInputChange: (event: ChangeEvent<HTMLInputElement>) => void,
    passwordToggleElement: JSX.Element
}

export default function CreatePasswordComponent({
    password,
    confirmPassword,
    onInputChange,
    passwordToggleElement,
    showPassword,
    disabled
}: ICreatePasswordComponentProps) {
    const { passwordValidator } = useValidators()
    const styles = CreatePasswordComponentStyles()

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

    return (
        <>
            <div className="col-12 password-field">
                <Field className={styles.field}
                    label={"Password"}
                    validationMessage={password.error}
                    validationState={
                        (
                            (password.value ? !passwordValidator.validate(password.value) : false) || password.error
                                ? "error"
                                : (password.value && passwordValidator.validate(password.value) ? "success" : "none")
                        )
                    }>
                    <Input value={password.value}
                        onChange={onInputChange}
                        name="password"
                        autoComplete="off"
                        //placeholder="Enter your new password"
                        disabled={disabled}
                        contentAfter={passwordToggleElement}
                        type={showPassword ? "text" : "password"}
                        aria-label="New Password input" />
                </Field>
            </div>
            <div className="col-12">
                <div className={styles.passwordState}>
                    <Caption1Stronger className={styles.requirementsTitle}>Requirements:</Caption1Stronger>
                    {renderRequirementsList(password.value ?? "")}
                </div>
            </div>
            <div className="col-12 password-field">
                <Field label={"Confirm Password"}
                    className={styles.field}
                    validationMessage={confirmPassword.error}
                    validationState={
                        confirmPassword.error ? "error" :
                            (confirmPassword.value && password.value === confirmPassword.value ? "success" : "none")
                    }>
                    <Input value={confirmPassword.value}
                        onChange={onInputChange}
                        //placeholder="Confirm your new password"
                        name="confirmPassword"
                        disabled={disabled}
                        autoComplete="off"
                        contentAfter={passwordToggleElement}
                        type={showPassword ? "text" : "password"}
                        aria-label="Confirm Password input" />
                </Field>
            </div>
        </>
    )
}

const CreatePasswordComponentStyles = makeStyles({
    field: {
        display: "flex",
        flexDirection: "column",
        marginBottom: `${tokens.spacingVerticalS}`
    },
    passwordStateIcon: {
        position: "relative",
        top: "-1px",
        marginRight: tokens.spacingHorizontalXS
    },
    passwordState: {
        display: "flex",
        flexDirection: "column",
        justifyContent: "flex-start",
        //margin: `${tokens.spacingVerticalM} 0`,
        border: Borders.thinNeutral,
        padding: tokens.spacingHorizontalM,
        borderRadius: tokens.borderRadiusMedium,
        backgroundColor: tokens.colorNeutralCardBackground
    },
    requirementsTitle: {
        marginBottom: tokens.spacingHorizontalXS
    }
})