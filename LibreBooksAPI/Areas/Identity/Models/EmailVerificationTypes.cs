namespace LibreBooks.Areas.Identity.Models
{
    public struct EmailVerificationTypes
    {
        public static string Registration = nameof(Registration).ToUpper();
        public static string PasswordReset = nameof(PasswordReset).ToUpper();
        public static string EmailChange = nameof(EmailChange).ToUpper();
    }
}
