using LibraryDTOs;
using LibraryServices;
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
        private readonly IAuthServices _authServices;
        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] DTOLogin login)
        {
            try
            {
                var token = await _authServices.AuthUser(login);

                if(token is null)
                {
                    return BadRequest("incorrect user or password");
                }
                else
                {
                    return Ok(new
                    {
                        response = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
