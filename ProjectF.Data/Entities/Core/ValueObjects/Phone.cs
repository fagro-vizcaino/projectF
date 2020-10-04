using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace ProjectF.Data.Entities.Common.ValueObjects
{
    public class Phone
    {
        public string Value { get; }

        protected Phone() { }

        public Phone(string value) : this()
        {
            Value = value;
        }

        public static Either<Error, Phone> Of(string phone) => IsValidName(phone);

        static Either<Error, Phone> IsValidName(string phone)
            => Regex.Match(phone, @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$").Success
                ? Right<Error, Phone>(new Phone(phone))
                : Left<Error, Phone>(Error.New(PhoneLengthError));
        public static string PhoneLengthError => "PhoneNumber is not valid";
    }
}
