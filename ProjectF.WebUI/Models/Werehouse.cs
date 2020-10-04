using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Models
{
    public class Werehouse : FEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
