using API_Application.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Application.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private const string TokenSecret = "JWTKeyforAuthentication";
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromDays(30);

        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] TokenRequest userData)
        {
            if (userData == null)
                return BadRequest("Invalid user data");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenSecret);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, userData.Email),
            new Claim("empUuid", userData.EmpUuid.ToString())
        };

            if (userData.CustomClaims != null && userData.CustomClaims.ContainsKey("claimType"))
            {
                var claimType = userData.CustomClaims["claimType"].ToString();
                claims.Add(new Claim("ClaimType", claimType));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                Issuer = "https://localhost:7003/api/Employees",
                Audience = "https://localhost:7003/api/Employees",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = tokenHandler.WriteToken(token);
            return Ok(jwt);
        }
    }

    public class TokenRequest
    {
        public string? Email { get; set; }
        public Guid EmpUuid { get; set; }
        public Dictionary<string, object>? CustomClaims { get; set; }
    }
}
