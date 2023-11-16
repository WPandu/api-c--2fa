using API2FA.Helpers;
using API2FA.Models;
using API2FA.Requests;
using API2FA.Responses;
using API2FA.IServices;
using Google.Authenticator;

namespace API2FA.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DataContext _context;
        private readonly TokenManager _tokenManager;

        public AuthenticationService(DataContext context, TokenManager tokenManager)
        {
            _context = context;
            _tokenManager = tokenManager;
        }

        public LoginResponse Login(LoginRequest loginRequest)
        {
            var user = _context.Users.Where(u => u.Email == loginRequest.Email).FirstOrDefault();
            if (user == null)
            {
                throw new System.UnauthorizedAccessException("Email or password incorrect");
            }

            var isLoginValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
            if (!isLoginValid)
            {
                throw new System.UnauthorizedAccessException("Email or password incorrect");
            }

            if (user.Google2faSecret != null && String.IsNullOrEmpty(loginRequest.OTP))
            {
                throw new System.MissingFieldException("OTP is required");
            }

            if (user.Google2faSecret != null && !String.IsNullOrEmpty(loginRequest.OTP))
            {
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                var checkOTP = tfa.ValidateTwoFactorPIN(user.Google2faSecret, loginRequest.OTP, true);
                if (!checkOTP)
                {
                    throw new System.MissingFieldException("OTP invalid");
                }
            }

            var accessToken = _tokenManager.GenerateAccessToken(user);

            return new LoginResponse
            {
                AccessToken = accessToken,
            };
        }

        public GoogleQrCodeResponse GoogleQrCode(User? user)
        {
            string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            SetupCode setupInfo = tfa.GenerateSetupCode("API 2FA C#", user.Email, key, false, 3);

            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            string secretKey = setupInfo.ManualEntryKey;

            return new GoogleQrCodeResponse
            {
                QrCode = qrCodeImageUrl,
                SecretKey = secretKey
            };
        }

        public void RegisterGoogleQrCode(Guid userID, RegisterGoogleQrCodeRequest registerGoogleQrCodeRequest)
        {
            if (registerGoogleQrCodeRequest == null)
            {
                throw new System.MissingFieldException("Google 2fa secret is required");
            }
            var user = _context.Users.Find(userID);
            if (user == null) 
            {
                throw new System.UnauthorizedAccessException();
            }
            user.Google2faSecret = registerGoogleQrCodeRequest.Google2faSecret;
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
