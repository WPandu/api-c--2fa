using API2FA.Helpers;
using API2FA.Models;
using API2FA.Requests;
using API2FA.Responses;
using API2FA.IServices;

namespace API2FA.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private DataContext _context;
        private TokenManager _tokenManager;

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

            var refreshToken = _tokenManager.GenerateRefreshToken(user);
            var accessToken = _tokenManager.GenerateAccessToken(user);

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.jwt
            };
        }
    }
}
