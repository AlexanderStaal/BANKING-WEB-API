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

namespace BankingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountDBContext _accountContext;
        private readonly ISPRepoitory _sPRepoitory;

        public AccountController(AccountDBContext accountContext,
            ISPRepoitory sPRepoitory)
        {
            _accountContext = accountContext;
            _sPRepoitory = sPRepoitory;
        }

        [HttpGet]
        [Route("GetAllAccounts")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Accounts>>> GetAllAccounts()
        {
            return await _accountContext.Accounts.ToListAsync();
        }


        [HttpPost]
        [Route("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] Accounts createAccountRequest)
        {
            var transaction = await _sPRepoitory.CreateAccount(createAccountRequest);
            return Ok(transaction);
        }


        [HttpGet]
        [Route("{accountNumber}")]
        public async Task<IActionResult> GetAccount([FromRoute] int accountNumber)
        {
            var account = await _accountContext.Accounts.FindAsync(accountNumber);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPut]
        [Route("UpdateAccount")]
        public async Task<ActionResult> UpdateAccount([FromBody] Accounts updateAccountRequest)
        {
            var account = await _accountContext.Accounts.FindAsync(updateAccountRequest.accountNumber);

            if (account == null)
            {
                return NotFound();
            }

            account.accountName = updateAccountRequest.accountName;
            account.balance = updateAccountRequest.balance;
            await _accountContext.SaveChangesAsync();

            return Ok(account);
        }


        [HttpDelete]
        [Route("{accountNumber}")]
        public async Task<ActionResult<Accounts>> DeleteAccount([FromRoute] int accountNumber)
        {
            var account = await _accountContext.Accounts.FindAsync(accountNumber);
            if (account == null)
            {
                return NotFound();
            }

            _accountContext.Accounts.Remove(account);
            await _accountContext.SaveChangesAsync();

            return Ok(account);
        }
    }
}
