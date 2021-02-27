using System;
using System.Collections.Generic;
using System.Text;
using TransactionDiscovery.Data.Models.Enums;

namespace TransactionDiscovery.Data.Models
{
    public class DiscoveryPatch : BaseModel
    {
        public PatchStatus Status { get; set; }

        public Guid AccountId { get; set; }

        public Account Account { get; set; }

        public string Cursor { get; set; }

        public int PulledRecordsCount { get; set; }

        public DiscoveryPatch()
        {

        }

        public DiscoveryPatch(Account account)
        {
            AccountId = account.Id;
            Account = account;
        }

        public void Start(string cursor)
        {
            Status = PatchStatus.Initiated;
            Cursor = cursor;
        }

        public void EndSuccessful(string cursor, int pulledCount)
        {
            Cursor = cursor;
            PulledRecordsCount = pulledCount;
            Status = PatchStatus.Success;
        }

        public DiscoveryPatch Failed(string cursor)
        {
            Cursor = cursor;
            Status = PatchStatus.Failed;

            return this;
        }

    }
}
