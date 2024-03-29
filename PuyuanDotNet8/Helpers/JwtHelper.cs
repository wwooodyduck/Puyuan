using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PuyuanDotNet8.Helpers
{
    public class JwtHelper
    {
        private readonly string issuer;
        private readonly string signKey;
        private readonly int expireMinutes;
        public JwtHelper(IConfiguration configuration)
        {
            issuer = configuration.GetValue<string>("Secret:JwtSettings:Issuer");
            signKey = configuration.GetValue<string>("Secret:JwtSettings:SignKey");
            expireMinutes = configuration.GetValue<int>("Secret:JwtSettings:ExpireMinutes");
        }
        public string GetJwtToken(string uuid, string role, string username)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, uuid));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, username));
            claims.Add(new Claim(ClaimTypes.Role, role));
            var userClaimsIdentity = new ClaimsIdentity(claims);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Subject = userClaimsIdentity,
                Expires = DateTime.Now.AddMinutes(expireMinutes),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var serializeToken = tokenHandler.WriteToken(securityToken);
            return serializeToken;
        }
    }
}
