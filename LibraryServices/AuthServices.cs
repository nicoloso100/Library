using LibraryDTOs;
using LibraryUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace LibraryServices
{
    public class AuthServices : IAuthServices
    {
        private readonly LibraryHttpClient _libraryHttpClient1;
        private readonly IConfiguration _configuration;
        public AuthServices(IConfiguration configuration, LibraryHttpClient libraryHttpClient)
        {
            _libraryHttpClient1 = libraryHttpClient;
            _configuration = configuration;
        }
        public async Task<SecurityToken> AuthUser(DTOLogin auth)
        {
            var users = await _libraryHttpClient1.GetUsers();
            var foundUser = users.ToList().FirstOrDefault(user => user.UserName.Equals(auth.Username) && user.Password.Equals(auth.Password));
            if(foundUser is not null)
            {
                var token = GenerateToken(foundUser);

                return token;
            }

            return null;
        }

        private JwtSecurityToken GenerateToken(DTOUser login)
        {
            string ValidIssuer = _configuration["ApiAuth:Issuer"];
            string ValidAudience = _configuration["ApiAuth:Audience"];
            SymmetricSecurityKey IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiAuth:SecretKey"]));

            DateTime dtFechaExpiraToken;
            DateTime now = DateTime.Now;
            dtFechaExpiraToken = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, 999);

            var claims = new[]
            {
                new Claim(Constants.JWT_CLAIM_USUARIO, login.UserName)
            };

            return new JwtSecurityToken
            (
                issuer: ValidIssuer,
                audience: ValidAudience,
                claims: claims,
                expires: dtFechaExpiraToken,
                notBefore: now,
                signingCredentials: new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
