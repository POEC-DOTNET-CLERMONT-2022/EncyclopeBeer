#nullable disable
using Microsoft.AspNetCore.Mvc;
using Ipme.WikiBeer.Entities;
using AutoMapper;
using Ipme.WikiBeer.Persistance.Repositories;
using Ipme.WikiBeer.Dtos;

namespace Ipme.WikiBeer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<BreweryEntity> _ddbRepository;

        public BreweriesController(IGenericRepository<BreweryEntity> ddbRepository, IMapper mapper)
        {
            _ddbRepository = ddbRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BreweryDto>), 200)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            try
            {
                var allBrewery = _mapper.Map<IEnumerable<BreweryDto>>(_ddbRepository.GetAll());
                return Ok(allBrewery);
            }
            catch (Exception e)
            {
                // toutes les exceptions non géré passe en 500
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BreweryDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(Guid id)
        {
            try
            {
                var brewery = _ddbRepository.GetById(id);
                if (brewery == null)
                    return NotFound();
                return Ok(_mapper.Map<BreweryDto>(brewery));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }

        /// <summary>
        /// CreatedAtAction doit retourner ici l'équivalent d'une méthode Get (cad un Dto!)!
        /// </summary>
        /// <param name="breweryDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] BreweryDto breweryDto)
        {
            try
            {
                var breweryEntity = _mapper.Map<BreweryEntity>(breweryDto);
                var breweryEntityCreated = _ddbRepository.Create(breweryEntity);
                var correspondingBreweryDto = _mapper.Map<BreweryDto>(breweryEntityCreated);
                return CreatedAtAction(nameof(Get), new { id = correspondingBreweryDto.Id }, correspondingBreweryDto);
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
        public IActionResult Put(Guid id, [FromBody] BreweryDto breweryDto)
        {
            try
            {
                var breweryEntity = _mapper.Map<BreweryEntity>(breweryDto);
                var updatedBreweryEntity = _ddbRepository.UpdateById(id, breweryEntity);
                if (updatedBreweryEntity == null)
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
