using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.WebUI.Components.Common;

namespace ProjectF.WebUI.Pages.UnitOfMeasures
{
    public class UnitOfMeasureContainerHandler : BaseContainerBasicCrud<UnitOfMeasure>
    {
        public UnitOfMeasureContainerHandler() : base("Unidad de medida")
        {
            var emptyModel = new UnitOfMeasure
            {
                Id = 0,
                Name = string.Empty
            };
            InitModel(emptyModel);
            NewOrEditOperation = GetNewModelOrEdit;
        }

        public UnitOfMeasure GetNewModelOrEdit(UnitOfMeasure unitOfMeasure = null)
            => unitOfMeasure != null 
            ? new UnitOfMeasure
            {
                Id = unitOfMeasure.Id,
                Name = unitOfMeasure.Name
            }
            : new UnitOfMeasure { Id = 0, Name = string.Empty };
    }
}
