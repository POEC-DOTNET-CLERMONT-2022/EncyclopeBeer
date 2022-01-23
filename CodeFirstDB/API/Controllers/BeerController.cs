using AutoMapper;
using Contexts;
using Dtos;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Perstistance;
using Perstistance.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


/// <summary>
/// Idée : et pourquoi pas un controller générique pour gérer tt les CRUD? Il suffirait de redéfinir les décorations
/// au niveau du controller... On pourrait ensuite hériter de Controler<SpecicfEntityType,CorresponddingDtoType> pour 
/// implémenter des requêtes plus élaboré spécifique à chaque type (avec details par exemple).
/// -> Des problèmes dans la différenciation des type lors des requêtes??? (Surement car on serialize...) les génériques ont l'air 
/// d'etre run time en plus, sa ne devrait donc pas être possible...
/// </summary>
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IMapper _mapper;
        //private readonly IBeerRepository _beerManager; // pour ne pas avoir à réecrire les CRUD...
        private readonly IGenericDbRepository<BeerEntity> _ddbRepository; // pour ne pas avoir à réecrire les CRUD...
        //private readonly WikiBeerSqlContext _beerContext; // avec sa on doit se retapper les crude en version API

        //public BeerController(IBeerRepository beerManager, IMapper beerMapper)
        public BeerController(IGenericDbRepository<BeerEntity> ddbRepository, IMapper mapper)
        //public BeerController(WikiBeerSqlContext beerContext, IMapper beerMapper)
        {
            //_beerContext = beerContext;
            _ddbRepository = ddbRepository;
            _mapper = mapper;
        }

        // GET: api/<BeerController>
        [HttpGet]
        public IEnumerable<BeerDto> Get()
        {
            var allBeers = _mapper.Map<IEnumerable<BeerDto>>(_ddbRepository.GetAll());
            return allBeers;
        } 

        // GET api/<BeerController>/5
        [HttpGet("GetById/{id}")]
        public BeerDto Get(Guid id)
        {
            var beer = _ddbRepository.GetById(id);
            return _mapper.Map<BeerDto>(beer);
        }

        [HttpPost]
        public void Post([FromBody] BeerDto beerDto)
        {
            var beerEntity = _mapper.Map<BeerEntity>(beerDto);
            _ddbRepository.Create(beerEntity);
        }

    }
}
