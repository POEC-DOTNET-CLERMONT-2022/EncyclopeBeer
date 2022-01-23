
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
        public IEnumerable<BeerDto> Get()
        {
            var allBeers = _mapper.Map<IEnumerable<BeerDto>>(_ddbRepository.GetAll());
            return allBeers;
        }

        [HttpGet("{id}")]
        public BeerDto Get(Guid id)
        {
            var beer = _ddbRepository.GetById(id);
            return _mapper.Map<BeerDto>(beer);
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
