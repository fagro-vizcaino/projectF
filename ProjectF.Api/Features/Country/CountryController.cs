using System.Threading.Tasks;
using ProjectF.Application.Countries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ProjectF.Api.Features.Countries
{

    [Route("api/common/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryCrudOperation _countryOperation;

        public CountryController(CountryCrudOperation countryOperation)
        {
            _countryOperation = countryOperation;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var items = await _countryOperation.GetAll();
            return Ok(items);
        }
    }
}