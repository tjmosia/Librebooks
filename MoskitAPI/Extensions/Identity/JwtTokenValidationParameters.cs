namespace Moskit.Extensions.Identity
{
    public class JwtTokenValidationParameters
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpiryTimeInMinutes { get; set; }

        public JwtTokenValidationParameters () { }

        public JwtTokenValidationParameters (string? secretKey, string? issuer, string? audience, int expiryTimeInMinutes)
            : this()
        {
            SecretKey = secretKey;
            Issuer = issuer;
            Audience = audience;
            ExpiryTimeInMinutes = expiryTimeInMinutes;
        }

        public static (string SecretKey, string Issuer, string Audience, string ExpiryTimeInMinutes) GetConfigurationKeyNames ()
            => (
                SecretKey: $"{nameof(JwtTokenValidationParameters)}:SecretKey",
                Issuer: $"{nameof(JwtTokenValidationParameters)}:Issuer",
                Audience: $"{nameof(JwtTokenValidationParameters)}:Audience",
                ExpiryTimeInMinutes: $"{nameof(JwtTokenValidationParameters)}:ExpiryTimeInMinutes"
            );
    }
}
