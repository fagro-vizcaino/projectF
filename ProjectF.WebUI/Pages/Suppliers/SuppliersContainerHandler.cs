using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Suppliers
{
    public class SupplierContainerHandler : BaseContainerBasicCrud<Supplier>
    {
        public SupplierContainerHandler() : base("Suplidor")
        {
            var emtpyModel = new Supplier
            {
                 Id                 = 0
                , Code              = GenerateCode
                , City              = string.Empty
                , Email             = string.Empty
                , Rnc               = string.Empty
                , Phone             = string.Empty
                , Street            = string.Empty
                , Notes             = string.Empty
                , HomeOrApartment   = string.Empty
                , Name              = string.Empty
                , Created           = DateTime.UtcNow
            };
            InitModel(emtpyModel);
            NewOrEditOperation = GetNewModelOrEdit;
        }

        public int EmptyDefaultCostAccount { get; set; }

        [Inject]
        public IBaseDataService<Country> CountryDataService { get; set; }
        public Country[] Countries { get; set; } = Array.Empty<Country>();
        
        [Inject]
        public IBaseDataService<PaymentTerm> PaymentTermDataService { get; set; }
        public PaymentTerm[] PaymentTerms { get; set; } = Array.Empty<PaymentTerm>();
        protected override async Task OnInitializedAsync()
        {
            Elements = (await DataService.GetAll()).ToArray();

            Countries = (await CountryDataService.GetAll()).ToArray();
            PaymentTerms = (await PaymentTermDataService.GetAll()).ToArray();
        }

        public Supplier GetNewModelOrEdit(Supplier supplier = null)
            => supplier is not null 
            ? new Supplier
            {
                Id              = supplier.Id,
                Name            = supplier.Name,
                Code            = supplier.Code,
                City            = supplier.City,
                SelectedCountry = supplier.SelectedCountry,
                Email           = supplier.Email,
                Country         = supplier.Country,
                IsIndependent   = supplier.IsIndependent,
                PaymentTermId   = supplier.PaymentTermId,
                PaymentTerm     = supplier.PaymentTerm,
                Rnc             = supplier.Rnc,
                HomeOrApartment = supplier.HomeOrApartment,
                Phone           = supplier.Phone,
                Street          = supplier.Street,
                Created         = supplier.Created,
                Modified        = supplier.Modified
            }
            : new Supplier 
            {    
                 Id               = 0
                , Name            = null
                , Code            = GenerateCode
                , Email           = string.Empty
                , Rnc             = string.Empty
                , Notes           = string.Empty
                , Phone           = string.Empty
                , Street          = string.Empty
                , City            = string.Empty
                , HomeOrApartment = string.Empty
                , Created         = DateTime.UtcNow
             };
    }
}
