using LanguageExt;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Text;
using LanguageExt.Common;

namespace ProjectF.Application
{
    public static partial class Validators
    {
        public static Func<string, Validation<Error, string>> NotLongerThan(int maxLength) =>
            str => Optional(str)
                .Where(s => s.Length <= maxLength)
                .ToValidation(Error.New($"{str} must not be longer than {maxLength}"));

        public static Validation<Error, string> NotEmpty(string str) =>
            Optional(str)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToValidation(Error.New("Empty string"));
    }
}
