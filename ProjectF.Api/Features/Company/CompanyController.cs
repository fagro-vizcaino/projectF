using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Application.Companies;
using ProjectF.Data.Entities.Companies;
using static ProjectF.Data.Entities.Companies.CompanyMapper;

namespace ProjectF.Api.Features.Company
{
    [Route("api/config/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        readonly CompanyCrudHandler _companyCrudHandler;

        public CompanyController(CompanyCrudHandler companyCrudHandler)
        {
            _companyCrudHandler = companyCrudHandler;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CompanyDto dto)
            => (await _companyCrudHandler.Create(dto)).Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: pt => CreatedAtRoute(nameof(GetCompanyById),
                        new { id = dto.Id }, dto));

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, CompanyDto model)
            => (await _companyCrudHandler.Update(id, model))
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: pt => CreatedAtRoute(nameof(GetCompanyById),
                        new { id = pt.Id }, model));


        [HttpGet("{id}", Name = "GetCompanyById")]
        public IActionResult GetCompanyById(long id)
           => _companyCrudHandler.Find(id).Match<ActionResult>(
                   Left: err => NotFound(err.Message),
                   Right: c => Ok(FromEntity(c)));



        [HttpGet]
        public ActionResult GetCompany()
        {
            var result = _companyCrudHandler.GetAll()
                .Select(c => c);
            if (result.Any()) return Ok(result);

            return NotFound();
        }

    }
}
