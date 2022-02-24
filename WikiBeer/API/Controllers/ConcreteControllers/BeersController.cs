using AutoMapper;
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
    public class BeersController : GenericController<BeerEntity,BeerDto>
    {
        public BeersController(IGenericRepository<BeerEntity> dbRepository, IMapper mapper, ILogger logger)
            : base(dbRepository, mapper, logger)
        {
        }
    }
}