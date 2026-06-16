using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using O2morny.Application.Common.Interfaces.Services;
using O2morny.Application.Common.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace O2morny.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public Task<string> GenerateToken(JwtUserModel user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),

                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),

                new Claim(JwtRegisteredClaimNames.Sub, user.Id),

                new Claim(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber)
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddYears(1),

                SigningCredentials = creds,

                Issuer = _configuration["Jwt:Issuer"],

                Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(
                tokenHandler.WriteToken(token));
        }
    }
}
