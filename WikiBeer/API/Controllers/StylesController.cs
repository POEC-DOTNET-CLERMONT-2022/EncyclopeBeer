using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ipme.WikiBeer.API.Controllers
{
    public class StylesController : GenericController<BeerStyleEntity,BeerStyleDto>
    {
        public StylesController(IGenericRepository<BeerStyleEntity> dbRepository, IMapper mapper)
            : base(dbRepository,mapper)
        {
        }
    }
}
