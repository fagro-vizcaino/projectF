using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

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
        public DocumentType DocumentType { get; private set; }
        public Name Name { get; private set; }
        public bool Automatic { get; private set; }
        public Code Prefix { get; private set; }
        public NumberSequence Sequence { get; private set; }
        public long FinalSequence { get; private set; }
        public DateTime ValidFrom { get; private set; }
        public DateTime ValidTo { get; private set; }
        public bool IsDefault { get; private set; }

        protected DocumentNumberSequence() { }

        public DocumentNumberSequence(DocumentType documentType,
            Name name,
            bool automatic,
            Code prefix,
            NumberSequence sequence,
            long finalsequence,
            DateTime validFrom,
            DateTime validTo,
            bool isDefault
            )
        {
            DocumentType   = documentType;
            Name           = name;
            Automatic      = automatic;
            Prefix         = prefix;
            Sequence       = sequence;
            FinalSequence  = finalsequence;
            ValidFrom      = validFrom;
            ValidTo        = validTo;
            IsDefault      = isDefault;

            if(Sequence > FinalSequence)
            {
                throw new ArgumentException("sequence cannot be greater than final sequence");
            }

            if(ValidFrom > ValidTo)
            {
                throw new ArgumentException("ValidFrom cannot be greater than ValidTo");
            }
        }


        public void EditDocumentNumberSequence(DocumentType documentType,
            Name name,
            bool automatic,
            Code prefix,
            NumberSequence sequence,
            long finalsequence,
            DateTime validFrom,
            DateTime validTo,
            bool isDefault)
        {
            DocumentType   = documentType;
            Name           = name;
            Automatic      = automatic;
            Prefix         = prefix;
            Sequence       = sequence;
            FinalSequence  = finalsequence;
            ValidFrom      = validFrom;
            ValidTo        = validTo;
            IsDefault      = isDefault;

            if(Sequence > FinalSequence)
            {
                throw new ArgumentException("sequence cannot be greater than final sequence");
            }

            if(ValidFrom > ValidTo)
            {
                throw new ArgumentException("ValidFrom cannot be greater than ValidTo");
            }
        }

    }
}
