namespace API2FA.Requests
{
    public class LoginRequest
    {
        
        public string Email { get; set; }
        public string Password { get; set; }
        public string? OTP { get; set; }
    }
}
