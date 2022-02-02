using AutoMapper;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Entities.Ingredients;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// On ne peut pas utiliser une abstract classe pour déclarer le controlleur générique!
/// voir : https://stackoverflow.com/questions/5861241/can-abstract-class-be-a-parameter-in-a-controllers-action
/// A creuser via les notions de covariance et contravariances : 
/// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/
/// On voudra donc un controller par type d'ingrédient!
/// 
/// </summary>
namespace Ipme.WikiBeer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<IngredientEntity> _dbRepository;

        public IngredientsController(IGenericRepository<IngredientEntity> dbRepository, IMapper mapper)
        {
            _dbRepository = dbRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IngredientDto>), 200)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            try
            {
                var allIngredients = _mapper.Map<IEnumerable<IngredientDto>>(_dbRepository.GetAll());
                return Ok(allIngredients);
            }
            catch (Exception e)
            {
                // toutes les exceptions non géré passe en 500
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IngredientDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(Guid id)
        {
            try
            {
                var ingredient = _dbRepository.GetById(id);
                if (ingredient == null)
                    return NotFound();
                return Ok(_mapper.Map<IngredientDto>(ingredient));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }
    }
}
