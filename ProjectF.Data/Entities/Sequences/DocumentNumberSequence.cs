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

        protected DocumentNumberSequence() { }

        public DocumentNumberSequence(Name name,
            string prefix,
            int intialSequence,
            int nextSequence,
            int finalSequence,
            DateTime validUntil,
            DateTime created,
            EntityStatus status = EntityStatus.Active)
        {
            Name            = name;
            Prefix          = prefix;
            InitialSequence = intialSequence;
            NextSequence    = nextSequence;
            FinalSequence   = finalSequence;
            ValidUntil      = validUntil;
            Created         = DateTime.MinValue == created ? DateTime.Now : created;            
            Status          = status;
        }


        public void EditDocumentNumberSequence(Name name,
            string prefix,
            int initialSequence,
            int nextSequence,
            int finalSequence,
            DateTime validUntil,
            EntityStatus status)
        {
            Name            = name;
            Prefix          = prefix;
            InitialSequence = initialSequence;
            NextSequence    = nextSequence;
            FinalSequence   = finalSequence;
            ValidUntil      = validUntil;
            Modified        = DateTime.Now;
            Status          = status;
        }

       

    }
}
