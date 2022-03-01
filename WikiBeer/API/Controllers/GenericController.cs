using AutoMapper;
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
        protected readonly IMapper _mapper;
        protected readonly IGenericRepository<TEntity> _dbRepository;
        protected readonly ILogger _logger;
        protected readonly string _errInfo;
  
        //protected GenericController(IMapper mapper, ILogger logger)
        //{
        //    _mapper = mapper;
        //    _logger = logger;
        //    _errInfo = $"From {GetType().Name}";
        //}

        public GenericController(IGenericRepository<TEntity> dbRepository, IMapper mapper, ILogger logger)
            //: this(mapper, logger)
        {
            _dbRepository = dbRepository;
            _mapper = mapper;
            _logger = logger;
            _errInfo = $"From {GetType().Name}";
        }

        [HttpGet]
        [ProducesResponseType(200)] 
        [ProducesResponseType(500)]
        //[EnableCors("LocalPolicy")]
        [EnableCors("Open")]
        public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAsync()
        {
            try
            {
                var allDtos = _mapper.Map<IEnumerable<TDto>>(await _dbRepository.GetAllAsync());
                return Ok(allDtos);
            }
            catch (AutoMapperMappingException e)
            {
                _logger.LogError(e,$"{_errInfo} GET : trying to map Entities ({typeof(TEntity).Name})" +
                    $" from Dtos ({typeof(TDto).Name}) cause {e.Message}");
                return StatusCode(500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{_errInfo} GET : Entities {typeof(TEntity).Name}. {e.Message})");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        //[EnableCors("LocalPolicy")]
        public virtual async Task<ActionResult<TDto>> GetAsync(Guid id)
        {
            try
            {
                var entity = await _dbRepository.GetByIdAsync(id);                
                return Ok(_mapper.Map<TDto>(entity));
            }
            catch(EntryNotFoundException e)
            {
                _logger.LogWarning(e,$"{_errInfo} GET(id) : trying to get entity with Id : {id} cause {e.Message}");
                return NotFound();
            }
            catch (AutoMapperMappingException e)
            {
                _logger.LogError(e,$"{_errInfo} GET(id) : trying to map Entity ({typeof(TEntity).Name})" +
                    $" from Dto ({typeof(TDto).Name}) sharing Id : {id} cause {e.Message}");
                return StatusCode(500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{_errInfo} GET(id) : Entity {typeof(TEntity)}. {e.Message})");
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public virtual async Task<IActionResult> PostAsync([FromBody] TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                var createdEntity = await _dbRepository.CreateAsync(entity);                
                var correspondingDto = _mapper.Map<TDto>(createdEntity);
                return CreatedAtAction(nameof(GetAsync), new { id = correspondingDto.Id }, correspondingDto);
            }            
            catch (AutoMapperMappingException e) 
            {
                _logger.LogError(e, $"{_errInfo} POST : trying to map Dto <-> entity ({typeof(TDto).Name}" +
                    $" <-> ({typeof(TEntity).Name} sharing Id = {dto.Id}) cause {e.Message}");
                return StatusCode(500);
            }
            catch (UnauthorizedDbOperationException e)
            {
                _logger.LogWarning(e, $"{_errInfo} POST : trying to insert entity (mapped " +
                    $"from dto {dto.GetType().Name} : Id = {dto.Id}) cause {e.Message}");
                return BadRequest();
            }
            catch (EntityRepositoryException e)
            {
                _logger.LogError(e, $"{_errInfo} POST : trying to insert entity (mapped " +
                    $"from dto {dto.GetType().Name} : Id = {dto.Id}) cause {e.Message}");
                return StatusCode(500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{_errInfo} POST : Dto {typeof(TDto).Name}, Entity {typeof(TEntity).Name} sharing Id : {dto.Id}. {e.Message})");
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        //[EnableCors("LocalPolicy")]
        public virtual async Task<IActionResult> PutAsync(Guid id, [FromBody] TDto dto) // Guid à passer en FromQuerry???
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto); // automapper plante si la forme du Dto n'est pas bonne -> BadRequest?
                var updatedEntity = await _dbRepository.UpdateAsync(entity);                
                return Ok(_mapper.Map<TDto>(updatedEntity));
            }
            catch (AutoMapperMappingException e)
            {
                _logger.LogError(e, $"{_errInfo} PUT(id) : trying to map Dto <-> entity ({typeof(TDto).Name})" +
                    $" <-> ({typeof(TEntity).Name} sharing Id = {id} cause {e.Message}");
                return StatusCode(500);
            }
            catch (EntryNotFoundException e)
            {
                _logger.LogWarning(e, $"{_errInfo} PUT(id) : trying to modify entity (mapped " +
                    $"from dto {dto.GetType().Name} with Id = {id}) cause {e.Message}");
                return NotFound();
            }
            catch (EntityRepositoryException e)
            {
                _logger.LogError(e, $"{_errInfo} PUT(id) : trying to modify entity (mapped " +
                    $"from dto {dto.GetType().Name} with Id = {id}) cause {e.Message}");
                return StatusCode(500);
            }        
            catch (Exception e)
            {
                _logger.LogError(e, $"{_errInfo} PUT(id) : Dto {typeof(TDto).Name}, Entity {typeof(TEntity).Name} sharing Id : {dto.Id}. {e.Message})");
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
                await _dbRepository.DeleteByIdAsync(id);                                
                return Ok();
            }
            catch (EntryNotFoundException e)
            {
                _logger.LogWarning(e, $"{_errInfo} DELETE(id) : trying to delete entity ({typeof(TEntity).Name} with Id = {id}) cause {e.Message}");
                return NotFound();
            }
            catch (EntityRepositoryException e)
            {
                _logger.LogError(e, $"{_errInfo} DELETE(id) : trying to delete entity ({typeof(TEntity).Name} with Id = {id}) cause {e.Message}");
                return StatusCode(500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{_errInfo} DELETE(id) : {e.Message})");
                return StatusCode(500);
            }
        }
    }
}
