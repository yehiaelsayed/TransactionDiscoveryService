using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionDiscovery.Data.Models;
using TransactionDiscovery.Data.Models.Enum;
using TransactionDiscovery.Data.Models.Enums;
using TransactionDiscovery.Data.Repository;
using TransactionDiscovery.Services.Models;
using TransactonDiscovery.Utils;
using TransactonDiscovery.Wallet.Models;
using TransactonDiscovery.Wallet.Services;

namespace TransactionDiscovery.Services
{
    public class TransactionDiscoveryService : ITransactionDiscoveryService
    {
        private IRepository<Account> _accountsRepository;
        private IRepository<DiscoveryPatch> _discoveryPatchRepository;
        private IRepository<Payment> _paymentRepository;
        private IWallet _wallet;
        public TransactionDiscoveryService(IRepository<Account> accountsRepository,
            IRepository<DiscoveryPatch> discoveryPatchRepository,
            IRepository<Payment> paymentPatchRepository,
            IWallet wallet)
        {
            _paymentRepository = paymentPatchRepository;
            _discoveryPatchRepository = discoveryPatchRepository;
            _accountsRepository = accountsRepository;
            _wallet = wallet;
        }
        public ServiceResult<List<Account>> AddAccounts(List<string> accountsIds)
        {
            try
            {
                var newAccounts = new List<Account>();
                var alreadyExistsAccounts = _accountsRepository.Select().Where(A => accountsIds.Contains(A.PublicKey)).ToList();
                var alreadyExistsAccountsId = alreadyExistsAccounts.Select(A => A.PublicKey);
                accountsIds = accountsIds.Where(A => !alreadyExistsAccountsId.Contains(A)).ToList();
                foreach (var accountId in accountsIds)
                {
                    newAccounts.Add(new Account() { PublicKey = accountId });
                }
                _accountsRepository.Insert(newAccounts);
                _accountsRepository.SaveChanges();
                newAccounts.AddRange(alreadyExistsAccounts);
                return new ServiceResult<List<Account>>(newAccounts, true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new ServiceResult<List<Account>>(ex);
            }
        }


        public async Task<ServiceResult<List<DiscoveryPatch>>> DiscoverPayments(List<Account> accounts)
        {
            List<DiscoveryPatch> discoveryPatches = new List<DiscoveryPatch>();

            try
            {
                foreach (var account in accounts)
                {
                    DiscoveryPatch newDiscoveryPatch = null;
                    string lastCursor = null;

                    try
                    {
                        var discoveryPatch = _discoveryPatchRepository.Select().Include(d => d.Account)
                            .Where(A => A.AccountId == account.Id && A.RecoredCreationDate == A.Account.DiscoveryPatchs.Max(x => x.RecoredCreationDate)).FirstOrDefault();

                        var walletPayments = new List<WalletPayment>();
                        var payments = new List<Payment>();

                        lastCursor = discoveryPatch?.Cursor;

                        newDiscoveryPatch = _discoveryPatchRepository.Insert(new DiscoveryPatch(account));
                        newDiscoveryPatch.Start(lastCursor);
                        _discoveryPatchRepository.SaveChanges();

                        await GetIncrementalWalletPayments(account.PublicKey, lastCursor, walletPayments);
                        walletPayments.ForEach(WP => payments.Add(new Payment()
                        {
                            Amount = WP.Amount,
                            CreatedAt = WP.CreatedAt,
                            FromAccount = WP.FromAccount,
                            PagingToken = WP.PagingToken,
                            PaymentId = WP.PaymentId,
                            SourceAccount = WP.SourceAccount,
                            ToAccount = WP.ToAccount,
                            TransactionHash = WP.TransactionHash,
                            TransactionSuccessful = WP.TransactionSuccessful
                        }));
                        _paymentRepository.Insert(payments);
                        var newCursor = payments.Count > 0 ? payments.Last()?.PagingToken : lastCursor;
                        newDiscoveryPatch.EndSuccessful(newCursor, payments.Count);
                        _discoveryPatchRepository.Update(newDiscoveryPatch);
                        _paymentRepository.SaveChanges();
                        discoveryPatches.Add(newDiscoveryPatch);
                    }
                    catch (Exception ex)
                    {
                        discoveryPatches.Add(newDiscoveryPatch ?? new DiscoveryPatch(account).Failed(lastCursor));
                        Logger.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new ServiceResult<List<DiscoveryPatch>>(ex);

            }

            return new ServiceResult<List<DiscoveryPatch>>(discoveryPatches, true);
        }

        public ServiceResult<Dictionary<string, double>> GetAccountTransactionHistory(string accountId)
        {
            var result = new Dictionary<string, double>();
            try
            {
                var account = _accountsRepository.Select().FirstOrDefault(A => A.PublicKey == accountId);

                var accountSentPayments = _paymentRepository.Select()
                     .Where(p => p.FromAccount == account.PublicKey)
                     .GroupBy(P => P.ToAccount)
                     .Select((g) => new { Amount = g.Sum(gp => gp.Amount), ToAccount = g.Key }).ToList();

                var accountRecivedPayments = _paymentRepository.Select()
                     .Where(p => p.ToAccount == account.PublicKey)
                     .GroupBy(P => P.FromAccount)
                     .Select((g) => new { Amount = g.Sum(gp => gp.Amount), FromAccount = g.Key }).ToList();

                foreach (var sentPayment in accountSentPayments)
                {
                    var RecivedPayment = accountRecivedPayments.Where(ARP => ARP.FromAccount == sentPayment.ToAccount).FirstOrDefault();
                    result[sentPayment.ToAccount] = sentPayment.Amount - (RecivedPayment?.Amount ?? 0);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new ServiceResult<Dictionary<string, double>>(ex);
            }
            return new ServiceResult<Dictionary<string, double>>(result, true);
        }

        private async Task<List<WalletPayment>> GetIncrementalWalletPayments(string accountId, string cursor, List<WalletPayment> payments)
        {
            payments = payments ?? new List<WalletPayment>();

            var walletPayments = await _wallet.GetAccountPayments(accountId, cursor, Strings.PaymentTransactionType, Strings.NativeAssetType, Configurations.GetWalletMaxRequestLimit);

            if (walletPayments.Count == 0)
            {
                return payments;
            }
            else
            {
                payments.AddRange(walletPayments);
                var lastCursor = payments.Count > 0 ? payments.Last()?.PagingToken : null;
                return await GetIncrementalWalletPayments(accountId, lastCursor, payments);
            }
        }
    }
}
