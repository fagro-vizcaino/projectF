using System;
using System.Collections.Generic;
using LanguageExt;
using LanguageExt.Common;
using ProjectF.Data.Entities.Core;
using static LanguageExt.Prelude;

namespace ProjectF.Data.Entities.Common.ValueObjects
{
    public class GeneralText: ValueObject
    {
        public string Value { get; }

        protected GeneralText() { }

        public GeneralText(string? value) : this()
        {
            Value = value ?? string.Empty;
        }

        public static Result<GeneralText> Of(string description) => IsValidGeneralText(description);

        static Result<GeneralText> IsValidGeneralText(string description)
            => description.Length >= 120 || string.IsNullOrEmpty(description)
            ? new Result<GeneralText>(new GeneralText(NameLengthError))
            : new Result<GeneralText>(new GeneralText(description));

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static string NameLengthError => "Description length is invalid, shouldn't be greater than 220";

        public static implicit operator string(GeneralText text)
        {
            return text.Value;
        }
    }
}
