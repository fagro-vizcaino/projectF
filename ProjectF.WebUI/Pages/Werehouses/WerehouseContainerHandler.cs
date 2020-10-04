using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using System.Linq;

namespace ProjectF.WebUI.Pages.Werehouses
{
    public class WerehouseContainerHandler : BaseContainerBasicCrud<Werehouse>
    {
        public WerehouseContainerHandler() : base("Almacen")
        {
            var emptyModel = new Werehouse
            {
                Id = 0,
                Name = null,
                Code = string.Empty,
                Location = string.Empty,
            };
            InitModel(emptyModel);
            NewOrEditOperation = GetNewModelOrEdit;
        }

        public Werehouse GetNewModelOrEdit(Werehouse werehouse = null)
          => werehouse != null
              ? new Werehouse
              {
                  Id = werehouse.Id,
                  Name = werehouse.Name,
                  Code = werehouse.Code,
                  Location = werehouse.Location

              }
              : new Werehouse
              {
                  Id = 0,
                  Name = null,
                  Code = GenerateCode,
                  Location = string.Empty
              };
    }
}
