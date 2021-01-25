using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Products;

namespace ProjectF.Data.Entities.Products
{
    public static class ProductMapper
    {
        public static Product FromDto(ProductDto dto)
            => new Product(new Code(dto.Code)
                , new Name(dto.Name)
                , new GeneralText(dto.Description)
                , new GeneralText(dto.Reference)
                , dto.Category
                , dto.Warehouse
                , dto.Tax
                , dto.IsService
                , dto.Cost
                , dto.Price
                , 0
                , 0
                , 0
                , dto.Created
                , dto.Status);

        public static ProductDto FromEntity(Product entity)
            => new ProductDto(entity.Id
                , entity.Code.Value
                , entity.Name.Value
                , entity.Description.Value
                , entity.Reference.Value
                , entity.Category
                , entity.Category.Id
                , entity.Warehouse
                , entity.Warehouse.Id
                , entity.Tax?.Id ?? 0
                , entity.Tax
                , entity.IsService
                , entity.Cost
                , entity.Price
                , entity.Created
                , entity.Modified
                , entity.Status);


        public static ProductTransaction FromDto(ProductTransactionDto dto)
            => new ProductTransaction(dto.DocumentType
                , dto.TransactinId
                , dto.ProductId
                , dto.Qty
                , dto.Amount
                , dto.WarehouseId
                , dto.UnitOfMeasureId
                , dto.Created
                , dto.Status);

        public static ProductTransactionDto FromEntity(ProductTransaction entity)
            => (new ProductTransactionDto(entity.DocumentType
                , entity.TransactionId
                , entity.ProductId
                , entity.Qty
                , entity.Amount
                , entity.WarehouseId
                , entity.UnitOfMeasureId)) with { Id = entity.Id
                , Status = entity.Status
                , Created = entity.Created
                , Modified = entity.Modified};
    }
}
