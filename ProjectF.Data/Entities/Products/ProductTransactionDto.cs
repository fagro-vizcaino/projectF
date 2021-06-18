using ProjectF.Data.Entities.Core;
using ProjectF.Data.Entities.DocumentTypes;

namespace ProjectF.Data.Entities.Products
{
    public record ProductTransactionDto(DocumentType DocumentType
        , int TransactinId
        , int ProductId
        , decimal Qty
        , decimal Amount
        , int WarehouseId
        , decimal UnitOfMeasureId): FDto;
}
