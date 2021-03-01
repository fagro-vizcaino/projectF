using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaywrightSharp;
using ProjectF.Application.Common;
using ProjectF.Application.Products;
using ProjectF.Data.Entities.Products;

namespace ProjectF.Api.Infrastructure.Extensions
{
    public static class OptionToActionResult
    {
        private static IActionResult ToActionResult<T>(this Option<T> option) =>
            option.Match<IActionResult>(
                Some: t => new OkObjectResult(t),
                None: () => new NotFoundResult());


        private static IActionResult ToActionResult<T, TInterface>(this Option<T> option, HttpResponse resp) 
            where T : IMainList<TInterface>
            => option.Match<IActionResult>(
                Some: t => {
                        resp?.Headers.Add("X-Pagination", JsonSerializer.Serialize(t.Meta));
                        return new OkObjectResult(t.List); 
                    },
                None: () => new NotFoundResult());

        public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option) =>
            option.Map(ToActionResult);
        
        public static Task<IActionResult> ToActionResult<Tk, TInterface>(this Task<Option<Tk>> option, HttpResponse resp)
        where Tk: IMainList<TInterface>
            => option.Map(x => ToActionResult<Tk, TInterface>(x, resp));
        
    }
}
