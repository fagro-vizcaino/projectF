using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Werehouses;
using System.Security.Principal;

namespace ProjectF.Data.Entities.Products
{
    public class ProductDto
    {
        public long Id { get;}
        public string Code { get;}
        public string Name { get; }
        public string Description { get;}
        public string Reference { get; }
        public Category Category { get; }
        public long CategoryId { get; }
        public Werehouse Werehouse { get; }
        public long WerehouseId { get;  }
        public bool IsService { get; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }

        public ProductDto(long id,
            string code,
            string name, 
            string description,
            string reference,
            Category category,
            long categoryId,
            Werehouse werehouse,
            long werehouseId,
            bool isService,
            decimal cost,
            decimal price)
        {
            Id          = id;
            Code        = code;
            Name        = name;
            Description = description;
            Reference   = reference;
            Category    = category;
            Werehouse   = werehouse;
            IsService   = isService;
            Cost        = cost;
            Price       = price;
            WerehouseId = werehouseId;
            CategoryId  = categoryId;
        }


        public ProductDto With(long? id = null
            , string? code              = null
            , string? name              = null
            , string? description       = null
            , string? reference         = null
            , Category? category        = null
            , Werehouse? werehouse      = null
            , bool? isService           = null
            , decimal? cost             = null
            , decimal? price            = null
            , long? categoryId          = null
            , long? werehouseId         = null)
                                        => 
            new ProductDto(id ?? this.Id, 
                code ?? this.Code,
                name ?? this.Name,
                description ?? this.Description,
                reference ?? this.Reference,
                category ?? this.Category,
                categoryId ?? this.CategoryId,
                werehouse ?? this.Werehouse,
                werehouseId ?? this.WerehouseId,
                isService ?? this.IsService,
                cost ?? this.Cost,
                price ?? this.Price);

    }
}