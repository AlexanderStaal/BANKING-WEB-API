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
using BankingWebAPI.Models;

namespace BankingWebAPI.Repositories
{
    public class SPRepository : ISPRepoitory
    {
        private readonly CreteAccountDBContext _creteAccountDBContext;
        private readonly TransactionsHistoryDBContext _transactionsHistoryContext;
        private readonly TransferFundsDBContext _transferFundsContext;
        private readonly TransferSourceDBContext _transferSourceDBContext;

        public SPRepository(CreteAccountDBContext creteAccountDBContext, TransactionsHistoryDBContext transactionsHistoryContext,
                            TransferFundsDBContext transferFundsContext, TransferSourceDBContext transferSourceDBContext)
        {
            _creteAccountDBContext = creteAccountDBContext;
            _transactionsHistoryContext = transactionsHistoryContext;
            _transferFundsContext = transferFundsContext;
            _transferSourceDBContext = transferSourceDBContext;
        }

        public async Task<CreateAccount> CreateAccount(Accounts createAccount)
        {

            var result = await _creteAccountDBContext.ReturnValue.FromSqlInterpolated($"exec CreateAccount @accountName={createAccount.accountName}, @balance={createAccount.balance}").ToListAsync();

            if (result != null)
            {
                return result.FirstOrDefault();
            }

            return result.FirstOrDefault();
        }

        public async Task<TransferFundsStatus> TransferFunds(TransferFunds transferFunds)
        {

            var result = await _transferFundsContext.TransferFundsStatus.FromSqlInterpolated($"exec TransferFunds @fromAccountNumber={transferFunds.fromAccountNumber}, @toAccountNumber={transferFunds.toAccountNumber}, @amount={transferFunds.amount}").ToListAsync();

            if (result != null)
            {
                return result.FirstOrDefault();
            }

            return result.FirstOrDefault();
        }

        public async Task<List<TransferSource>> GetTransferSource()
        {
            return await _transferSourceDBContext.TransferSource.FromSqlInterpolated($"exec GetTransferSource").ToListAsync();

        }
    }
}

