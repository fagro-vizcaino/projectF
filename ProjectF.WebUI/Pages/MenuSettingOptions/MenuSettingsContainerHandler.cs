using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProjectF.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.MenuSettingOptions
{
    public class MenuSettingsContainerHandler : ComponentBase
    {
        public IReadOnlyList<MenuSetting> MenuSettingOptions { get; set; } = Enumerable.Empty<MenuSetting>().ToList();

        [Inject]
        NavigationManager NavigationManager { get; set;}

        protected override Task OnInitializedAsync()
        {
            MenuSettingOptions = new List<MenuSetting>()
            {
                new ()
                {
                    Route = "config/tax/",
                    Icon = @"<i class='fas fa-percentage fa-lg'></i>",
                    Name = "Impuestos",
                    Description = "Define los impuesto que manejas"
                },
                new ()
                {
                    Route = "/config/paymentterm/",
                    Icon = @"<i class='fas fa-comment-dollar fa-lg'></i>",
                    Name = "Termino de Pagos",
                    Description = "Define los impuesto que manejas"
                },
                new ()
                {
                    Route = "/config/bankAccountTypes",
                    Icon = @"<i class='fab fa-bandcamp fa-lg'></i>",
                    Name = "Tipo Cuenta Banco",
                    Description = "Define los impuesto que manejas"
                },
                new ()
                {
                    Route = "/config/documentsequence",
                    Icon = @"<i class='fas fa-stream'></i>",
                    Name = "Sequencia Númerica",
                    Description = "Defina la sequencia numerica para los documentos"
                },
                new ()
                {
                    Route = "/config/warehouses",
                    Icon = @"<i class='fas fa-warehouse'></i>",
                    Name = "Almacenes",
                    Description = "Defina sus almancenes"
                },
                new ()
                {
                    Route = "/config/categories",
                    Icon = @"<i class='fas fa-swatchbook'></i>",
                    Name = "Categorias",
                    Description = "Defina la categoria de sus productos"
                },
                new ()
                {
                    Route = "/config/company",
                    Icon = @"<i class='far fa-building'></i>",
                    Name = "Empresa",
                    Description = "Registre los datos de su empresa"
                },
                new ()
                {
                    Route = "/config/unitofmeasure",
                    Icon = @"<i class='far fa-building'></i>",
                    Name = "Medidas",
                    Description = "Define las medidas que utilizas en tus ventas"
                }
            };
            return base.OnInitializedAsync();
        }
        protected void GoToRoute(string route)
        {
            NavigationManager.NavigateTo(route);
        }
    }
}
