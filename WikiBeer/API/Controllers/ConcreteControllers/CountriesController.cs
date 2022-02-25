using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ipme.WikiBeer.API.Controllers
{
    public class CountriesController : GenericController<CountryEntity,CountryDto>
    {
        public CountriesController(IGenericRepository<CountryEntity> dbRepository, IMapper mapper, ILogger<CountriesController> logger)
           : base(dbRepository,mapper, logger)
        {
        }
    }
}
