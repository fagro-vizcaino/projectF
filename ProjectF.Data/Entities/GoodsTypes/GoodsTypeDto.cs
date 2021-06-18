using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Core;

namespace ProjectF.Data.Entities.GoodsTypes
{
    public record GoodsTypeDto(int Id, string Code, string Description, EntityStatus Status);
}
