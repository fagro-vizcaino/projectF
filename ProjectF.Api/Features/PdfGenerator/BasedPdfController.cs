using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PlaywrightSharp;

namespace ProjectF.Api.Features.PdfGenerator
{
    public class BasePdfController : Controller
    {
        private static readonly Task playwrightInstall;
        static BasePdfController()
        {
            playwrightInstall = Playwright.InstallAsync();
        }

        protected Task<FileContentResult> Pdf() => Pdf(View());
        protected Task<FileContentResult> Pdf(string viewName) => Pdf(View(viewName));
        protected Task<FileContentResult> Pdf(object model) => Pdf(View(model));
        protected Task<FileContentResult> Pdf(string viewName, object model) => Pdf(View(viewName, model));

        protected async Task<FileContentResult> Pdf(ViewResult result)
        {
            await playwrightInstall;

            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            var page = await browser.NewPageAsync();

            var html = await GetHtml(result);

            await page.SetContentAsync(html);
            var pdf = await page.GetPdfAsync();
            return File(pdf, "application/pdf");
        }

        private async Task<string> GetHtml(ViewResult result)
        {
            // Get html from the result.
            // https://weblogs.asp.net/ricardoperes/getting-html-for-a-viewresult-in-asp-net-core

            var routeData = HttpContext.GetRouteData();
            var viewName = result.ViewName ?? routeData.Values["action"] as string;
            var actionContext = new ActionContext(HttpContext, routeData, new ControllerActionDescriptor());
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<MvcViewOptions>>();
            var htmlHelperOptions = options.Value.HtmlHelperOptions;

            var viewEngineResult = result.ViewEngine?.FindView(actionContext, viewName, true)
                                   ?? options.Value.ViewEngines.Select(x => x.FindView(actionContext, viewName, true))
                                       .First(x => x != null);

            var view = viewEngineResult.View;
            var builder = new StringBuilder();

            await using var output = new StringWriter(builder);
            var viewContext = new ViewContext(actionContext, view, result.ViewData, result.TempData, output, htmlHelperOptions);
            await view.RenderAsync(viewContext);

            return builder.ToString();
        }
    }
}
