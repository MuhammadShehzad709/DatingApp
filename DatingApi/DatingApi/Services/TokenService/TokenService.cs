using DatingApi.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DatingApi.Services.TokenService
{
    public class TokenService(IConfiguration config) : ITokenService
    {
        public string CreateToken(AppUser user)
        {
            var tokenkey = config["Tokenkey"] ?? throw new Exception("Cannot Get Token Key");
            if (tokenkey.Length < 64)
                throw new Exception("Your Token needs to be >=64 chahracters");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey));
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email,user.Email),
                new(ClaimTypes.NameIdentifier,user.Id.ToString()),
            };
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials=creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}