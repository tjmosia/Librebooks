﻿namespace LibreBooks.Areas.Identity.Models
{
    public class AuthRespDTOs
    {
        public class FindUserDto
        {
            public string? Email { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Photo { get; set; }
        }

        public class LoginDto
        {
            public string? Email { get; set; }
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
