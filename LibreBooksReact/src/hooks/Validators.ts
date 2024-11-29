
export default function useValidators() {
    const Regex = {
        email: /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/,
        digit: /[0-9]/,
        lowerCase: /[a-z]/,
        upperCase: /[A-Z]/,
        nonAlphaNumeric: /^[a-zA-Z\d\s:]/
    }

    function hasDigit(password: string) {
        return Regex.digit.test(password)
    }

    function hasLowerCase(password: string) {
        return Regex.lowerCase.test(password)
    }

    function hasUpperCase(password: string) {
        return Regex.upperCase.test(password)
    }

    function hasSymbol(password: string) {
        return Regex.nonAlphaNumeric.test(password)
    }

    function hasMinLength(password: string, minLength = 8) {
        return password.length >= minLength
    }

    return {
        emailvalidator: {
            validate(value: string) {
                return Regex.email.test(value)
            }
        },
        passwordValidator: {
            validate(password: string, minLength = 8) {
                return hasDigit(password)
                    && hasLowerCase(password)
                    && hasUpperCase(password)
                    && hasSymbol(password)
                    && hasMinLength(password, minLength)
            },
            hasDigit,
            hasLowerCase,
            hasSymbol,
            hasUpperCase,
            hasMinLength
        }
    }
}

