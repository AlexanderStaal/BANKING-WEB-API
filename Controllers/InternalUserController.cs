using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.FileProviders;
using BankingWebAPI.Context;
using BankingWebAPI.Data;
using BankingWebAPI.Repositories;
using BankingWebAPI.Models;
using BankingWebAPI.Helper;

namespace BankingWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InternalUserController : ControllerBase
    { 

    private readonly UsersDBContext _userContext;
    private readonly JWTSetting setting;
    private readonly IRefreshTokenGenerator tokenGenerator;

    public InternalUserController(UsersDBContext userContext, IOptions<JWTSetting> options, IRefreshTokenGenerator _refreshToken)
    {
        _userContext = userContext;
        setting = options.Value;
        tokenGenerator = _refreshToken;
    }

    [NonAction]
    public TokenResponse Authenticate(string username, Claim[] claims)
    {
        TokenResponse tokenResponse = new TokenResponse();
        var tokenkey = Encoding.UTF8.GetBytes(setting.securitykey);
        var tokenhandler = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
             signingCredentials: new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)

            );
        tokenResponse.JWTToken = new JwtSecurityTokenHandler().WriteToken(tokenhandler);
        tokenResponse.RefreshToken = tokenGenerator.GenerateToken(username);

        return tokenResponse;
    }

    [Route("Authenticate")]
    [HttpPost]
    public IActionResult Authenticate([FromBody] User user)
    {
        TokenResponse tokenResponse = new TokenResponse();
        var _user = _userContext.User.FirstOrDefault(o => o.userName == user.userName);
        if (_user == null)
            return Unauthorized();

        var tokenhandler = new JwtSecurityTokenHandler();
        var tokenkey = Encoding.UTF8.GetBytes(setting.securitykey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new Claim[]
                {
                        new Claim(ClaimTypes.Name, _user.userName),
                        new Claim(ClaimTypes.Role, _user.role)

                }
            ),
            Expires = DateTime.Now.AddMinutes(600),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenhandler.CreateToken(tokenDescriptor);
        string finaltoken = tokenhandler.WriteToken(token);

        tokenResponse.JWTToken = finaltoken;
        tokenResponse.RefreshToken = tokenGenerator.GenerateToken(user.userName);

        return Ok(tokenResponse);
    }

    [Route("Refresh")]
    [HttpPost]
    public IActionResult Refresh([FromBody] TokenResponse token)
    {

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token.JWTToken);
        var username = securityToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;


        //var username = principal.Identity.Name;
        var _reftable = _userContext.Refreshtoken.FirstOrDefault(o => o.UserId == username && o.RefreshToken == token.RefreshToken);
        if (_reftable == null)
        {
            return Unauthorized();
        }
        TokenResponse _result = Authenticate(username, securityToken.Claims.ToArray());
        return Ok(_result);
    }

    [Route("GetMenubyRole/{role}")]
    [HttpGet]
    public IActionResult GetMenubyRole(string role)
    {
        var _result = (from q1 in _userContext.Permission.Where(item => item.RoleId == role)
                       join q2 in _userContext.Menu
                       on q1.MenuId equals q2.Id
                       select new { q1.MenuId, q2.Name, q2.LinkName }).ToList();
        // var _result = context.TblPermission.Where(o => o.RoleId == role).ToList();

        return Ok(_result);
    }

    [Route("HaveAccess")]
    [HttpGet]
    public IActionResult HaveAccess(string role, string menu)
    {
        APIResponse result = new APIResponse();
        //var username = principal.Identity.Name;
        var _result = _userContext.Permission.Where(o => o.RoleId == role && o.MenuId == menu).FirstOrDefault();
        if (_result != null)
        {
            result.result = "pass";
        }
        return Ok(result);
    }

    [Route("GetAllRole")]
    [HttpGet]
    public IActionResult GetAllRole()
    {
        var _result = _userContext.Role.ToList();
        // var _result = context.TblPermission.Where(o => o.RoleId == role).ToList();

        return Ok(_result);
    }
}
}




