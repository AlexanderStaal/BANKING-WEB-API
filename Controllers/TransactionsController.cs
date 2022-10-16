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
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionsHistoryDBContext _transactionsHistoryContext;
        private readonly ISPRepoitory _sPRepoitory;


        public TransactionsController(ISPRepoitory sPRepoitory,
            TransferFundsDBContext transferFundsContext,
            TransactionsHistoryDBContext ransactionsHistoryContext)
        {
            _transactionsHistoryContext = ransactionsHistoryContext;
            _sPRepoitory = sPRepoitory;
        }

        [HttpGet]
        [Route("GetAllTransactions")]
        public async Task<ActionResult<IEnumerable<TransactionsHistory>>> GetAllTransactions()
        {
            return await _transactionsHistoryContext.GetAllTransactions.ToListAsync();
        }


        [HttpGet("GetTransferSource")]
        public async Task<ActionResult<IEnumerable<TransferSource>>> GetTransferSource()
        {
            return await _sPRepoitory.GetTransferSource();
        }

        [HttpPost]
        [Route("TransferFunds")]
        public async Task<IActionResult> TransferFunds([FromBody] TransferFunds transferFundsRequest)
        {
            var transaction = await _sPRepoitory.TransferFunds(transferFundsRequest);

            return Ok(transaction);
        }

    }
}


