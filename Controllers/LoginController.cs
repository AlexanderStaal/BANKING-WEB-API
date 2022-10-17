using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BankingWebAPI.Context;
using BankingWebAPI.Data;
using BankingWebAPI.Repositories;
using BankingAPI.Helper;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using System.Threading.Tasks;
using BankingWebAPI.Models;

namespace BankingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly UsersDBContext _userContext;

        public LoginController(IConfiguration config, UsersDBContext userContext)
        {
            _config = config;
            _userContext = userContext;
        }

        [HttpPost]
        [Route("{userName}")]
        public async Task<IActionResult> Login([FromRoute] string userName)
        {
            var user = await _userContext.User.FirstOrDefaultAsync(x => x.userName == userName);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.userName),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.GivenName, user.firstName),
                new Claim(ClaimTypes.Surname, user.lastName),
                new Claim(ClaimTypes.Role, user.role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}
