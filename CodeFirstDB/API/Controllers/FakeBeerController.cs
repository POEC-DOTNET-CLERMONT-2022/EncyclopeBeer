using AutoMapper;
using Dtos;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Perstistance;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeBeerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFakeBeerRepository _ddbRepository; // pour ne pas avoir à réecrire les CRUD...

        public FakeBeerController(IFakeBeerRepository ddbRepository, IMapper mapper)
        {
            _ddbRepository = ddbRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<BeerDto> Get()
        {
            var beers = _ddbRepository.GetAll();
            var allBeers = _mapper.Map<IEnumerable<BeerDto>>(beers);
            return allBeers;
        }

        //[HttpGet("Entities")]
        //public IEnumerable<BeerEntity> GetEntities()
        //{
        //    var allEntities = _ddbRepository.GetAll();
        //    var allDtos = _mapper.Map<IEnumerable<BeerDto>>(allEntities);
        //    return allEntities;
        //}

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BeerDto))] // gestion des codes de retour
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Action Result permet de renvoyer un type et/ou un code d'errur
        public ActionResult<BeerDto> Get([FromQuery]Guid id) 
        {
            var beerEntity = _ddbRepository.GetById(id);

            if (beerEntity == null)
            {
                return NotFound();
            }
            var mapProj = _mapper.Map<BeerDto>(beerEntity);

            return Ok(_mapper.Map<BeerDto>(beerEntity));
        }

        [HttpGet("name/{name}")]
        public BeerDto Get([FromQuery] string name) // Query ne suffit pas à faire la différence dans le routing (c'est juste plus verbeux pour la requête)
        {
            var beer = _ddbRepository.GetByName(name);
            return _mapper.Map<BeerDto>(beer);
        }

        // Pour vérifier la bonne conduite du Mappeur
        // Version sans ActionResult pour tester ce truc...
        //[HttpGet("GetEntityById/{id}")] 
        //public BeerEntity GetEntity(Guid id)
        //{
        //    var beer = _ddbRepository.GetById(id);
        //    return beer;// _mapper.Map<BeerDto>(beer);
        //}

        ///// Routing à refaire ici!

        //[HttpGet("GetByName/{name}")]
        //public BeerDto GetByName([FromQuery] string name) // Query pour test
        //{
        //    var beer = _ddbRepository.GetByName(name);
        //    return _mapper.Map<BeerDto>(beer);
        //}

        // POST api/<BeerController> -> Create
        [HttpPost]
        public void Post([FromBody] BeerDto beerDto)
        {
            var beerEntity = _mapper.Map<BeerEntity>(beerDto);
            _ddbRepository.Create(beerEntity);
        }

        [HttpPut("{id}")]
        public void PutById(Guid id, [FromBody] BeerDto beerDto)
        {
            // A implémenter une fois implémenté dans le repos (avec Action Result pour test)
        }

        // DELETE api/<BeerController>/5
        [HttpDelete("{id}")]
        public bool DeleteById(Guid id) // on pourrait retourner un boléen ici
        {
            return _ddbRepository.DeleteById(id);
        }
    }
}
