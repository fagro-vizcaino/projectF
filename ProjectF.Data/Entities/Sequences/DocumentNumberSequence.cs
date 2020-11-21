using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using System;

namespace ProjectF.Data.Entities.Sequences
{
    /// <summary>
    /// This document number sequence use the following format:
    /// Invoice Sequence = Name
    /// B0 = means Prefix
    /// 1021032040 = sequence
    /// 9999999999 = final sequence is the limit of the sequence
    /// </summary>
    public class DocumentNumberSequence : _BaseEntity
    {
        public Name Name { get; private set; }
        public string Prefix { get; private set; }
        public int InitialSequence { get; private set; }
        public int NextSequence { get; private set; }
        public int FinalSequence { get; private set; }
        public DateTime ValidUntil { get; private set; }
        public bool? IsActive { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Modified { get; private set;}

        protected DocumentNumberSequence() { }

        public DocumentNumberSequence(Name name,
            string prefix,
            int intialSequence,
            int nextSequence,
            int finalSequence,
            DateTime validUntil,
            bool? isActive,
            DateTime created,
            DateTime? modified = null)
        {
            Name            = name;
            Prefix          = prefix;
            InitialSequence = intialSequence;
            NextSequence    = nextSequence;
            FinalSequence   = finalSequence;
            ValidUntil      = validUntil;
            IsActive        = isActive;
            Created         = DateTime.MinValue == created ? DateTime.Now : created;
            Modified        = modified;
        }


        public void EditDocumentNumberSequence(Name name,
            string prefix,
            int initialSequence,
            int nextSequence,
            int finalSequence,
            DateTime validUntil,
            bool? isActive,
            DateTime? modified = null)
        {
            Name            = name;
            Prefix          = prefix;
            InitialSequence = initialSequence;
            NextSequence    = nextSequence;
            FinalSequence   = finalSequence;
            ValidUntil      = validUntil;
            IsActive        = isActive;
            Modified        = modified == null ? DateTime.Now : modified;
        }

        public static implicit operator NumberSequenceDto(DocumentNumberSequence model)
          => new NumberSequenceDto(model.Id,
              model.Name.Value,
              model.Prefix,
              model.InitialSequence,
              model.NextSequence,
              model.FinalSequence,
              model.ValidUntil,
              model.IsActive,
              model.Created,
              model.Modified);

    }
}
