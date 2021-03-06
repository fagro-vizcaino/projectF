﻿using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;

namespace ProjectF.WebUI.Pages.Taxes
{
    public class TaxesContainerHandler : BaseContainerBasicCrud<Tax>
    {
        public TaxesContainerHandler() : base("Impuesto")
        {
            var emptyModel = new Tax
            {
                Id = 0,
                Code = string.Empty,
                Name =string.Empty,
                PercentValue =0
            };
            InitModel(emptyModel);
            NewOrEditOperation = GetNewModelOrEdit;   
        }

        public Tax GetNewModelOrEdit(Tax tax = null)
            => tax !=null 
            ? new Tax
            {
                Id = tax.Id,
                Name = tax.Name,
                Code = tax.Code,
                PercentValue = tax.PercentValue
            }
            : new Tax { Id = 0, Name = null, Code = GenerateCode, PercentValue = 0};
    }
}
