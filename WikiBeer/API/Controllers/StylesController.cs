using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Ipme.WikiBeer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StylesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<BeerStyleEntity> _ddbRepository;

        public StylesController(IGenericRepository<BeerStyleEntity> ddbRepository, IMapper mapper)
        {
            _ddbRepository = ddbRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BeerStyleDto>), 200)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            try
            {
                var allStyles = _mapper.Map<IEnumerable<BeerStyleDto>>(_ddbRepository.GetAll());
                return Ok(allStyles);
            }
            catch (Exception e)
            {
                // toutes les exceptions non géré passe en 500
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BeerStyleDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(Guid id)
        {
            try
            {
                var country = _ddbRepository.GetById(id);
                if (country == null)
                    return NotFound();
                return Ok(_mapper.Map<BeerStyleDto>(country));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }

        /// <summary>
        /// CreatedAtAction doit retourner ici l'équivalent d'une méthode Get (cad un Dto!)!
        /// </summary>
        /// <param name="beerStyleDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] BeerStyleDto beerStyleDto)
        {
            try
            {
                var styleEntity = _mapper.Map<BeerStyleEntity>(beerStyleDto);
                var styleEntityCreated = _ddbRepository.Create(styleEntity);
                var correspondingStyleDto = _mapper.Map<BeerStyleDto>(styleEntityCreated);
                return CreatedAtAction(nameof(Get), new { id = correspondingStyleDto.Id }, correspondingStyleDto);
            }
            catch (Exception e)
            {
                // On peut gérer les problèmes de mapping ici
                return StatusCode(500);
            }

        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Put(Guid id, [FromBody] BeerStyleDto beerStyleDto)
        {
            try
            {
                var styleEntity = _mapper.Map<BeerStyleEntity>(beerStyleDto);
                var updatedStyleEntity = _ddbRepository.UpdateById(id, styleEntity);
                if (updatedStyleEntity == null)
                    return NotFound();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(Guid id) // on pourrait retourner un boléen ici
        {
            try
            {
                var response = _ddbRepository.DeleteById(id);
                if (response == null)  // id non trouvé en base
                    return NotFound();
                // bool == true car ce bool en particulier peut etre null! (on ne peut pas faire if(bool?) directement!)
                if (response == true) // si vrai le delete à fonctionné
                    return Ok();
                // Ni null, ni vrai, alors faux, id correct mais pas de suppression en base
                return StatusCode(500);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
