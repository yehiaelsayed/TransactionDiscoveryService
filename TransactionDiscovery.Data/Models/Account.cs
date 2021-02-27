using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionDiscovery.Data.Models
{
    public class Account : BaseModel
    {
        public string PublicKey { get; set; }

        public List<DiscoveryPatch> DiscoveryPatchs { get; set; }
    }
}
