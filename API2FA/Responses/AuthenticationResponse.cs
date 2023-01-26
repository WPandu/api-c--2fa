using System.Text.Json;
using System.Text.Json.Serialization;

namespace API2FA.Responses
{
    public class LoginResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
    }

    public class GoogleQrCodeResponse
    {
        [JsonPropertyName("qr_code")]
        public string? QrCode { get; set; }

        [JsonPropertyName("secret_key")]
        public string? SecretKey { get; set; }
    }
}
