using System;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Sequences
{
    public class NumberSequenceDto
    {
        public long Id { get; }
        public string Name { get; }
        public string Prefix { get; }
        public int InitialSequence { get; }
        public int NextSequence { get; }
        public int FinalSequence { get; }
        public DateTime ValidUntil { get; }
        public bool IsActive { get; }

        public NumberSequenceDto(long id, 
            string name,
            string prefix,
            int initialSequence,
            int nextSequence,
            int finalSequence,
            DateTime validUntil,
            bool isActive)
        {
            Id              = id;
            Name            = name;
            Prefix          = prefix;
            InitialSequence = initialSequence;
            NextSequence    = nextSequence;
            FinalSequence   = finalSequence;
            ValidUntil      = validUntil;
            IsActive        = isActive;
        }

        public NumberSequenceDto With(long? id = null,
            string? name = null,
            string? prefix = null,
            int? initialSequence = null,
            int? nextSequence = null,
            int? finalSequence = null,
            DateTime? validUntil = null,
            bool? isActive = null
            ) => new NumberSequenceDto(id ?? this.Id,
                name ?? this.Name,
                prefix ?? this.Prefix,
                initialSequence ?? this.InitialSequence,
                nextSequence ?? this.NextSequence,
                finalSequence ?? this.FinalSequence,
                validUntil ?? this.ValidUntil,
                isActive ?? this.IsActive);

        public static implicit operator DocumentNumberSequence(NumberSequenceDto dto)
            => new DocumentNumberSequence(new Name(dto.Name),
                dto.Prefix, 
                dto.InitialSequence, 
                dto.NextSequence, 
                dto.FinalSequence, 
                dto.ValidUntil, 
                dto.IsActive);
    }
}
