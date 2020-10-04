using ProjectF.Api.Features.Product;
using ProjectF.Data.Entities.Products;
using ProjectF.Data.Entities.Werehouses;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ProjectF.Api.Features.Werehouses
{
    public class WerehouseViewModel
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public WerehouseDto ToDto() => new WerehouseDto(Id, Code, Name, Location);
        public static WerehouseViewModel FromDto(WerehouseDto werehouse)
            => new WerehouseViewModel()
            {
                Id = werehouse.Id,
                Code = werehouse.Code,
                Name = werehouse.Name,
                Location = werehouse.Location
            };

    }
}
