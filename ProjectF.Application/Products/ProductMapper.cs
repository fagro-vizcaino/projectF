using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using ProjectF.Application.Categories;
using ProjectF.Application.Taxes;
using ProjectF.Application.UnitOfMeasures;
using ProjectF.Application.Warehouses;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Products;
using ProjectF.Data.Entities.Taxes;
using ProjectF.Data.Products;

namespace ProjectF.Application.Products
{
    public static class ProductMapper
    {
        public static Product FromDto(ProductDto dto)
            => new(new Code(dto.Code)
                , new Name(dto.Name)
                , new GeneralText(dto.Description)
                , new GeneralText(dto.Reference)
                , CategoryMapper.FromDto(dto.Category)
                , WarehouseMapper.FromDto(dto.Warehouse)
                , UnitOfMeasureMapper.FromDto(dto.UnitOfMeasure)
                , TaxMapper.FromDto(dto.Tax)
                , dto.IsService
                , dto.Cost
                , dto.Price
                , 0
                , 0
                , 0
                , dto.Created
                , dto.Modified
                , dto.Status);

        public static ProductDto FromEntity(Product entity)
            => new ProductDto(entity.Code.Value
                , entity.Name.Value
                , entity.Description.Value
                , entity.Reference.Value
                , CategoryMapper.FromEntity(entity.Category)
                , WarehouseMapper.FromEntity(entity.Warehouse)
                , UnitOfMeasureMapper.FromEntity(entity.UnitOfMeasure)
                , TaxMapper.FromEntity(entity.Tax)
                , entity.IsService
                , entity.Cost
                , entity.Price)
            with
            {
                Modified = entity.Modified,
                Created = entity.Created,
                Status = entity.Status,
                Id = entity.Id
            };

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
                , entity.UnitOfMeasureId))
            with
            {
                Id = entity.Id,
                Status = entity.Status,
                Created = entity.Created,
                Modified = entity.Modified
            };

        public static Option<ProductDto> FromRequestDto(CreateProductRequest request)
            => request.Category is not null && request.Warehouse is not null
              && request.Tax is not null && request.UnitOfMeasure is not null
            ? new ProductDto(request.Code,
                request.Name,
                request.Description,
                request.Reference,
                CategoryMapper.FromEntity(request.Category),
                WarehouseMapper.FromEntity(request.Warehouse),
                UnitOfMeasureMapper.FromEntity(request.UnitOfMeasure),
                TaxMapper.FromEntity(request.Tax),
                request.IsService,
                request.Cost,
                request.Price)
            with
            { Created = request.Created, Modified = request.Modified, Status = request.Status }
            : Option<ProductDto>.None;

        public static Option<Product> FromRequestEntity(CreateProductRequest req)
            => req.Category is not null && req.Warehouse is not null
              && req.Tax is not null && req.UnitOfMeasure is not null
            ? new Product(new Code(req.Code)
                , new Name(req.Name)
                , new GeneralText(req.Description)
                , new GeneralText(req.Reference)
                , req.Category
                , req.Warehouse
                , req.UnitOfMeasure
                , req.Tax
                , req.IsService
                , req.Cost
                , req.Price
                , req.Price2
                , req.Price3
                , req.Price4
                , req.Created
                , req.Modified
                , req.Status)
            : Option<Product>.None;
    }
}
