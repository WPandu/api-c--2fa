namespace API2FA.Helpers
{
    using System;
    using API2FA.Models;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    public class TokenManager
    {
        public static readonly string _secret = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
        public static readonly string _issuer = "JWTIssuer";
        public static readonly string _audience = "JWTAudience";

        public string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secToken = new JwtSecurityToken(
                signingCredentials: credentials,
                issuer: _issuer,
                audience: _audience,
                claims: new[]
                {
                    new Claim("id", user.ID.ToString()),
                    new Claim("email", user.Email),
                    new Claim("exp", DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds().ToString())

                },
                expires: DateTime.UtcNow.AddDays(1));
            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(secToken);
        }

        public string ValidateToken(string token)
        {
            if (token == null)
            {
                throw new System.UnauthorizedAccessException("Unauthorized");
            }

            if (token.Contains(" ")) {
                token = token.Split(" ").Last();
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();
                SecurityToken validatedToken;
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userID = jwtToken.Claims.First(x => x.Type == "id").Value;

                return userID;
            }
            catch
            {
                throw new System.UnauthorizedAccessException("Unauthorized");
            }
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret)) // The same key as the one that generate the token
            };
        }
    }
}
