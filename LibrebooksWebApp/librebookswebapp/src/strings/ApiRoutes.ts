
export default {
    Auth: {
        Username: "/auth",
        Login: "/auth/login",
        Register: "/auth/register",
        PasswordReset: "/auth/reset-password",
        ConfirmLogin: "/auth/confirm-login",
        SendVerificationCode: "/auth/send-verification-code",
        Verify: "/auth/verify-email"
    },
    Account: {
        Profile: "/account/profile",
        UpdatePersonInfo: "/account/personal-info/edit",
        UpdateContactInfo: "/account/change-email",
        ChangePassword: "/account/change-password",
        ResetPassword: "/account/reset-password",
        Logout: "/account/logout",
        SendVerificationCode: "/account/send-verification-code"
    },
    System: {
        BusinessSector: {
            GetAll: "/sectors",
            Find: "/sectors/find",
            Create: "/sectors/create",
            Delete: "/sectors/delete",
            Update: "/sectors/edit"
        }
    }
}