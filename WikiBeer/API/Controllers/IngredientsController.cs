using AutoMapper;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Entities.Ingredients;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// On ne peut pas utiliser une abstract classe pour déclarer le controlleur générique!
/// voir : https://stackoverflow.com/questions/5861241/can-abstract-class-be-a-parameter-in-a-controllers-action
/// A creuser via les notions de covariance et contravariances : 
/// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/
/// On voudra donc un controller par type d'ingrédient!
/// 
/// </summary>
namespace Ipme.Wiki.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<IngredientEntity> _ddbRepository;

        public IngredientsController(IGenericRepository<IngredientEntity> ddbRepository, IMapper mapper)
        {
            _ddbRepository = ddbRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IngredientDto>), 200)]
        [ProducesResponseType(500)]
        [EnableCors("LocalPolicy")]
        public IActionResult Get()
        {
            try
            {
                var allIngredients = _mapper.Map<IEnumerable<IngredientDto>>(_ddbRepository.GetAll());
                return Ok(allIngredients);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IngredientDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [EnableCors("LocalPolicy")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var Ingredient = _ddbRepository.GetById(id);
                if (Ingredient == null)
                    return NotFound();
                return Ok(_mapper.Map<IngredientDto>(Ingredient));
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
        public IActionResult Post([FromBody] IngredientDto IngredientDto)
        {
            try
            {
                var IngredientEntity = _mapper.Map<IngredientEntity>(IngredientDto);
                var IngredientEntityCreated = _ddbRepository.Create(IngredientEntity);
                var correspondingIngredientDto = _mapper.Map<IngredientDto>(IngredientEntityCreated);
                return CreatedAtAction(nameof(Get), new { id = correspondingIngredientDto.Id }, correspondingIngredientDto);
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
        public IActionResult Put(Guid id, [FromBody] IngredientDto IngredientDto) // Guid à passer en FromQuerry???
        {
            try
            {
                var IngredientEntity = _mapper.Map<IngredientEntity>(IngredientDto); // automapper plante si la forme du Dto n'est pas bonne -> BadRequest?
                var updatedIngredientEntity = _ddbRepository.Update(IngredientEntity);
                if (updatedIngredientEntity == null)
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
