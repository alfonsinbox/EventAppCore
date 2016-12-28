using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventAppCore.Models;
using EventAppCore.Models.View;
using Microsoft.IdentityModel.Tokens;

namespace EventAppCore.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        public ViewAccessToken GetToken(User user)
        {
            var jwt = new JwtSecurityToken(
                issuer: "ExampleIssuer",
                audience: "ExampleAudience",
                claims: new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,
                        (DateTime.UtcNow - DateTime.MinValue).TotalSeconds.ToString(CultureInfo.InvariantCulture),
                        ClaimValueTypes.Integer64)
                    // Claim for JWT type (refresh)?
                },
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(20)),
                signingCredentials:
                new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("abc123def456ghi789")),
                    SecurityAlgorithms.HmacSha256));

            return new ViewAccessToken()
            {
                Token = jwt.RawData,
                Expires = jwt.ValidTo.Subtract(jwt.ValidFrom).Seconds
            };
        }

        public string GetIdFromToken(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}