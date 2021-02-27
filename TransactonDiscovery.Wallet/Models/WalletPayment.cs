using System;
using System.Collections.Generic;
using System.Text;

namespace TransactonDiscovery.Wallet.Models
{
    public class WalletPayment
    {
        public string PaymentId { get; set; }
        public string PagingToken { get; set; }
        public bool TransactionSuccessful { get; set; }
        public string SourceAccount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TransactionHash { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public double Amount { get; set; }
    }
}
