﻿using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

/// <summary>
/// TODO : affiner les block catch (renvoyer autre chose que du 500)
/// liste des codes d'erreurs possibles : https://developer.mozilla.org/en-US/docs/Web/HTTP/Status#client_error_responses
/// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc?view=aspnetcore-6.0
/// classes status Code https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes?view=aspnetcore-6.0
/// Un truc sympa : sur des controller génériques : 
/// https://www.strathweb.com/2018/04/generic-and-dynamically-generated-controllers-in-asp-net-core-mvc/
/// De la doc sur les opration Filter (Les ProduceResponseType) : on perd qq infos Swagger en passant par le générique
/// https://stackoverflow.com/questions/46817207/how-to-return-generic-types-on-producesresponsetype-swagger
/// En passant des ActionResult<T> comme type de retour on retrouve un peu plus de détails dans la doc!
/// TT se passera bien tant que les objets potentiellement retournés sont casté correctement dans les ActionResult -> StatusCodeResult/ObjectResult
/// Un peu plus sur les Formatter (pour se passer de NewtonSoft)
/// https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/custom-formatters?view=aspnetcore-6.0
/// Note : un service dédié au mapping (et validation) permettrai de vraiment découpler cette classe de l'assembly Entities!
/// Sur un bug super étrange lié au nommage des controller et du CreatedAtAction
/// https://stackoverflow.com/questions/59288259/asp-net-core-3-0-createdataction-returns-no-route-matches-the-supplied-values
/// </summary>
namespace Ipme.WikiBeer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<TEntity, TDto> : ControllerBase, IGenerciController<TDto>
        where TEntity : class, IEntity
        where TDto : class, IDto
    {
        private readonly IMapper _mapper;//TODO protected 
        private readonly IGenericRepository<TEntity> _dbRepository;//TODO protected 

        public GenericController(IGenericRepository<TEntity> dbRepository, IMapper mapper)
        {
            _dbRepository = dbRepository;
            _mapper = mapper;           
        }

        [HttpGet]
        [ProducesResponseType(200)] // si on avait une classe BaseDto on pourrait la mettre là
        [ProducesResponseType(500)]
        [EnableCors("LocalPolicy")]
        public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAsync()
        {
            try
            {
                var allDtos = _mapper.Map<IEnumerable<TDto>>(await _dbRepository.GetAllAsync());
                return Ok(allDtos);
            }
            catch (Exception e)
            {
                //TODO logger
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [EnableCors("LocalPolicy")]
        public virtual async Task<ActionResult<TDto>> GetAsync(Guid id)
        {
            try
            {
                //TODO logger
                var entity = await _dbRepository.GetByIdAsync(id);
                if (entity == null)
                    return NotFound();
                return Ok(_mapper.Map<TDto>(entity));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// voir : https://docs.microsoft.com/en-us/dotnet/api/system.web.http.invalidmodelstateresult?view=aspnetcore-2.2
        /// pour amélioration des bad request
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        //[Consumes("application/json-patch+json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public virtual async Task<IActionResult> PostAsync([FromBody] TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                var createdEntity = await _dbRepository.CreateAsync(entity);
                if (createdEntity == null)
                    return BadRequest($"L'id du dto envoyé {dto} => Id = {dto.Id} est non null, impossible de l'insérer en base");
                var correspondingDto = _mapper.Map<TDto>(createdEntity);
                return CreatedAtAction(nameof(GetAsync), new { id = correspondingDto.Id }, correspondingDto);
            }
            catch (UndesiredBorderEffectException ubee)
            {
                return BadRequest(ubee.Message);
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
        public virtual async Task<IActionResult> PutAsync(Guid id, [FromBody] TDto dto) // Guid à passer en FromQuerry???
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto); // automapper plante si la forme du Dto n'est pas bonne -> BadRequest?
                var updatedEntity = await _dbRepository.UpdateAsync(entity);
                if (updatedEntity == null)
                    return NotFound();
                return Ok(_mapper.Map<TDto>(updatedEntity));
            }
            catch (UndesiredBorderEffectException ubee)
            {
                return BadRequest(ubee.Message);
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
        public virtual async Task<IActionResult> DeleteAsync(Guid id) 
        {
            try
            {
                var response = await _dbRepository.DeleteByIdAsync(id);
                if (response == null)  // id non trouvé en base
                    return NotFound();
                // bool == true car ce bool en particulier peut etre null! (on ne peut pas faire if(bool?) directement!)
                if (response == true) // si vrai le delete à fonctionné
                    return Ok(true);
                // Ni null, ni vrai, alors faux, id correct mais pas de suppression en base
                //return StatusCode(500);
                return Ok(false); // serait peut être mieux... ou alors renvoyé une erreur custom?
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
