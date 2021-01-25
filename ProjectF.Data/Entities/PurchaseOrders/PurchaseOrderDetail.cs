using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.PurchaseOrders
{
    public class PurchaseOrderDetail : _BaseEntity
    {
        public Code ProductCode { get; private set; }
        public Name Description { get; private set; }
        public decimal Cost { get; private set; }
        public decimal Qty { get; private set; }
        public decimal DiscountValue { get; private set; }
        public decimal TaxPercent { get; private set; }
        public PurchaseOrderHeader PurchaseOrderHeader { get; private set; }

        protected PurchaseOrderDetail() { }

        public PurchaseOrderDetail(Code productCode
            , Name productName
            , decimal cost
            , decimal qty
            , decimal discountValue
            , decimal taxpercent
            , PurchaseOrderHeader purchaseOrderHeader)
        {
            ProductCode         = productCode;
            Description         = productName;
            Cost                = cost;
            Qty                 = qty;
            DiscountValue       = discountValue;
            TaxPercent          = taxpercent;
            PurchaseOrderHeader = purchaseOrderHeader;
        }

        public void Deconstruct(out Code dproductCode
            , out Name dproductName
            , out decimal dqty
            , out decimal dcost
            , out decimal dtaxpercent
            , out decimal ddiscountValue
            , out PurchaseOrderHeader dpurchaseOrderHeader)
        {
            dproductCode         = ProductCode;
            dproductName         = Description;
            ddiscountValue       = DiscountValue;
            dqty                 = Qty;
            dcost                = Cost;
            dtaxpercent          = TaxPercent;
            dpurchaseOrderHeader = PurchaseOrderHeader;
        }


        public void EditPurchaseOrderDetail(Code productCode
            , Name productName
            , decimal qty
            , decimal cost
            , decimal discountValue
            , decimal taxpercent
            , PurchaseOrderHeader purchaseOrderHeader)
        {
            ProductCode         = productCode;
            Description         = productName;
            DiscountValue       = discountValue;
            Qty                 = qty;
            Cost                = cost;
            TaxPercent          = taxpercent;
            PurchaseOrderHeader = purchaseOrderHeader;
        }
    }
}
