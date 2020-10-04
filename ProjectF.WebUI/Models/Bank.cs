using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Models
{
    public class Bank : FEntity
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public decimal InitialBalance { get; set; }
        public long BankAccountTypeId { get; set; }
        public BankAccountType BankAccountType { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
