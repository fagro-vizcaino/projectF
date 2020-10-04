using ProjectF.Data.Entities.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectF.Data.Entities.Taxes.BusinessTaxRegimeType
{
    public class TaxRegimeDto
    {
         public long Id { get; set; }
        public string Name { get; set;}
        public string Description { get;  set;}


        public TaxRegimeDto(long id
            , string name
            , string description)
        {
            Id           = id;
            Name         = name;
            Description  = description;
        }

        public TaxRegimeDto With(long? id = null
            , string? name = null
            , string? description = null)
            => new TaxRegimeDto(id ?? this.Id
                , name ?? this.Name
                , description ?? this.Description);

           public static implicit operator TaxRegimeType(TaxRegimeDto dto)
            =>  new TaxRegimeType(new Name(dto.Name), new GeneralText(dto.Description));
    }
}
