using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Products;
using System;
using System.Collections.Generic;

namespace ProjectF.Data.Entities.Werehouses
{
    public class WerehouseDto
    {
        public long Id { get; set; }
        public string Code { get; }
        public string Name { get; }
        public string Location { get; }
        public IReadOnlyList<Product> Products { get; set; }

        public WerehouseDto(long id, string code, string name, string location)
        {
            Id = id;
            Code = code;
            Name = name;
            Location = location;
        }

        public WerehouseDto With(long? id = null
            , string? code = null
            , string? name = null
            , string? location = null)
            => new WerehouseDto(id ?? this.Id
                , code ?? this.Code
                , name ?? this.Name
                , location ?? this.Location);

        public static implicit operator Werehouse(WerehouseDto dto)
            => new Werehouse(new Code(dto.Code), new Name(dto.Name), dto.Location);
    }
}
