﻿using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// TODO : affiner les block catch (renvoyer autre chose que du 500)
/// liste des codes d'erreurs possibles : https://developer.mozilla.org/en-US/docs/Web/HTTP/Status#client_error_responses
/// Un truc sympa : sur des controller génériques : 
/// https://www.strathweb.com/2018/04/generic-and-dynamically-generated-controllers-in-asp-net-core-mvc/
/// </summary>
/// 
//[assembly: InternalsVisibleTo(typeof(BeerEntity).Assembly.GetName().Name)]
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
        [EnableCors("LocalPolicy")]
        public IActionResult Get()
        {
            try
            {
                var allBeers = _mapper.Map<IEnumerable<BeerDto>>(_ddbRepository.GetAll());
                return Ok(allBeers);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BeerDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [EnableCors("LocalPolicy")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var beer = _ddbRepository.GetById(id);
                if (beer == null)
                    return NotFound();
                return Ok(_mapper.Map<BeerDto>(beer));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] BeerDto beerDto)
        {
            try
            {
                var beerEntity = _mapper.Map<BeerEntity>(beerDto);
                var beerEntityCreated = _ddbRepository.Create(beerEntity);
                var correspondingBeerDto = _mapper.Map<BeerDto>(beerEntityCreated);
                return CreatedAtAction(nameof(Get), new { id = correspondingBeerDto.Id }, correspondingBeerDto);
            }
            catch (UndesiredBorderEffectException ubee)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Put(Guid id, [FromBody] BeerDto beerDto) // Guid à passer en FromQuerry???
        {
            try
            {
                var beerEntity = _mapper.Map<BeerEntity>(beerDto); // automapper plante si la forme du Dto n'est pas bonne -> BadRequest?
                var updatedBeerEntity = _ddbRepository.Update(beerEntity);
                if (updatedBeerEntity == null)
                    return NotFound();
                return Ok();
            }
            catch (UndesiredBorderEffectException ubee)
            {
                return BadRequest();
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
                    return Ok(true);
                // Ni null, ni vrai, alors faux, id correct mais pas de suppression en base
                //return StatusCode(500);
                return Ok(false); // serait peut être mieux...
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}