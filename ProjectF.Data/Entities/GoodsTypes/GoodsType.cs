using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.GoodsTypes
{
    public class GoodsType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public EntityStatus Status { get; set; }

        protected GoodsType() { }

        public GoodsType(int id, string code, string description, EntityStatus status)
        {
            Id          = id;
            Code        = code;
            Description = description;
            Status      = status;
        }
    }
}
