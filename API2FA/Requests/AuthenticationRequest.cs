using System.Text.Json.Serialization;

namespace API2FA.Requests
{
    public class LoginRequest
    {
        
        public string Email { get; set; }
        public string Password { get; set; }
        public string? OTP { get; set; }
    }

    public class RegisterGoogleQrCodeRequest
    {
        [JsonPropertyName("google_2fa_secret")]
        public string Google2faSecret { get; set; }
    }
}
