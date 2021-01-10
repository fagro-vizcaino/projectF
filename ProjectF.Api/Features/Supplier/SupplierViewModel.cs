using ProjectF.Data.Entities.Suppliers;
using ProjectF.Application.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Api.Features.Supplier
{
    public class SupplierViewModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Rnc { get; set; }
        public string HomeOrApartment { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int SelectedCountry { get; set; }
        public Data.Entities.Countries.Country? Country { get; set; }
        public bool IsIndependent { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public SupplierDto ToDto()
            => new SupplierDto(Id
                , Code
                , Name
                , Email
                , Phone
                , Rnc
                , HomeOrApartment
                , City
                , Street
                , SelectedCountry
                , Country
                , IsIndependent
                , Created
                , Modified
                , Status);


        public static SupplierViewModel FromDto(SupplierDto dto)
              => new SupplierViewModel()
              {
                  Id              = dto.Id,
                  Code            = dto.Code,
                  Name            = dto.Name,
                  Email           = dto.Email,
                  Phone           = dto.Phone,
                  Rnc             = dto.Rnc,
                  HomeOrApartment = dto.HomeOrApartment,
                  City            = dto.City,
                  Street          = dto.Street,
                  SelectedCountry = dto.SelectedCountry,
                  Country         = dto.Country,
                  IsIndependent   = dto.IsIndependent,
                  Created         = dto.Created,
                  Modified        = dto.Modified,
                  Status          = dto.Status,
              };

    }
}
