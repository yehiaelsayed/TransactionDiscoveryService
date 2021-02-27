using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransactionDiscovery.Data.Models;
using TransactionDiscovery.Services.Models;

namespace TransactionDiscovery.Services
{
    public interface ITransactionDiscoveryService
    {
        ServiceResult<List<Account>> AddAccounts(List<string> accountsIds);

        Task<ServiceResult<List<DiscoveryPatch>>> DiscoverPayments(List<Account> account);

        ServiceResult<Dictionary<string, double>> GetAccountTransactionHistory(string accountId);
    }
}
