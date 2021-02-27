using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionDiscovery.API.Models.Requests
{
    public class AddAccounts
    {
        public List<string> AccountsIds { get; set; }
    }
}
