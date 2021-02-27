using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionDiscovery.API.Models.Responses
{
    public class DiscoveryPatchResponse
    {
        public string AccountId { get; set; }

        public bool IsSuccess { get; set; }

        public int PulledRecordsCount { get; set; }
    }
}
