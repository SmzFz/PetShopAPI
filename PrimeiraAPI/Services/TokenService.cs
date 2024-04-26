using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using PrimeiraAPI.Data;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PrimeiraAPI.Services
{
    public class TokenService
    {
        public static object GenerateToken(IdentityUser user, List<string> roles)
        {
            var listClaims = new List<Claim>();
            foreach (var role in roles)
            {
                listClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            listClaims.Add(new Claim("userId", user.Id.ToString()));
            listClaims.Add(new Claim("userName", user.UserName));
            listClaims.Add(new Claim("userEmail", user.Email));


            var key = Encoding.ASCII.GetBytes(MyKey.Secret);

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(listClaims),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }
    }
}
