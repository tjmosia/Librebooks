using System.ComponentModel.DataAnnotations;

namespace LibreBooks.Areas.Identity.Models
{
    public class AuthReqModels
    {
        public class UsernameModel
        {
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Enter a valid email address.")]
            public string? Email { get; set; }
        }

        public class LoginModel : UsernameModel
        {
            [Required(ErrorMessage = "Password is required.")]
            public string? Password { get; set; }
        }

        public class VerifyModel : UsernameModel
        {
            [Required(ErrorMessage = "Code is require.d")]
            public string? Code { get; set; }

            [Required(ErrorMessage = "CodeHashString is required.")]
            public string? CodeHashString { get; set; }

            [Required(ErrorMessage = "Reason is required.")]
            public string? Reason { get; set; }
        }

        public class ResetPasswordModel : UsernameModel
        {
            [Required(ErrorMessage = "Password is required.")]
            public string? Password { get; set; }

            [Required(ErrorMessage = "Code is require.d")]
            public string? Code { get; set; }

            [Required(ErrorMessage = "CodeHashString is required.")]
            public string? CodeHashString { get; set; }
        }

        public class SendVerificationModel : UsernameModel
        {
            [Required(ErrorMessage = "Reason is required.")]
            public string? Reason { get; set; }
        }

        public class RegisterModel : UsernameModel
        {
            [Required(ErrorMessage = "FirstName is required.")]
            public string? FirstName { get; set; }

            [Required(ErrorMessage = "LastName is required.")]
            public string? LastName { get; set; }

            [Required(ErrorMessage = "Birthday is required.")]
            public string? Birthday { get; set; }

            [Required(ErrorMessage = "Gender is required.")]
            public string? Gender { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            public string? Password { get; set; }
        }
    }
}
