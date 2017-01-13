using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventAppCore.Models;
using EventAppCore.Models.View;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace EventAppCore.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        public ViewAccessToken GetToken(User user)
        {
            var tokenValidFor = TimeSpan.FromMinutes(20);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: "https://eventappcore.azurewebsites.net/",
                audience: "https://eventappcore.azurewebsites.net/",
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
                expires: DateTime.Now.Add(tokenValidFor),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SIGNING_KEY"))),
                    SecurityAlgorithms.HmacSha256)));

            Console.WriteLine("We have this:" + JsonConvert.SerializeObject(encodedJwt));

            return new ViewAccessToken()
            {
                Token = encodedJwt,
                Expires = (int) tokenValidFor.TotalSeconds
            };
        }

        public string GetIdFromToken(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}