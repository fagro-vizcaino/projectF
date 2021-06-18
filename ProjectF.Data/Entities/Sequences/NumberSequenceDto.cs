using System;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.Entities.Sequences
{
    public record NumberSequenceDto(long Id,
            string Name,
            string Prefix,
            int InitialSequence,
            int NextSequence,
            string? DisplaySequence,
            int FinalSequence,
            DateTime ValidUntil,
            DateTime Created,
            DateTime? Modified,
            EntityStatus Status);
    //{
    //    public long Id { get; }
    //    public string Name { get; }
    //    public string Prefix { get; }
    //    public int InitialSequence { get; }
    //    public int NextSequence { get; init;}
    //    public int FinalSequence { get; }
    //    public DateTime ValidUntil { get; }
    //    public string DisplaySequence 
    //        => $"{Prefix}{NextSequence.ToString().PadLeft(8,'0')}";
    //    public DateTime Created { get; }
    //    public DateTime? Modified { get;}
    //    public EntityStatus Status { get; }

    //    public NumberSequenceDto(long id, 
    //        string name,
    //        string prefix,
    //        int initialSequence,
    //        int nextSequence,
    //        int finalSequence,
    //        DateTime validUntil,
    //        DateTime created,
    //        DateTime? modified,
    //        EntityStatus status)
    //    {
    //        Id              = id;
    //        Name            = name;
    //        Prefix          = prefix;
    //        InitialSequence = initialSequence;
    //        NextSequence    = nextSequence;
    //        FinalSequence   = finalSequence;
    //        ValidUntil      = validUntil;
    //        Created         = created;
    //        Modified        = modified;
    //        Status          = status;
    //    }

    //    public static implicit operator DocumentNumberSequence(NumberSequenceDto dto)
    //        => new DocumentNumberSequence(new Name(dto.Name),
    //            dto.Prefix, 
    //            dto.InitialSequence, 
    //            dto.NextSequence, 
    //            dto.FinalSequence, 
    //            dto.ValidUntil, 
    //            dto.Created,
    //            dto.Status);
    //}
}
