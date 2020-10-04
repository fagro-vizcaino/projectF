using ProjectF.Api.Features.Category;
using ProjectF.Api.Features.Werehouses;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Products;
using ProjectF.Data.Entities.Werehouses;

namespace ProjectF.Api.Features.Product
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public CategoryViewModel Category { get; set; }
        public WerehouseViewModel Werehouse { get; set; }
        public bool IsService { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal Price2 { get; set; }
        public decimal Price3 { get; set; }
        public decimal Price4 { get; set; }
        public ProductDto ToDto()
        {
            var category = new Data.Entities.Categories.Category(
                new Code(Category.Code),
                new Name(Category.Name),
                Category.ShowOn);

            var werehouse = new Werehouse(new Code(Werehouse.Code)
                , new Name(Werehouse.Name)
                , Werehouse.Location);

            return new ProductDto(Id
               , Code
               , Name
               , Description
               , Reference
               , category
               , Category.Id
               , werehouse
               , Werehouse.Id
               , IsService
               , Cost
               , Price);
        }

        public static ProductViewModel FromDto(ProductDto productDto)
        {
            var category = new CategoryViewModel()
            {
                Code   = productDto.Category.Code.Value,
                Name   = productDto.Category.Name.Value,
                Id     = productDto.Category.Id,
                ShowOn = productDto.Category.ShowOn
            };

            var werehouse = new WerehouseViewModel()
            {
                Code     = productDto.Werehouse.Code.Value,
                Name     = productDto.Werehouse.Name.Value,
                Id       = productDto.Werehouse.Id,
                Location = productDto.Werehouse.Location,
            };

            return new ProductViewModel()
            {
                Id          = productDto.Id,
                Code        = productDto.Code,
                Name        = productDto.Name,
                Description = productDto.Description,
                Reference   = productDto.Reference,
                Category    = category,
                Werehouse   = werehouse,
                IsService   = productDto.IsService,
                Cost        = productDto.Cost,
                Price       = productDto.Price,
                Price2      = 0,
                Price3      = 0,
                Price4      = 0
            };
        }

    }
}
