using ProjectF.Data.Entities.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectF.Data.Entities.Taxes
{
    public class TaxDto
    {
        public long Id { get; set; }
        public string Name { get; }
        public decimal PercentValue { get; }


        public TaxDto(long id
            , string name
            , decimal percentValue)
        {
            Id           = id;
            Name         = name;
            PercentValue = percentValue;
        }

        public TaxDto With(long? id = null
            , string? name = null
            , decimal? percentValue = null)
            => new TaxDto(id ?? this.Id
                , name ?? this.Name
                , percentValue ?? this.PercentValue);


        public static implicit operator Tax(TaxDto dto)
            =>  new Tax(new Name(dto.Name), dto.PercentValue);
    }
}
