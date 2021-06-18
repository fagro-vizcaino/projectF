using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectF.Data.Entities.DocumentTypes
{
    public enum DocumentType
    {
        Invoice, //factura de cobro
        InvoiceRecibt, //Recibo de cobro
        CreditNote, //Nota de credito
        CashRecibt, //Recibo de caja
        Bill, //Factura de pago
        BillRecibt, //Recibo de pago
        DebitNote,
        DisbursementVoucher, //comprobante de egreso
        Quotation, //Cotización
        PurchaseOrder, //Orden de compra
        ServiceOrder, //conduce

    }
}
