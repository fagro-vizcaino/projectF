using System;
using System.Collections.Generic;
using System.Linq;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.BillsPayment
{
    public class BillPaymentHeader : _BaseEntity
    {
        /// <summary>
        /// NCf
        /// </summary>
        public Code Sequence { get; private set; }
        public int SequenceId { get; private set; }
        public DateTime BillPaymentDate { get; private set; }
        public Code SupplierCode { get; private set; }
        public Name SupplierName { get; private set; }
        public Code BankAccountId { get; private set; }
        public Name BankAccount { get; private set; }
        public Code Reference { get; private set; }
        public GeneralText Notes { get; private set; }
        public decimal Subtotal { get; private set; }
        public decimal TaxTotal { get; private set; }
        public decimal DiscountTotal { get; private set; }
        public decimal Total { get; private set; }

        private readonly List<BillPaymentDetail> _billPaymentDetails = new();
        public IReadOnlyList<BillPaymentDetail> BillPaymentDetails => _billPaymentDetails.ToList();

        public BillPaymentHeader() { }

        public BillPaymentHeader(Code sequence
            , int sequenceId
            , DateTime billPaymentDate 
            , Code supplierCode 
            , Name supplierName 
            , Code bankAccountId 
            , Name bankAccount 
            , Code reference 
            , GeneralText notes 
            , decimal subtotal 
            , decimal taxTotal 
            , decimal discountTotal 
            , decimal total
            , List<BillPaymentDetail> billPaymentDetails
            , DateTime created
            , EntityStatus status = EntityStatus.Active)
        {
            Sequence        = sequence;
            SequenceId      = sequenceId;
            BillPaymentDate = billPaymentDate;
            SupplierCode    = supplierCode;
            SupplierName    = supplierName;
            BankAccountId   = bankAccountId;
            BankAccount     = bankAccount;
            Reference       = reference;
            Notes           = notes;
            Subtotal        = subtotal;
            TaxTotal        = taxTotal;
            DiscountTotal   = discountTotal;
            Total           = total;
            Status          = status;
            Created         = created == DateTime.MinValue ? DateTime.UtcNow : created;

            _billPaymentDetails.AddRange(CreateDetails(billPaymentDetails));
        }

        IEnumerable<BillPaymentDetail> CreateDetails(List<BillPaymentDetail> billPaymentDetails)
        {
            var header = this;
            return billPaymentDetails.Map(c => new BillPaymentDetail(c.Code
                , c.Concept
                , c.BillInvoiceHeaderId
                , c.Amount
                , c.Qty
                , c.TaxPercent
                , header));
        }

        public void EditBillPaymentHeader(Code sequence
            , int sequenceId
            , DateTime billPaymentDate
            , Code supplierCode
            , Name supplierName
            , Code bankAccountId
            , Name bankAccount
            , Code reference
            , GeneralText notes
            , decimal subtotal
            , decimal taxTotal
            , decimal discountTotal
            , decimal total
            , List<BillPaymentDetail> billPaymentDetails
            , EntityStatus status = EntityStatus.Active) 
            {
            var header = this;
            var _localBillPaymentDetals = billPaymentDetails.Map(d =>
                new BillPaymentDetail(d.Code
                , d.Concept
                , d.BillInvoiceHeaderId
                , d.Amount
                , d.Qty
                , d.TaxPercent
                , header));

            Sequence        = sequence;
            SequenceId      = sequenceId;
            BillPaymentDate = billPaymentDate;
            SupplierCode    = supplierCode;
            SupplierName    = supplierName;
            BankAccountId   = bankAccountId;
            BankAccount     = bankAccount;
            Reference       = reference;
            Notes           = notes;
            Subtotal        = subtotal;
            TaxTotal        = taxTotal;
            DiscountTotal   = discountTotal;
            Total           = total;
            Status          = status;
            Modified        = DateTime.UtcNow;
        }
    }
}
