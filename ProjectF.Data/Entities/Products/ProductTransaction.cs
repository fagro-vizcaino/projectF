using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.DocumentTypes;

namespace ProjectF.Data.Entities.Products
{
    public class ProductTransaction : _BaseEntity
    {
        public DocumentType DocumentType { get; private set; }
        public int TransactionId { get; private set; }
        public int ProductId { get; private set; }
        public decimal Qty { get; private set; }
        public decimal Amount { get; private set; }
        public int WarehouseId { get; private set; }
        public decimal UnitOfMeasureId { get; private set; }

        protected ProductTransaction() { }

        public ProductTransaction(DocumentType documentType
            , int transactionId
            , int productId
            , decimal qty
            , decimal amount
            , int warehouseId
            , decimal unitOfMeasureId
            , DateTime created
            , EntityStatus status = EntityStatus.Active)
        {
            DocumentType    = documentType;
            TransactionId   = transactionId;
            ProductId       = productId;
            Qty             = qty;
            Amount          = amount;
            WarehouseId     = warehouseId;
            UnitOfMeasureId = unitOfMeasureId;
            Created         = created == DateTime.MinValue ? DateTime.UtcNow : created;
            Status          = status;
        }
    }
}
