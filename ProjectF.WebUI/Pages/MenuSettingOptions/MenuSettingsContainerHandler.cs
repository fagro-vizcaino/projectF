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
        public IReadOnlyList<MenuSetting> MenuSettingOptions { get; private set; }

        [Inject]
        NavigationManager NavigationManager { get; set;}

        public MenuSettingsContainerHandler()
        {
            MenuSettingOptions = new List<MenuSetting>()
            {
                new MenuSetting 
                {
                    Route = "config/tax/",
                    Icon = @"<i class='fas fa-percentage fa-lg'></i>",
                    Name ="Impuestos",
                    Description = "Define los impuesto que manejas"
                },
                new MenuSetting 
                { 
                    Route = "/config/paymentterm/",
                    Icon = @"<i class='fas fa-comment-dollar fa-lg'></i>",
                    Name ="Termino de Pagos", 
                    Description = "Define los impuesto que manejas"
                },
                new MenuSetting 
                { 
                    Route = "/config/bankAccountTypes",
                    Icon = @"<i class='fab fa-bandcamp fa-lg'></i>",
                    Name ="Tipo Cuenta Banco", 
                    Description = "Define los impuesto que manejas"
                }
            };
        }

        protected void GoToRoute(string route)
        {
            NavigationManager.NavigateTo(route);
        }
    }
}
