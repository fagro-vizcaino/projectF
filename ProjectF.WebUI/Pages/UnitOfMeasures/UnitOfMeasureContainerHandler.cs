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
                Name = string.Empty,
                Value = 1
            };
            InitModel(emptyModel);
            NewOrEditOperation = GetNewModelOrEdit;
        }

        public UnitOfMeasure GetNewModelOrEdit(UnitOfMeasure unitOfMeasure = null)
            => unitOfMeasure != null 
            ? new UnitOfMeasure
            {
                Id = unitOfMeasure.Id,
                Name = unitOfMeasure.Name,
                Value = unitOfMeasure.Value
            }
            : new UnitOfMeasure { Id = 0, Name = string.Empty, Value = 1 };
    }
}
