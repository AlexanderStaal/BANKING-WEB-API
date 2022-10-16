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
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankingAPI.Controllers
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

        [HttpGet("Admins")]
        [Authorize(Roles = "Administrator")]
            public async Task<IActionResult> AdminsEndpoint([FromRoute] string userName)
        {
            var currentUser = await _userContext.User.FirstOrDefaultAsync(x => x.userName == userName);

            return Ok($"Hi {currentUser.userName}, you are an {currentUser.role}");
        }


        [HttpGet("user")]
        [Authorize(Roles = "user")]
            public async Task<IActionResult> SellersEndpoint([FromRoute] string userName)
        {
            var currentUser = await _userContext.User.FirstOrDefaultAsync(x => x.userName == userName);

            return Ok($"Hi {currentUser.userName}, you are a {currentUser.role}");
        }

        [HttpGet("AdminsAndUsers")]
        [Authorize(Roles = "Administrator,Seller")]
            public async Task<IActionResult> AdminsAndSellersEndpoint([FromRoute] string userName)
        {
            var currentUser = await _userContext.User.FirstOrDefaultAsync(x => x.userName == userName);

            return Ok($"Hi {currentUser.userName}, you are an {currentUser.role}");
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Hi, you're on public property");
        }
    }
}
