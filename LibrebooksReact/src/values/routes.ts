
export const routes = {
    auth: {
        login: "/auth/login",
        register: "/auth/register",
        forgotPassword: "/auth/forgot-password",
        resetPassword: "/auth/reset-password",
        verifyEmail: "/auth/verify-email",
        sendVerificationEmail: "/auth/send-email-verificaiton",
        resetPasswordComplete: "/auth/reset-password-complete",
        unverifiedEmail: "/auth/unverified-email",
        verifyEmailComplete: "/auth/verify-email-complete",
    },
    getPageRouteName: (route: string) => route.split('/').pop()
}