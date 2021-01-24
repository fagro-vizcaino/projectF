using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.BillsPayment
{
    public class BillPaymentDetail : _BaseEntity
    {
        public Code Code { get; private set; }
        public GeneralText Concept { get; private set; }
        public int BillInvoiceHeaderId { get; private set; }
        public decimal Amount { get; private set; }
        public decimal Qty { get; private set; }
        public decimal TaxPercent { get; private set; }
        public BillPaymentHeader BillPaymentHeader { get; private set; }

        protected BillPaymentDetail() { }

        public BillPaymentDetail(Code code
            , GeneralText concept
            , int billInvoiceHeaderId
            , decimal amount
            , decimal qty
            , decimal taxPercent
            , BillPaymentHeader billPaymentHeader)
        {
            Code                = code;
            Concept             = concept;
            BillInvoiceHeaderId = billInvoiceHeaderId;
            Amount              = amount;
            Qty                 = qty;
            TaxPercent          = taxPercent;
            BillPaymentHeader   = billPaymentHeader;
        }

        public void EditBillPaymentEdit(Code code
            , GeneralText concept
            , int billInvoiceHeaderId
            , decimal amount
            , decimal qty
            , decimal taxPercent
            , BillPaymentHeader billPaymentHeader)
        {
            Code                = code;
            Amount              = amount;
            Qty                 = qty;
            BillInvoiceHeaderId = billInvoiceHeaderId;
            TaxPercent          = taxPercent;
            Concept             = concept;
            BillPaymentHeader   = billPaymentHeader;

        }

    }
}