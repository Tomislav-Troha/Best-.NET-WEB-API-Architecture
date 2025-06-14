using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BestArchitecture.Infrastructure.Persistance
{
    public static class TokenGenerator
    {
        public static string? GenerateDefaultToken(List<Claim> claims, string audience)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JwtKey");
            var jwtIssuer = Environment.GetEnvironmentVariable("JwtIssuer");

            if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer))
                return null;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(21),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
