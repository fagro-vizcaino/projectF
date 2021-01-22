using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PaymentList;
using ProjectF.Data.Entities.Suppliers;
using ProjectF.Data.Entities.Taxes;

namespace ProjectF.Data.Entities.PurchaseOrders
{
    public class PurchaseOrderHeader : _BaseEntity
    {
        public Code Code { get; private set; }
        public Name SupplierName { get; private set; }
        public string Rnc { get; private set; }
        public GeneralText Address { get; private set; }
        public DateTime DeliverDate { get; private set; }
        public PaymentTerm PaymentTerm { get; private set; }
        public GeneralText Notes { get; private set; }

        private readonly List<PurchaseOrderDetail> _purchaseOrderDetails = new List<PurchaseOrderDetail>();
        public  IReadOnlyList<PurchaseOrderDetail> PurchaseDetails => _purchaseOrderDetails.ToList();
        public PurchaseOrderHeader() { }

        public PurchaseOrderHeader(Code code
            , Name supplierName
            , string rnc
            , GeneralText address
            , DateTime deliverDate
            , PaymentTerm paymentTerm
            , Tax tax
            , GeneralText note
            , List<PurchaseOrderDetail> purchaseOrderDetails
            , DateTime created
            , EntityStatus status = EntityStatus.Active)
        {
            Code            = code;
            SupplierName    = supplierName;
            Rnc             = rnc;
            Address         = address;
            DeliverDate     = deliverDate;
            PaymentTerm     = paymentTerm;
            Notes           = note;
            Status          = status;
            Created         = created == DateTime.MinValue ? DateTime.UtcNow : created;
            _purchaseOrderDetails.AddRange(CreateDetails(purchaseOrderDetails));
        }


        IEnumerable<PurchaseOrderDetail> CreateDetails(IEnumerable<PurchaseOrderDetail> purchaseOrderDetails)
        {
            var purchaseOrderHeader = this;
            return purchaseOrderDetails.Map(d => new PurchaseOrderDetail());
        }

        public void EditPurchaseOrderHeader(Code code
            , Name supplierName
            , string rnc
            , GeneralText address
            , DateTime deliverDate
            , PaymentTerm paymentTerm
            , Tax tax
            , GeneralText note
            , List<PurchaseOrderDetail> purchaseOrderDetails
            , EntityStatus status = EntityStatus.Active)
        {
            var header = this;
            var _localpurchaseOrder = purchaseOrderDetails.Map(d =>
                new PurchaseOrderDetail());

            Code            = code;
            SupplierName    = supplierName;
            Rnc             = rnc;
            Address         = address;
            DeliverDate     = deliverDate;
            PaymentTerm     = paymentTerm;
            Notes           = note;
            Status          = status;
            Modified        = DateTime.UtcNow;
            _purchaseOrderDetails.AddRange(_localpurchaseOrder);
        }
    }
}
