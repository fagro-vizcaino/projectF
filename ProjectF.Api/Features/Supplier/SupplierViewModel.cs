﻿using ProjectF.Data.Entities.Suppliers;
using ProjectF.Application.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public string HomeOrApartment { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int SelectedCountry { get; set; }
        public ProjectF.Data.Entities.Countries.Country Country { get; set; }
        public bool IsIndependent { get; set; }


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
                , IsIndependent);


        public static SupplierViewModel FromDto(SupplierDto supplierDto)
              => new SupplierViewModel()
              {
                  Id              = supplierDto.Id,
                  Code            = supplierDto.Code,
                  Name            = supplierDto.Name,
                  Email           = supplierDto.Email,
                  Phone           = supplierDto.Phone,
                  Rnc             = supplierDto.Rnc,
                  HomeOrApartment = supplierDto.HomeOrApartment,
                  City            = supplierDto.City,
                  Street          = supplierDto.Street,
                  SelectedCountry = supplierDto.SelectedCountry,
                  Country         = supplierDto.Country,
                  IsIndependent   = supplierDto.IsIndependent
              };

    }
}
