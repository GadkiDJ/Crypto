using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Crypto.Entities;
using Crypto.Infrastructure.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Crypto.Application.Auth
{
    public class JwtService
    {
        private readonly JwtOptions _options;

        public JwtService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public string Generate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("tenantId", user.TenantId.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token =  new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
