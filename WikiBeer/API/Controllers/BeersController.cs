
using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
/// <summary>
/// TODO : Coller des ActionResult Partout
/// </summary>
namespace Ipme.WikiBeer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<BeerEntity> _ddbRepository;

        public BeersController(IGenericRepository<BeerEntity> ddbRepository, IMapper mapper)
        {
            _ddbRepository = ddbRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BeerDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {

            var allBeers = _mapper.Map<IEnumerable<BeerDto>>(_ddbRepository.GetAll());
            if (allBeers == null)
                return NotFound();
            return Ok(allBeers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BeerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid id)
        {
            var beer = _mapper.Map<BeerDto>(_ddbRepository.GetById(id));
            if (beer == null) return NotFound();
            return Ok(beer);
        }

        [HttpPost]
        public void Post([FromBody] BeerDto beerDto)
        {
            var beerEntity = _mapper.Map<BeerEntity>(beerDto);
            _ddbRepository.Create(beerEntity);
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] BeerDto beerDto)
        {
            var beerEntity = _mapper.Map<BeerEntity>(beerDto);
            _ = _ddbRepository.UpdateById(id, beerEntity);
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id) // on pourrait retourner un boléen ici
        {
            return _ddbRepository.DeleteById(id);
        }
    }
}
