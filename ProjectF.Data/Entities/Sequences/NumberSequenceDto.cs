using System;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Sequences
{
    public record NumberSequenceDto
    {
        public long Id { get; }
        public string Name { get; }
        public string Prefix { get; }
        public int InitialSequence { get; }
        public int NextSequence { get; init;}
        public int FinalSequence { get; }
        public DateTime ValidUntil { get; }
        public bool? IsActive { get; }
        public string DisplaySequence 
            => $"{Prefix}{NextSequence.ToString().PadLeft(8,'0')}";
        public DateTime Created { get; }
        public DateTime? Modified { get;}

        public NumberSequenceDto(long id, 
            string name,
            string prefix,
            int initialSequence,
            int nextSequence,
            int finalSequence,
            DateTime validUntil,
            bool? isActive,
            DateTime created,
            DateTime? modified)
        {
            Id              = id;
            Name            = name;
            Prefix          = prefix;
            InitialSequence = initialSequence;
            NextSequence    = nextSequence;
            FinalSequence   = finalSequence;
            ValidUntil      = validUntil;
            IsActive        = isActive;
            Created         = created;
            Modified        = modified;
        }

        public static implicit operator DocumentNumberSequence(NumberSequenceDto dto)
            => new DocumentNumberSequence(new Name(dto.Name),
                dto.Prefix, 
                dto.InitialSequence, 
                dto.NextSequence, 
                dto.FinalSequence, 
                dto.ValidUntil, 
                dto.IsActive,
                dto.Created,
                dto.Modified);
    }
}
