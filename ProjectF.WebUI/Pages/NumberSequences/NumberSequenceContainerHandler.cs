using ProjectF.WebUI.Components.Common;
using System;

namespace ProjectF.WebUI.Pages.NumberSequences
{
    public class NumberSequenceContainerHandler : BaseContainerBasicCrud<NumberSequence>
    {
        public NumberSequenceContainerHandler() : base("Sequencias Numericas")
        {  
            InitModel(EmptyObject());
            NewOrEditOperation = GetNewModelOrEdit;
        }

        public NumberSequence GetNewModelOrEdit(NumberSequence sequence = null)
          => sequence?.Id > 0 
            ? new NumberSequence()
            {
                Id              = sequence.Id,
                DisplaySequence = sequence.DisplaySequence,
                Name            = sequence.Name,
                InitialSequence = sequence.InitialSequence,
                NextSequence    = sequence.NextSequence,
                FinalSequence   = sequence.FinalSequence,
                IsActive        = sequence.IsActive,
                Prefix          = sequence.Prefix,
                ValidUntil      = sequence.ValidUntil

            }
            : EmptyObject();

        NumberSequence EmptyObject() => new NumberSequence
            {
                Id              = 0,
                Name            = string.Empty,
                DisplaySequence = string.Empty,
                InitialSequence = 0,
                NextSequence    = 0,
                FinalSequence   = 0,
                IsActive        = true,
                Prefix          = string.Empty,
                ValidUntil      = DateTime.Now
            };

    }
}
