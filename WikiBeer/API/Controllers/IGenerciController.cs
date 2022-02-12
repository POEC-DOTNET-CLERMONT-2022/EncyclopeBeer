using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ipme.WikiBeer.API.Controllers
{
    public interface IGenerciController<TDto>        
        where TDto : class, IDto
    {
        public ActionResult<IEnumerable<TDto>> Get();
        public ActionResult<TDto> Get(Guid id);
        public IActionResult Post([FromBody] TDto dto);
        public IActionResult Put(Guid id, [FromBody] TDto dto);
    }
}
