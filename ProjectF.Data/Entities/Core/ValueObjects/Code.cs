using System.Collections.Generic;
using LanguageExt;
using LanguageExt.Common;
using ProjectF.Data.Entities.Core;
using static LanguageExt.Prelude;

namespace ProjectF.Data.Entities.Common.ValueObjects
{
  public class Code : ValueObject
  {
    public string Value { get; }

    protected Code() { }

    public Code(string entityCode) : this()
    {
      Value = entityCode;
    }

    public static Either<Error, Code> Of(string code) => IsValidCode(code);

    static Either<Error, Code> IsValidCode(string code)
        => code.Length < 20 && !string.IsNullOrEmpty(code) && !string.IsNullOrWhiteSpace(code)
        ? Right<Error, Code>(new Code(code))
        : Left<Error, Code>(Error.New("Code length is invalid, shouldn't be greater than 20"));

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static string CodeLengthError => "Code length is invalid, shouldn't be greater than 20";
  }
}
