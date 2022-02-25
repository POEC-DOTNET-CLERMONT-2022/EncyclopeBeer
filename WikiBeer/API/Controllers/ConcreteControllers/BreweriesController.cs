#nullable disable
using Microsoft.AspNetCore.Mvc;
using Ipme.WikiBeer.Entities;
using AutoMapper;
using Ipme.WikiBeer.Persistance.Repositories;
using Ipme.WikiBeer.Dtos;

namespace Ipme.WikiBeer.API.Controllers
{
    public class BreweriesController : GenericController<BreweryEntity,BreweryDto>
    {
        public BreweriesController(IGenericRepository<BreweryEntity> dbRepository, IMapper mapper, ILogger<BreweriesController> logger)
            : base(dbRepository,mapper, logger)
        {
        }
    }
}
