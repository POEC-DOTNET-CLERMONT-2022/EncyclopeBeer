using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ipme.WikiBeer.API.Controllers
{
    public class ColorsController : GenericController<BeerColorEntity,BeerColorDto>
    {
        public ColorsController(IGenericRepository<BeerColorEntity> dbRepository, IMapper mapper)
            : base(dbRepository, mapper)
        {
        }
    }
}
