using LibraryDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Library.Controllers
{
    public class AuthController: ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] DTOLogin login)
        {
            try
            {
                //Verifica el usuario y contraseña
                if (login.Username != "usuario1" || login.Password != "123")
                {
                    return BadRequest("Usuario/Contraseña incorrectos");
                }

                //Genera el token
                var token = GenerarToken(login);

                return Ok(new
                {
                    response = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        private JwtSecurityToken GenerarToken(DTOLogin login)
        {
            string ValidIssuer = _configuration["ApiAuth:Issuer"];
            string ValidAudience = _configuration["ApiAuth:Audience"];
            SymmetricSecurityKey IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiAuth:SecretKey"]));

            //La fecha de expiracion sera el mismo dia a las 12 de la noche
            DateTime dtFechaExpiraToken;
            DateTime now = DateTime.Now;
            dtFechaExpiraToken = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, 999);

            //Agregamos los claim nuestros
            var claims = new[]
            {
                new Claim(Constants.JWT_CLAIM_USUARIO, login.Username)
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
