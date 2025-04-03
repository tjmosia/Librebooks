export const Validators = {
    password: {
        isValid: (value: string) => value.match(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{8,}$/),
        hasDigit: (value: string) => /[0-9]/.test(value),
        hasUpperCase: (value: string) => /[A-Z]/.test(value),
        hasLowerCase: (value: string) => /[a-z]/.test(value),
        hasMinLength: (value: string, minLength: number) => minLength <= value.length,
        hasNonAlphaNumeric: (value: string) => /[^a-zA-Z\d\s:]/.test(value),
    }
}