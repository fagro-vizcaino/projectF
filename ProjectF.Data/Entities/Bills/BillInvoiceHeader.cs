using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Bills
{
    public class BillInvoiceHeader : _BaseEntity
    {
        public Code Code { get; private set; }
        public Name SupplierName { get; private set; }
        public int SupplierId { get; private set; }

        public DateTime BillDate { get; set; }
        public Code InvoiceNumber { get; set; }
        public Code Ncf { get; set; }
        public string PaymentTerm { get; set; }
        public int PaymentTermId { get; set; }
        public int MyProperty { get; set; }

    }
}
