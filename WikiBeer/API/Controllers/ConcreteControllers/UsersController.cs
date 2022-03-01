using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ipme.WikiBeer.API.Controllers.ConcreteControllers
{
    public class UsersController : GenericController<UserEntity, UserDto>
    {
        private new readonly UserRepository _dbRepository;
        //private readonly UserRepository DbRepository;
        public UsersController(UserRepository dbRepository, IMapper mapper, ILogger<UsersController> logger)
            : base(dbRepository, mapper, logger)
        {
            _dbRepository = dbRepository;
        }

        [HttpGet("connection/{connectionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [EnableCors("Open")]
        //[EnableCors("LocalPolicy")]
        public async Task<ActionResult<UserDto>> GetAsync(string connectionId)
        {
            try
            {
                var entity = await _dbRepository.GetByConnectionIdAsync(connectionId);
                return Ok(_mapper.Map<UserDto>(entity));
            }
            catch (EntryNotFoundException e)
            {
                _logger.LogWarning(e, $"{_errInfo} GET(id) : trying to get entity with Id : {connectionId} cause {e.Message}");
                return NotFound();
            }
            catch (AutoMapperMappingException e)
            {
                _logger.LogError(e, $"{_errInfo} GET(id) : trying to map Entity ({typeof(UserEntity).Name})" +
                    $" from Dto ({typeof(UserDto).Name}) sharing Id : {connectionId} cause {e.Message}");
                return StatusCode(500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{_errInfo} GET(id) : Entity {typeof(UserEntity)}. {e.Message})");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/favoriteBeers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [EnableCors("Open")]
        //[EnableCors("LocalPolicy")]
        public async Task<ActionResult<IEnumerable<BeerEntity>>> GetAsync(Guid id)
        {
            try
            {
                var allDtos = _mapper.Map<IEnumerable<BeerDto>>(await _dbRepository.GetFavoriteBeersAsync(id));
                return Ok(allDtos);
            }
            catch (AutoMapperMappingException e)
            {
                _logger.LogError(e, $"{_errInfo} GET : trying to map Entities ({typeof(BeerEntity).Name})" +
                    $" from Dtos ({typeof(BeerDto).Name}) cause {e.Message}");
                return StatusCode(500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{_errInfo} GET : Entities {typeof(BeerEntity).Name}. {e.Message})");
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [EnableCors("Open")]
        public override async Task<IActionResult> PutAsync(Guid id, [FromBody] UserDto dto)
        {
            return await base.PutAsync(id, dto);
        }

    }
}
