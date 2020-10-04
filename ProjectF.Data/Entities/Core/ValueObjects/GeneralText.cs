using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace ProjectF.Data.Entities.Common.ValueObjects
{
    public class GeneralText
    {
        public string Value { get; }

        protected GeneralText() { }

        public GeneralText(string value) : this()
        {
            Value = value;
        }

        public static Either<Error, GeneralText> Of(string description) => IsValidGeneralText(description);

        static Either<Error, GeneralText> IsValidGeneralText(string description)
            => description.Length >= 120 || string.IsNullOrEmpty(description)
            ? Left<Error, GeneralText>(Error.New(NameLengthError))

            : Right<Error, GeneralText>(new GeneralText(description));

        public static string NameLengthError => "Description length is invalid, shouldn't be greater than 220";
    }
}
