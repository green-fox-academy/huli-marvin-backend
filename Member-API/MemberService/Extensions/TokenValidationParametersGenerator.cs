using MemberService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MemberService.Extensions
{
    public static class TokenValidationParametersGenerator
    {
        public static void GenerateValidationParameters(this JwtBearerOptions options, IConfigurationRoot Configuration)
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = Configuration.GetValue<bool>("ValidateIssuerSigningKey"),
                ValidateIssuer = Configuration.GetValue<bool>("ValidateIssuer"),
                ValidIssuer = Configuration.GetValue<string>("ValidIssuer"),
                ValidateAudience = Configuration.GetValue<bool>("ValidateAudience"),
                ValidAudience = Configuration.GetValue<string>("ValidAudience"),
                ValidateLifetime = Configuration.GetValue<bool>("ValidateLifetime"),
                IssuerSigningKey = AuthService.GetPublicKey()
            };
        }
    }
}
