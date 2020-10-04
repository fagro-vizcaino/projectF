using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.PaymentTerms
{
    public class PaymentTermContainerHandler : BaseContainerBasicCrud<PaymentTerm>
    {
        public PaymentTermContainerHandler() : base("Termino de pago")
        {
            var emptyObject = new PaymentTerm
            {
                Id = 0,
                Description = string.Empty,
                DayValue = 0
            };
            InitModel(emptyObject);
            NewOrEditOperation = GetNewModelOrEdit;
        }

        public PaymentTerm GetNewModelOrEdit(PaymentTerm paymentTerm = null)
          => paymentTerm ?? new PaymentTerm
          {
              Id = 0,
              DayValue = 0,
              Description = string.Empty
          }
                ;
    }
}
