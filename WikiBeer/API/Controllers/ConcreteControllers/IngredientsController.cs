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
/// https://mrczetty.houseofczetty.com/2013/10/31/abstract-classes-controller-params/
/// </summary>
namespace Ipme.WikiBeer.API.Controllers
{
    public class IngredientsController : GenericController<IngredientEntity, IngredientDto>
    {
        public IngredientsController(IGenericRepository<IngredientEntity> dbRepository, IMapper mapper, ILogger logger)
            : base(dbRepository,mapper, logger)
        {
        }
    }
}
