using System.ComponentModel.DataAnnotations;

namespace LibreBooks.Areas.Identity.Models
{
    public class AccountReqModels
    {
        public class ChangePasswordModel
        {
            [Required(ErrorMessage = "Current password is required.")]
            public string? OldPassword { get; set; }

            [Required(ErrorMessage = "password is required.")]
            public string? Password { get; set; }
        }

        public class UpdateUserPersonalInfoModel
        {
            [Required(ErrorMessage = "First name is required.")]
            public string? FirstName { get; set; }

            [Required(ErrorMessage = "Last name is required.")]
            public string? LastName { get; set; }

            [Required(ErrorMessage = "Gender is required.")]
            public string? Gender { get; set; }

            [Required(ErrorMessage = "Birthday is required.")]
            public string? Birthday { get; set; }
        }

        public class SendVerificationCodeModel
        {
            [Required(ErrorMessage = "Email is required.")]
            [DataType(DataType.Password, ErrorMessage = "Enter a valid email address.")]
            public string? Email { get; set; }
        }



        public class ChangeEmailModel : SendVerificationCodeModel
        {
            [Required(ErrorMessage = "Code Hash is required.")]
            public string? CodeHashString { get; set; }

            [Required(ErrorMessage = "Code is required.")]
            [MinLength(8, ErrorMessage = "Please check your code.")]
            public string? Code { get; set; }
        }
    }
}
