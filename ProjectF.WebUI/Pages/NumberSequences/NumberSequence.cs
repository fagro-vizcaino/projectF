using ProjectF.WebUI.Models;
using System;

namespace ProjectF.WebUI.Pages.NumberSequences
{
    public class NumberSequence: FEntity
    {
        public string Name { get; set; }
        public string Prefix { get; set; }
        public int InitialSequence { get; set; }
        public int NextSequence { get; set; }
        public int FinalSequence { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool IsActive { get; set; }
        public string DisplaySequence {get; set; }
    }
}
