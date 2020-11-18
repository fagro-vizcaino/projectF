using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;

namespace ProjectF.WebUI.Pages.Werehouses
{
    public class WarehouseContainerHandler : BaseContainerBasicCrud<Warehouse>
    {
        public WarehouseContainerHandler() : base("Almacen")
        {
            var emptyModel = new Warehouse
            {
                Id = 0,
                Name = null,
                Code = string.Empty,
                Location = string.Empty,
            };
            InitModel(emptyModel);
            NewOrEditOperation = GetNewModelOrEdit;
        }

        public Warehouse GetNewModelOrEdit(Warehouse werehouse = null)
          => werehouse != null
              ? new Warehouse
              {
                  Id = werehouse.Id,
                  Name = werehouse.Name,
                  Code = werehouse.Code,
                  Location = werehouse.Location

              }
              : new Warehouse
              {
                  Id = 0,
                  Name = null,
                  Code = GenerateCode,
                  Location = string.Empty
              };
    }
}
