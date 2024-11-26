namespace MacbooksAPI.Areas.Identity.Models
{
    public struct EmailVerificationTypes
    {
        public static string Registration = nameof(Registration).ToUpper();
        public static string PasswordReset = nameof(PasswordReset).ToUpper();
    }
}
