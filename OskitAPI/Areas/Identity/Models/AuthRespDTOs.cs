namespace OskitAPI.Areas.Identity.Models
{
    public class AuthRespDTOs
    {
        public class FindUserDto
        {
            public string? Username { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Photo { get; set; }
        }

        public class LoginDto
        {
            public string? Username { get; set; }
            public string? AccessToken { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Photo { get; set; }
        }

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
