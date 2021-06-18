using System.Collections.Generic;
using ProjectF.Application.Common;
using ProjectF.Data.Entities.Products;
using ProjectF.Data.Entities.RequestFeatures;

namespace ProjectF.Application.Products
{
    public class ProductMainListDto : IMainList<ProductDto>
    {
        public ProductMainListDto(IList<ProductDto> list, MetaData meta)
            => (List, Meta) = (list, meta);
        public IList<ProductDto> List { get; }
        public MetaData Meta { get; }
    }
}