using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ipme.WikiBeer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<BeerColorEntity> _ddbRepository;

        public ColorsController(IGenericRepository<BeerColorEntity> ddbRepository, IMapper mapper)
        {
            _ddbRepository = ddbRepository;
            _mapper = mapper;
            //var tt = typeof(BeerEntity).Assembly.GetName().Name;
            //var tt = new BeerEntity();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BeerColorDto>), 200)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            try
            {
                var allBeers = _mapper.Map<IEnumerable<BeerColorDto>>(_ddbRepository.GetAll());
                return Ok(allBeers);
            }
            catch (Exception e)
            {
                // toutes les exceptions non géré passe en 500
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BeerColorDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(Guid id)
        {
            try
            {
                var beer = _ddbRepository.GetById(id);
                if (beer == null)
                    return NotFound();
                return Ok(_mapper.Map<BeerColorDto>(beer));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }

        /// <summary>
        /// CreatedAtAction doit retourner ici l'équivalent d'une méthode Get (cad un Dto!)!
        /// </summary>
        /// <param name="colorDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] BeerColorDto colorDto)
        {
            try
            {
                var colorEntity = _mapper.Map<BeerColorEntity>(colorDto);
                var colorEntityCreated = _ddbRepository.Create(colorEntity);
                var correspondingColorDto = _mapper.Map<BeerColorDto>(colorEntityCreated);
                return CreatedAtAction(nameof(Get), new { id = correspondingColorDto.Id }, correspondingColorDto);
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
        public IActionResult Put(Guid id, [FromBody] BeerColorDto colorDto)
        {
            try
            {
                var colorEntity = _mapper.Map<BeerColorEntity>(colorDto);
                var updatedColorEntity = _ddbRepository.UpdateById(id, colorEntity);
                if (updatedColorEntity == null)
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
