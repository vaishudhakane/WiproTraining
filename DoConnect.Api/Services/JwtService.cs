using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoConnect.Api.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config) { _config = config; }

        public string GenerateToken(int userId, string username, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim("id", userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim("username", username)  //changes made after getting bug
            };

            // var token = new JwtSecurityToken(
            //     _config["Jwt:Issuer"],
            //     _config["Jwt:Audience"],
            //     claims,
            //     expires: DateTime.Now.AddHours(3),
            //     signingCredentials: creds);

            // return new JwtSecurityTokenHandler().WriteToken(token);

            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(6),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
