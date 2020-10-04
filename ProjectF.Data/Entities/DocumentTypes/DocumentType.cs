using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectF.Data.Entities.DocumentTypes
{
    public enum DocumentType
    {
        Invoice,
        CreditNote,
        CashRecibt, //Recibo de caja
        DisbursementVoucher, //comprobante de egreso
        Quotation,
        PurchaseOrder,
        ServiceOrder //conduce
    }
}
