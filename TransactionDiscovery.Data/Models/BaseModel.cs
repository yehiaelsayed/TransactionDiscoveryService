using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TransactionDiscovery.Data.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime RecoredCreationDate { get; set; }
        public Nullable<DateTime> RecoredUpdateDate { get; set; }
        public BaseModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
