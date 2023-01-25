namespace API2FA.Helpers
{
    using System.Security.Cryptography;
    using System;
    using System.Collections.Generic;
    using API2FA.Models;
    using JWT.Builder;
    using JWT.Algorithms;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;

    public class TokenManager
    {
        public static readonly string _secret = "Nu0qMql2ueGdf81YfaiU2izDdDESV9XvTIwYroexooHtkdVgqTlpN07QPMtQktcd";
        public static readonly string _issuer = "JWTIssuer";

        public string GenerateAccessToken(User user)
        {
            return new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(Encoding.ASCII.GetBytes(_secret))
                .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds())
                .AddClaim("id", user.ID)
                .AddClaim("email", user.Email)
                .Issuer(_issuer)
                .Audience("access")
                .Encode();
        }

        public (string refreshToken, string jwt) GenerateRefreshToken(User user)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                Convert.ToBase64String(randomNumber);
            }

            var randomString = System.Text.Encoding.ASCII.GetString(randomNumber);

            string jwt = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(_secret)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(4).ToUnixTimeSeconds())
                .AddClaim("refresh", randomString)
                .AddClaim("id", user.ID)
                .AddClaim("email", user.Email)
                .Issuer(_issuer)
                .Audience("refresh")
                .Encode();

            return (randomString, jwt);
        }

        public IDictionary<string, object> VerifyToken(string token)
        {
            return new JwtBuilder()
                    .WithSecret(_secret)
                    .MustVerifySignature()
                    .Decode<IDictionary<string, object>>(token);
        }

        public string? ValidateToken(string token)
        {
            if (token == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(1) //1 minute tolerance for the expiration date
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userID = jwtToken.Claims.First(x => x.Type == "id").Value;

                // return user id from JWT token if validation successful
                return userID;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
