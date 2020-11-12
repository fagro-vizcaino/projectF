using ProjectF.WebUI.Components.Common;
using System;

namespace ProjectF.WebUI.Pages.NumberSequence
{
    public class NumberSequenceContainerHandler : BaseContainerBasicCrud<NumberSequence>
    {
        public NumberSequenceContainerHandler() : base("Sequencias Numericas")
        {  
            InitModel(EmptyObject());
            NewOrEditOperation = GetNewModelOrEdit;
        }

        public NumberSequence GetNewModelOrEdit(NumberSequence numberSequence = null)
          => numberSequence ?? EmptyObject();

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
