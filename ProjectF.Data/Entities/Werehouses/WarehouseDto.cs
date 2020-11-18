using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Products;
using System;
using System.Collections.Generic;

namespace ProjectF.Data.Entities.Warehouses
{
    public class WarehouseDto
    {
        public long Id { get; set; }
        public string Code { get; }
        public string Name { get; }
        public string Location { get; }
        public IReadOnlyList<Product> Products { get; set; }

        public WarehouseDto(long id, string code, string name, string location)
        {
            Id = id;
            Code = code;
            Name = name;
            Location = location;
        }

        public WarehouseDto With(long? id = null
            , string? code = null
            , string? name = null
            , string? location = null)
            => new WarehouseDto(id ?? this.Id
                , code ?? this.Code
                , name ?? this.Name
                , location ?? this.Location);

        public static implicit operator Warehouse(WarehouseDto dto)
            => new Warehouse(new Code(dto.Code), new Name(dto.Name), dto.Location);
    }
}
