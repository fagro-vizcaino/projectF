using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectF.Api.Features.Test
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var items = Enumerable.Range(1, 50);
            return Ok(items);
        }
    }
}