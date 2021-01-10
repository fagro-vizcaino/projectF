using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Sequences
{
    public static class NumberSequenceMapper
    {
        public static DocumentNumberSequence FromDto(NumberSequenceDto dto)
            => new DocumentNumberSequence(new Name(dto.Name),
                dto.Prefix,
                dto.InitialSequence,
                dto.NextSequence,
                dto.FinalSequence,
                dto.ValidUntil,
                dto.Created,
                dto.Status);

        public static NumberSequenceDto FromEntity(DocumentNumberSequence model)
          => new NumberSequenceDto(model.Id,
            model.Name.Value,
            model.Prefix,
            model.InitialSequence,
            model.NextSequence,
$"{model.Prefix}{model.NextSequence.ToString().PadLeft(8, '0')}",
            model.FinalSequence,
            model.ValidUntil,
            model.Created,
            model.Modified,
            model.Status);
    }
}
