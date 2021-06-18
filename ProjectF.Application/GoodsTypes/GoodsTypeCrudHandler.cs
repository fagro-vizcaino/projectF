using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.GoodsTypes;
using static ProjectF.Data.Entities.GoodsTypes.GoodsTypeMapper;
using ProjectF.Data.Repositories;

namespace ProjectF.Application.GoodsTypes
{
    public class GoodsTypeCrudHandler
    {
        readonly GoodsTypeRepository _goodsTypeRepository;

        public GoodsTypeCrudHandler(GoodsTypeRepository goodsTypeRepository)
        {
            _goodsTypeRepository = goodsTypeRepository;
        }

        public async Task<IEnumerable<GoodsTypeDto>> GetAll()
            => (await _goodsTypeRepository.GetAllAsync())
            .Map(c => FromEntity(c));

        public async Task<GoodsTypeDto> Find(int id)
            => FromEntity(await _goodsTypeRepository.FindAsync(id));
    }
}
