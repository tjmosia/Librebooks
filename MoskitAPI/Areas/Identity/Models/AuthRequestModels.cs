using System.ComponentModel.DataAnnotations;

namespace Moskit.Areas.Identity.Models
{
    public class AuthRequestModels
    {
        public class UsernameModel
        {
            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Please check your email.")]
            public string? Email { get; set; }
        }

        public class LoginModel : UsernameModel
        {
            [Required(ErrorMessage = "Password is required")]
            public string? Password { get; set; }
        }

        public class VerifyModel : UsernameModel
        {
            [Required(ErrorMessage = "Code is required")]
            public string? Code { get; set; }

            [Required(ErrorMessage = "CodeHashString is required")]
            public string? CodeHashString { get; set; }
        }

        public class ResetPasswordModel : VerifyModel
        {
            [Required(ErrorMessage = "Password is required")]
            public string? Password { get; set; }
        }

        public class VendVerificationModel : UsernameModel
        {
            [Required(ErrorMessage = "Reason is required.")]
            public string? Reason { get; set; }
        }

        public class RegisterModel : VerifyModel
        {
            [Required(ErrorMessage = "FirstName is required.")]
            public string? FirstName { get; set; }

            [Required(ErrorMessage = "LastName is required.")]
            public string? LastName { get; set; }

            //[Required(ErrorMessage = "DateOfBirth is required.")]
            //public string? DateOfBirth { get; set; }

            //[Required(ErrorMessage = "Gender is required.")]
            //public string? Gender { get; set; }

            [Required(ErrorMessage = "Password is required")]
            public string? Password { get; set; }
        }
    }
}
