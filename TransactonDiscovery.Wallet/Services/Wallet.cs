using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses.operations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransactonDiscovery.Utils;
using TransactonDiscovery.Wallet.Models;

namespace TransactonDiscovery.Wallet.Services
{
    public class Wallet : IWallet
    {

        private Server _server;

        public Wallet(string serverUrl)
        {
            _server = new Server(serverUrl);
        }
        public async Task<List<WalletPayment>> GetAccountPayments(string accountId, string cursor, string transactionType, string assetType, int limit)
        {
            try
            {
                var walletPayments = new List<WalletPayment>();
                var requestBuilder = _server.Payments.ForAccount(accountId).Order(OrderDirection.ASC).Limit(limit);
                if (!string.IsNullOrEmpty(cursor))
                {
                    requestBuilder.Cursor(cursor);
                }
                var result = await requestBuilder.Execute();
                foreach (var record in result.Records)
                {
                    var payment = record as PaymentOperationResponse;
                    if (payment != null && payment.Type?.ToLower() == transactionType && payment.AssetType?.ToLower() == assetType)
                    {
                        walletPayments.Add(new WalletPayment()
                        {
                            PaymentId = payment.Id.ToString(),
                            FromAccount = payment.From,
                            ToAccount = payment.To,
                            PagingToken = payment.PagingToken,
                            SourceAccount = payment.SourceAccount,
                            TransactionHash = payment.TransactionHash,
                            TransactionSuccessful = payment.TransactionSuccessful,
                            CreatedAt = DateTime.Parse(payment.CreatedAt),
                            Amount = double.Parse(payment.Amount)
                        });
                    }

                }

                return walletPayments;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }
    }
}
