using MemberService.Extensions;
using MemberService.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MemberService.Services
{
    public class AuthService
    {
        private User user;

        public string GetUrl(string redirect)
        {
            return $"{redirect}?token={GenerateToken()}";
        }

        public bool UserExists(string email)
        {
            user = new User()
            {
                Id = 0,
                Github = "SuchHandle",
                Email = email
            };

            return true;
        }

        public static SecurityKey GetPublicKey()
        {
            var publicRsa = RSA.Create();
            publicRsa.FromXmlStringCore(Startup.Configuration["PublicKey"]);
            return new RsaSecurityKey(publicRsa);
        }

        private string GenerateToken()
        {
            var token = new JwtSecurityToken(
                claims: CreateClaims(),
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: CreateCredentials()
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Claim[] CreateClaims()
        {
            return new Claim[]
            {
                new Claim("uid", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("github", user.Github),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
                new Claim("AccessLevel", user.Level.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Iss, "CHOPIN"),
                new Claim(JwtRegisteredClaimNames.Aud, "MARVIN")
            };
        }

        private SigningCredentials CreateCredentials()
        {
            using (RSA privateRsa = RSA.Create())
            {
                privateRsa.FromXmlStringCore(Startup.Configuration["PrivateKey"]);
                var privateKey = new RsaSecurityKey(privateRsa);
                return new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);
            }
        }
    }
}
