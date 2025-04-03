using Librebooks.Models.Entity.IdentitySpace;

namespace Librebooks.Areas.Identity.Models
{
    public class AuthRespDTOs
    {
        public class FindUserDto
        {
            public string? Email { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Photo { get; set; }

            public static LoginDto Create (User user) => new LoginDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Photo = user.GetPhotoAsBase64()
            };
        }

        public class LoginDto : FindUserDto
        { }

        public class RegisterDto
        {

        }

        public class SendVerificationDto (string codeHashString)
        {
            public string? CodeHashString { get; } = codeHashString;
        }

        public class VerifyDto
        {

        }
        public class ResetPasswordDto
        {

        }
    }
}
