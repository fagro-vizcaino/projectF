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
    public class DocumentNumberSequence : Entity
    {
        public Name Name { get; private set; }
        public string Prefix { get; private set; }
        public int InitialSequence { get; private set; }
        public int NextSequence { get; private set; }
        public int FinalSequence { get; private set; }
        public DateTime ValidUntil { get; private set; }
        public bool IsActive { get; private set; }

        protected DocumentNumberSequence() { }

        public DocumentNumberSequence(Name name,
            string prefix,
            int intialSequence,
            int nextSequence,
            int finalSequence,
            DateTime validUntil,
            bool isActive)
        {
            Name            = name;
            Prefix          = prefix;
            InitialSequence = intialSequence;
            NextSequence    = nextSequence;
            FinalSequence   = finalSequence;
            ValidUntil      = validUntil;
            IsActive        = isActive;
        }


        public void EditDocumentNumberSequence(Name name,
            string prefix,
            int initialSequence,
            int nextSequence,
            int finalSequence,
            DateTime validUntil,
            bool isActive)
        {
            Name            = name;
            Prefix          = prefix;
            InitialSequence = initialSequence;
            NextSequence    = nextSequence;
            FinalSequence   = finalSequence;
            ValidUntil      = validUntil;
            IsActive        = isActive;
        }

        public static implicit operator NumberSequenceDto(DocumentNumberSequence entity)
          => new NumberSequenceDto(entity.Id,
              entity.Name.Value,
              entity.Prefix,
              entity.InitialSequence,
              entity.NextSequence,
              entity.FinalSequence,
              entity.ValidUntil,
              entity.IsActive);

    }
}
