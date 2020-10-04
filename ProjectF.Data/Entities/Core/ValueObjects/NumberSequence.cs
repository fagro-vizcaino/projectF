using LanguageExt;
using LanguageExt.Common;
using System.Linq;
using static LanguageExt.Prelude;

namespace ProjectF.Data.Entities.Common.ValueObjects
{
    public class NumberSequence
    {
        public long Value { get; }

        protected NumberSequence() { }

        public NumberSequence(long sequence) : this()
        {
            Value = sequence;
        }

        public static Either<Error, NumberSequence> Of(long sequence) => IsValidSequence(sequence);

        static Either<Error, NumberSequence> IsValidSequence(long sequence)
            => sequence <= 99999999
            ? Right<Error, NumberSequence>(new NumberSequence(sequence))
            : Left<Error, NumberSequence>(Error.New("Sequence value not allowed"));

        public static implicit operator long(NumberSequence sequence)
        {
            return sequence.Value;
        }

    }
}
