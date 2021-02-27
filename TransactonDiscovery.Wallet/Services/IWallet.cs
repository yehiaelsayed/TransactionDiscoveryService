using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransactonDiscovery.Wallet.Models;

namespace TransactonDiscovery.Wallet.Services
{
    public interface IWallet
    {
        Task<List<WalletPayment>> GetAccountPayments(string accountId, string cursor, string transactionType, string assetType, int limit);
    }
}
