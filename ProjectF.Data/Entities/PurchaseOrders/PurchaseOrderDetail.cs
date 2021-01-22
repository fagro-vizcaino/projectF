using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.PurchaseOrders
{
    public class PurchaseOrderDetail : _BaseEntity
    {
        public Code ProductCode { get; private set; }
        public Name ProductName { get; private set; }
        public Name Warehouse { get; private set; }
        public int WarehouseId { get; private set; }
        public decimal Qty { get; private set; }
        public decimal Cost { get; private set; }
        public decimal TaxPercent { get; private set; }
        public PurchaseOrderHeader PurchaseOrderHeader { get; private set; }

        public PurchaseOrderDetail() { }

        public PurchaseOrderDetail(Code productCode
            , Name productName
            , Name warehouse
            , int warehouseId
            , decimal qty
            , decimal cost
            , decimal taxpercent
            , PurchaseOrderHeader purchaseOrderHeader)
        {
            ProductCode         = productCode;
            ProductName         = productName;
            Warehouse           = warehouse;
            WarehouseId         = warehouseId;
            Qty                 = qty;
            Cost                = cost;
            TaxPercent          = taxpercent;
            PurchaseOrderHeader = purchaseOrderHeader;
        }

        public void Deconstruct(out Code dproductCode
            , out Name dproductName
            , out Name dwarehouse
            , out int dwarehouseId
            , out decimal dqty
            , out decimal dcost
            , out decimal dtaxpercent
            , out PurchaseOrderHeader dpurchaseOrderHeader)
        {
            dproductCode         = ProductCode;
            dproductName         = ProductName;
            dwarehouse           = Warehouse;
            dwarehouseId         = WarehouseId;
            dqty                 = Qty;
            dcost                = Cost;
            dtaxpercent          = TaxPercent;
            dpurchaseOrderHeader = PurchaseOrderHeader;
        }


        public void EditPurchaseOrderDetail(Code productCode
            , Name productName
            , Name warehouse
            , int warehouseId
            , decimal qty
            , decimal cost
            , decimal taxpercent
            , PurchaseOrderHeader purchaseOrderHeader)
        {
            ProductCode         = productCode;
            ProductName         = productName;
            Warehouse           = warehouse;
            WarehouseId         = warehouseId;
            Qty                 = qty;
            Cost                = cost;
            TaxPercent          = taxpercent;
            PurchaseOrderHeader = purchaseOrderHeader;
        }
    }
}
