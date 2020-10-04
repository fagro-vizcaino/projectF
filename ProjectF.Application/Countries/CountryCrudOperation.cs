using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Countries;
using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;

namespace ProjectF.Application.Countries
{
    public class CountryCrudOperation
    {
        readonly CountryRepository _countryRepository;

        public CountryCrudOperation(CountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<List<CountryDto>> GetAll()
         => (await _countryRepository.GetAllAsync())
            .Map(ct => new CountryDto(ct.Id, ct.Name, ct.IconImage))
            .ToList();

        public Country FromCountryId(int id)
          => _countryRepository.FromCountryId(id);

    }
}