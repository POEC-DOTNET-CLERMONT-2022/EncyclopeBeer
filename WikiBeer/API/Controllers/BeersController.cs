
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
        [ProducesResponseType(typeof(IEnumerable<BeerDto>), 200)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            try
            {
                var allBeers = _mapper.Map<IEnumerable<BeerDto>>(_ddbRepository.GetAll());
                return Ok(allBeers);
            }
            catch (Exception e)
            {
                // toutes les exceptions non géré passe en 500
                return StatusCode(500); 
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BeerDto), 200)]
        [ProducesResponseType(404)] 
        public IActionResult Get(Guid id)
        {
            var beer = _ddbRepository.GetById(id);
            if (beer == null)
                return NotFound();
            return Ok(_mapper.Map<BeerDto>(beer));
        }

        [HttpPost]
        [ProducesResponseType(201)] 
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] BeerDto beerDto)
        {
            try
            {
                var beerEntity = _mapper.Map<BeerEntity>(beerDto);
                var beerEntityCreated = _ddbRepository.Create(beerEntity);
                return CreatedAtAction(nameof(Get), new { id = beerEntityCreated.Id }, beerEntityCreated); ;
            }
            catch(Exception e)
            {
                // On peut gérer les problèmes de mapping ici
                return StatusCode(500);
            }

        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Put(Guid id, [FromBody] BeerDto beerDto)
        {
            try
            {
                var beerEntity = _mapper.Map<BeerEntity>(beerDto);
                var updatedBeerEntity = _ddbRepository.UpdateById(id, beerEntity);
                if (updatedBeerEntity == null)
                    return NotFound();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id) // on pourrait retourner un boléen ici
        {
            return _ddbRepository.DeleteById(id);
        }
    }
}
