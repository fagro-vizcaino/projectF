using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Models
{
    public class PaymentTerm : FEntity
    {
        public string Description { get; set; }
        public int DayValue { get; set; }
    }
}
