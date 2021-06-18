using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Application.GoodsTypes;

namespace ProjectF.Api.Features.GoodsTypes
{
    [Route("api/common/[controller]")]
    [ApiController]
    public class GoodsTypeController : ControllerBase
    {
        readonly GoodsTypeCrudHandler _goodsTypeCrudHandler;

        public GoodsTypeController(GoodsTypeCrudHandler goodsTypeCrudHandler)
            => _goodsTypeCrudHandler = goodsTypeCrudHandler;


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = (await _goodsTypeCrudHandler
                .GetAll()).ToList();

            return Ok(items);

        }
    }
}
