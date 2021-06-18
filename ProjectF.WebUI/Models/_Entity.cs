using System;

namespace ProjectF.WebUI.Models
{
    public class FEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
    }
}
