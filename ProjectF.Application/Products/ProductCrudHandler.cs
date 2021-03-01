using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System.Collections.Generic;
using ProjectF.Data.Products;
using ProjectF.Data.Entities.Products;
using static ProjectF.Application.Products.ProductMapper;
using System.Threading.Tasks;
using ProjectF.Data.Entities.RequestFeatures;
using System.Linq;
using ProjectF.Application.Common;

namespace ProjectF.Application.Products
{
    public class ProductCrudHandler : BaseCrudHandler<ProductDto, Product, ProductRepository>
    {
        readonly ProductRepository _productRepository;
        readonly CategoryRepository _categoryRepository;
        readonly WerehouseRepository _werehouseRepository;
        readonly TaxRepository _taxRepository;
        readonly UnitOfMeasureRepository _unitOfMeasureRepository;

        public ProductCrudHandler(ProductRepository productRepository,
            CategoryRepository categoryRepository,
            WerehouseRepository werehouseRepository,
            TaxRepository taxRepository,
            UnitOfMeasureRepository unitOfMeasureRepository) : base(productRepository)
        {
            _productRepository       = productRepository;
            _categoryRepository      = categoryRepository;
            _werehouseRepository     = werehouseRepository;
            _taxRepository           = taxRepository;
            _unitOfMeasureRepository = unitOfMeasureRepository;
            _fromDto                 = FromDto;
            _fromEntity              = FromEntity;
            _updateEntity            = UpdateEntity;
        }

        public Either<Error, Product> Create(CreateProductRequest req)
            => SetCategory(req)
            .Bind(SetWerehouse)
            .Bind(SetTax)
            .Bind(SetUniOfMeasure)
            .Map(FromRequestDto)
            .Bind(c => c.ToEither(() => Error.New("Fail mapping in FromRequestEntity")))
            .Bind(base.Create);

        public Either<Error, Product> Update(long id, CreateProductRequest req)
            => SetCategory(req)
            .Bind(SetWerehouse)
            .Bind(SetTax)
            .Map(FromRequestDto)
            .Bind(c => c.ToEither(() => Error.New("Fail mapping in from requestDto")))
            .Bind(c => base.Update(id, c));
      

        public Task<Option<ProductMainListDto>> GetProductList(ProductListParameters listParameters)
           => _productRepository.GetProductListAsync(listParameters, true)
           .MapT(c => new ProductMainListDto(c.Select(FromEntity).ToList(), c.MetaData));
         
        public Task<Option<ProductDto>> GetByKey(long id)
        => _productRepository.GetByKeys(id)
            .MapT(FromEntity);
          

        Either<Error, CreateProductRequest> SetCategory(CreateProductRequest req)
            => _categoryRepository
            .Find(req.CategoryId)
            .Match(Some: c => Right<Error, CreateProductRequest>(req with { Category = c }) , 
                    None: () => Error.New("couldn't find to category"));


        Either<Error, CreateProductRequest> SetWerehouse(CreateProductRequest req)
            => _werehouseRepository.Find(req.WarehouseId)
            .Match(Some: c => Right<Error, CreateProductRequest>(req with { Warehouse = c }),
                    None: () => Error.New("couldn't find to warehouse"));


        Either<Error, CreateProductRequest> SetTax(CreateProductRequest req)
            => _taxRepository.Find(req.TaxId)
            .Match(Some: c => Right<Error, CreateProductRequest>(req with { Tax = c }),
                    None: () => Error.New("couldn't find to tax"));

        Either<Error, CreateProductRequest> SetUniOfMeasure(CreateProductRequest req)
            => _unitOfMeasureRepository.Find(req.UnitOfMeasureId)
            .Match(Some: c => Right<Error, CreateProductRequest>(req with { UnitOfMeasure = c }),
                    None: () => Error.New("couldn't find to unitof measure"));

        Either<Error, Product> UpdateEntity(ProductDto dto, Product product)
        {
            var updateWith = FromDto(dto);

            product.EditProduct(updateWith.Code
                , updateWith.Name
                , updateWith.Description
                , updateWith.Reference
                , updateWith.Category
                , updateWith.Warehouse
                , updateWith.UnitOfMeasure
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
    }
}
