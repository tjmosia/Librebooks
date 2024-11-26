namespace MacbooksAPI.Extensions.Identity
{
    public class JwtParams
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpiryTimeInMinutes { get; set; }

        public JwtParams () { }

        public JwtParams (string? secretKey, string? issuer, string? audience, int expiryTimeInMinutes)
            : this()
        {
            SecretKey = secretKey;
            Issuer = issuer;
            Audience = audience;
            ExpiryTimeInMinutes = expiryTimeInMinutes;
        }

        public static (string SecretKey, string Issuer, string Audience, string ExpiryTimeInMinutes) GetKeyNames ()
            => (
                SecretKey: $"{nameof(JwtParams)}:SecretKey",
                Issuer: $"{nameof(JwtParams)}:Issuer",
                Audience: $"{nameof(JwtParams)}:Audience",
                ExpiryTimeInMinutes: $"{nameof(JwtParams)}:ExpiryTimeInMinutes"
            );
    }
}
