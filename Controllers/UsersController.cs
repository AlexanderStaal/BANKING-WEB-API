using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BankingWebAPI.Models;
using BankingWebAPI.Data;
using BankingWebAPI.Repositories;

namespace BankingAPI.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersDBContext _userContext;
        public UsersController(UsersDBContext userContext)
        {
            _userContext = userContext;
        }

        [HttpGet]
        [Route("{userName}")]
        public async Task<ActionResult> GetUser([FromRoute] string userName)
        {
            var users = await _userContext.Users.FindAsync(userName);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
    }
}

