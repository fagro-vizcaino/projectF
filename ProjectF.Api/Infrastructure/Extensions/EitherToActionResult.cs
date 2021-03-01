﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using LanguageExt;

namespace ProjectF.Api.Infrastructure.Extensions
{
    public static class EitherToActionResult
    {
        public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either) =>
            either.Map(Match);

        public static Task<IActionResult> ToActionResult(this Task<Either<Error, Task>> either) =>
            either.Bind(Match);

        private static IActionResult Match<L, R>(this Either<L, R> either) =>
            either.Match<IActionResult>(
                Left: l => new BadRequestObjectResult(l),
                Right: r => new OkObjectResult(r));

        private async static Task<IActionResult> Match(Either<Error, Task> either) =>
            await either.MatchAsync<IActionResult>(
                RightAsync: async t => { await t; return new OkResult(); },
                Left: e => new BadRequestObjectResult(e));
    }
}
