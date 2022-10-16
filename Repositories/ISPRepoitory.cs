using System.Threading.Tasks;
using BankingWebAPI.Context;

namespace BankingWebAPI.Data
{
    public interface ISPRepoitory
    {
        Task<CreateAccount> CreateAccount(Accounts createAccount);
        Task<TransferFundsStatus> TransferFunds(TransferFunds transferFunds);
        Task<List<TransferSource>> GetTransferSource();
    }
}

