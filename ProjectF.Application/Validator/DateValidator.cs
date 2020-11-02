using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ProjectF.Application.Validator
{
    public static partial class Validators
    {
        public static Func<DateTime, Validation<Error, DateTime>> AtLeast(DateTime datefrom) =>
          value => Optional(value)
              .Where(d => d >= datefrom)
              .ToValidation(Error.New($"El Valor debe ser mayor o igual a {datefrom.ToString("MMM dd, yyyy", new CultureInfo("es-Do"))}"));
    }
}
