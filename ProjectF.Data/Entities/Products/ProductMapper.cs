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
    }
}
