using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Products;
using ProjectF.Data.Entities.Products;
using static ProjectF.Data.Entities.Products.ProductMapper;
using System.Threading.Tasks;
using ProjectF.Data.Entities.RequestFeatures;
using System.Linq;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Application.Products
{
    public class ProductCrudHandler
    {
        readonly ProductRepository _productRepository;
        readonly CategoryRepository _categoryRepository;
        readonly WerehouseRepository _werehouseRepository;
        readonly TaxRepository _taxRepository;

        public ProductCrudHandler(ProductRepository productRepository,
            CategoryRepository categoryRepository,
            WerehouseRepository werehouseRepository,
            TaxRepository taxRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _werehouseRepository = werehouseRepository;
            _taxRepository = taxRepository;
        }

        public Either<Error, Product> Create(ProductDto productDto)
           => ValidateCode(productDto)
               .Bind(ValidateName)
               .Bind(ValidateCategory)
               .Bind(SetCategory)
               .Bind(ValidateWerehouse)
               .Bind(SetWerehouse)
               .Bind(ValidateTax)
               .Bind(SetTax)
               .Bind(c => Add(FromDto(c)))
               .Bind(Save);

        public Either<Error, Product> Update(long id, ProductDto productDto)
            => ValidateIsCorrectUpdate(id, productDto)
            .Bind(ValidateCode)
            .Bind(ValidateName)
            .Bind(ValidateCategory)
            .Bind(SetCategory)
            .Bind(ValidateWerehouse)
            .Bind(SetWerehouse)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(productDto, c))
            .Bind(Save);

        public IEnumerable<ProductDto> GetAll()
           => _productRepository.GetAll()
               .Map(ct => FromEntity(ct));

        public Task<Either<Error, (List<ProductDto> list, MetaData meta)>> GetProductList(ProductListParameters listParameters)
           => _productRepository.GetProductListAsync(listParameters, true)
           .MapT(c => (c.Select(i => FromEntity(i)).ToList(), c.MetaData));

        public Either<Error, Product> Find(params object[] valueKeys)
            => _productRepository.Find(valueKeys).Match(Some: t => t,
             None: Left<Error, Product>(Error.New("couldn't find Product type")));

        public Either<Error, Product> GetByKey(long id)
        {
            var product = _productRepository.GetByKeys(id);
            return product is null || product.Category is null || product.Warehouse is null
                ? Error.New("Product || category || werehouse is null")
                : (Either<Error, Product>)product;
        }

        public Either<Error, Product> Delete(long id)
          => Find(id)
            .Bind(Delete)
            .Bind(Save)
            .MapLeft(errors => Error.New(string.Join("; ", errors)));

        Either<Error, ProductDto> ValidateIsCorrectUpdate(long id, ProductDto productDto)
        {
            if (id == productDto.Id) return productDto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, ProductDto> ValidateCode(ProductDto productDto)
            => Code.Of(productDto.Code)
                .Match<Either<Error, ProductDto>>(
                    Left: err => Error.New(err.Message),
                    Right: c => productDto);

        Either<Error, ProductDto> ValidateName(ProductDto productDto)
            => Name.Of(productDto.Name).Match(Succ: c => productDto,
            Fail: err => Left<Error, ProductDto>(Error.New(string.Join(";", err))));

        Either<Error, ProductDto> ValidateCategory(ProductDto productDto)
        {
            if (productDto.Category == null)
                return Error.New("category is required");

            return productDto;
        }

        Either<Error, ProductDto> SetCategory(ProductDto productDto)
        {
            var dto = _categoryRepository.Find(productDto.CategoryId)
                .Match(Some: c => productDto with { Category = c }, None: productDto);

            if (dto.Category is null) return Error.New("couldn't find to category");

            return dto;
        }

        Either<Error, ProductDto> ValidateWerehouse(ProductDto productDto)
            => productDto.Warehouse is null
            ? Error.New("werehouse is required")
            : Right<Error, ProductDto>(productDto);

        Either<Error, ProductDto> SetWerehouse(ProductDto productDto)
        {
            var dto = _werehouseRepository.Find(productDto.CategoryId)
               .Match(Some: c => productDto with { Warehouse = c }, None: productDto);

            return dto.Warehouse is null
                ? Error.New("couldn't find to werehouse")
                : Right<Error, ProductDto>(dto);
        }

        Either<Error, ProductDto> ValidateTax(ProductDto productDto)
            => productDto.Tax is null
            ? Error.New("tax is required")
            : Right<Error, ProductDto>(productDto);

        Either<Error, ProductDto> SetTax(ProductDto productDto)
        {
            var dto = _taxRepository.Find(productDto.TaxId)
                .Match(t => productDto with { Tax = t }, () => productDto);

            return dto.Tax is null
                ? Error.New("couldn't find to tax")
                : Right<Error, ProductDto>(dto);
        }

        Either<Error, Product> UpdateEntity(ProductDto dto, Product product)
        {
            var updateWith = FromDto(dto);

            product.EditProduct(updateWith.Code
                , updateWith.Name
                , updateWith.Description
                , updateWith.Reference
                , updateWith.Category
                , updateWith.Warehouse
                , updateWith.Tax
                , updateWith.IsService
                , updateWith.Cost
                , updateWith.Price
                , 0
                , 0
                , 0
                , updateWith.Status);

            return product;
        }

        Either<Error, Product> Add(Product product)
        {
            try
            {
                _productRepository.Add(product);
                return product;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Product> Save(Product product)
        {
            try
            {
                _productRepository.Save();
                return product;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Product> Delete(Product supplier)
        {
            try
            {
                supplier.EditProduct(supplier.Code
                    , supplier.Name
                    , supplier.Description
                    , supplier.Reference
                    , supplier.Category
                    , supplier.Warehouse
                    , supplier.Tax
                    , supplier.IsService
                    , supplier.Cost
                    , supplier.Price
                    , 0
                    , 0
                    , 0
                    , EntityStatus.Deleted);
                return supplier;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
