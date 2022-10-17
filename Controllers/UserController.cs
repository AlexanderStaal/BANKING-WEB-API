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
using BankingWebAPI.Models;

namespace BankingWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersDBContext _userContext;
        public UserController(UsersDBContext userContext)
        {
            _userContext = userContext;
        }

        [HttpGet]
        [Route("{userName}")]
        public async Task<IActionResult> GetUser([FromRoute] string userName)
        {
            var user = await _userContext.User.FirstOrDefaultAsync(x => x.userName == userName);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            var base64Hash = Hasher.HashPassword(user.password);
            var isPasswordValid = Hasher.VerifyPassword(user.password, base64Hash);

            if (!isPasswordValid)
                return NotFound(new { Message = "Password is invalid" });

            var result = await _userContext.User.FirstOrDefaultAsync(x => x.userName == user.userName);
            if (result == null)
                return NotFound(new { Message = "User Not Found" });

            return Ok(new
            {
                Message = "Login Success"
            });

        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            if (await CheckUserNameExistAsync(user.userName))
                return BadRequest(new { Message = "User Already Exist" });

            if (await CheckUserNameExistAsync(user.email))
                return BadRequest(new { Message = "Email Already Exist" });

            var psw = CheckPasswordStrength(user.password);
            if (!string.IsNullOrEmpty(psw))
                return BadRequest(new { Message = psw.ToString() });

            user.password = Hasher.HashPassword(user.password);
            user.token = "";
            await _userContext.User.AddAsync(user);
            await _userContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "User Registered"
            });

        }

        private Task<bool> CheckUserNameExistAsync(string username)
        => _userContext.User.AnyAsync(x => x.userName == username);

        private Task<bool> CheckEmailExistAsync(string email)
        => _userContext.User.AnyAsync(x => x.email == email);

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("Minimum password length should be 8" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be Alphanumeric" + Environment.NewLine);
            //if (Regex.IsMatch(password, "[<,>,@,!,\\[,\\],:,~,*,|,#,$,%,&,/,(,),=,?,»,«,£,§,€,{,},-,;,',_]"))
            //    sb.Append("Password should contain special character" + Environment.NewLine);
            return sb.ToString();
        }

    }
}

