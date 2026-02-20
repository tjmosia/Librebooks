

export const FormValidators = (() => {
    const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&<>^])[A-Za-z\d@$!%*?&<>^]{8,}$/
    const emailRegex = /^[\w.!#$%&'*+/=?^`{|}~-]+@[a-z\d](?:[a-z\d-]{0,61}[a-z\d])?(?:\.[a-z\d](?:[a-z\d-]{0,61}[a-z\d])?)*$/i
    return {
        validatePassword(password: string) {
            return regex.test(password)
        },
        validateEmail(email: string) {
            return emailRegex.test(email)
        }
    }
})()