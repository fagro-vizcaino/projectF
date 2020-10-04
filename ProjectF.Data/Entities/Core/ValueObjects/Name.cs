using ProjectF.Data.Entities.Core;
using LanguageExt;
using LanguageExt.Common;
using System.Collections.Generic;
using static LanguageExt.Prelude;

namespace ProjectF.Data.Entities.Common.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; }

        protected Name() { }

        public Name(string value) : this()
        {
            Value = value;
        }

        public static Validation<Error, Name> Of(string name)
            => name.Length <= 60 || !string.IsNullOrEmpty(name)
                    ? Success<Error, Name>(new Name(name))
                    : Fail<Error, Name>(Error.New("Name is not valid"));


        //public static implicit operator Name(string name) => name;
       
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
