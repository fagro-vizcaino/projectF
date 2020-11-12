using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Products;
using ProjectF.Data.Entities.Products;
using Microsoft.EntityFrameworkCore.Infrastructure;

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
            _productRepository   = productRepository;
            _categoryRepository  = categoryRepository;
            _werehouseRepository = werehouseRepository;
            _taxRepository       = taxRepository;
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
               .Bind(CreateEntity)
               .Bind(Add)
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
               .Map(ct => new ProductDto(ct.Id,
                   ct.Code.Value,
                   ct.Name.Value,
                   ct.Description.Value,
                   ct.Reference.Value,
                   ct.Category,
                   ct.Category.Id,
                   ct.Werehouse,
                   ct.Werehouse.Id,
                   ct.Tax?.Id ?? 0,
                   ct.Tax,
                   ct.IsService,
                   ct.Cost,
                   ct.Price));

        public Either<Error, Product> Find(params object[] valueKeys)
            => _productRepository.Find(valueKeys).Match(Some: t => t,
             None: Left<Error, Product>(Error.New("couldn't find Product type")));

        public Either<Error, Product> GetByKey(long id)
        {
            var product = _productRepository.GetByKeys(id);
            if (product == null || product.Category == null || product.Werehouse == null)
                return Error.New("Product || category || werehouse is null");

            return product;
        }

        public ProductDto EntityToDto(Product product)
           => new ProductDto(product.Id
               , product.Code.Value
               , product.Name.Value
               , product.Description.Value
               , product.Reference.Value
               , product.Category
               , product.Category.Id
               , product.Werehouse
               , product.Werehouse.Id
               , product.Tax.Id
               , product.Tax
               , product.IsService
               , product.Cost
               , product.Price);

        //Missing Pagination
        public Either<Error, Product> Delete(long id)
          => Find(id)
            .Bind(Delete)
            .Bind(Save)
            .MapLeft(errors => Error.New(string.Join("; ", errors)));

        Either<Error, Product> CreateEntity(ProductDto productDto)
        {
            var code = new Code(productDto.Code);
            var name = new Name(productDto.Name);
            var description = new GeneralText(productDto.Description);
            var reference = new GeneralText(productDto.Reference);
            var altReference = 0;
            var product = new Product(code
                , name
                , description
                , reference
                , productDto.Category
                , productDto.Werehouse
                , productDto.Tax
                , productDto.IsService
                , productDto.Cost
                , productDto.Price
                , altReference
                , altReference
                , altReference);

            return product;
        }

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
            Fail: err => Left<Error, ProductDto>(Error.New(string.Join(";",err))));

        Either<Error, ProductDto> ValidateCategory(ProductDto productDto)
        {
            if (productDto.Category == null)
                return Error.New("category is required");

            return productDto;
        }

        Either<Error, ProductDto> SetCategory(ProductDto productDto)
        {
            var dto = _categoryRepository.Find(productDto.CategoryId)
                .Match(Some: c => productDto.With(category: c), None: productDto);

            if (dto.Category == null) return Error.New("couldn't find to category");

            return dto;
        }

        Either<Error, ProductDto> ValidateWerehouse(ProductDto productDto)
            => productDto.Werehouse == null
            ? Error.New("werehouse is required")
            : Right<Error, ProductDto>(productDto);

        Either<Error, ProductDto> SetWerehouse(ProductDto productDto)
        {
            var dto = _werehouseRepository.Find(productDto.CategoryId)
               .Match(Some: c => productDto.With(werehouse: c), None: productDto);

            return dto.Werehouse == null
                ? Error.New("couldn't find to werehouse")
                : Right<Error, ProductDto>(dto);
        }

        Either<Error, ProductDto> ValidateTax(ProductDto productDto)
            => productDto.Tax == null
            ? Error.New("tax is required")
            : Right<Error, ProductDto>(productDto);

        Either<Error, ProductDto> SetTax(ProductDto productDto)
        {
            var dto = _taxRepository.Find(productDto.TaxId)
                .Match(t => productDto.With(tax: t), () => productDto);

            return dto.Tax == null 
                ? Error.New("couldn't find to tax")
                : Right<Error, ProductDto>(dto);
        }

        Either<Error, Product> UpdateEntity(ProductDto productDto, Product product)
        {
            var code = new Code(productDto.Code);
            var name = new Name(productDto.Name);
            var description = new GeneralText(productDto.Description);
            var reference = new GeneralText(productDto.Reference);
            var altPrice = 0;

            product.EditProduct(code
                , name
                , description
                , reference
                , productDto.Category
                , productDto.Werehouse
                , productDto.Tax
                , productDto.IsService
                , productDto.Cost
                , productDto.Price
                , altPrice
                , altPrice
                , altPrice);

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
                _productRepository.Delete(supplier);
                return supplier;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
