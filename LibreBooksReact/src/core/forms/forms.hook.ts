

export function useFormUtils() {
    const errorMessages = {
        required(fieldName?: string) {
            return `${fieldName} is required.`
        },
        passwordMismatch: "Passwords do not match.",
        passwordWeak: "Password doesn't meet requirements."
    }

    return {
        fieldErrors: errorMessages
    }
}