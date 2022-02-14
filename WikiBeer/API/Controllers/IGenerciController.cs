using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ipme.WikiBeer.API.Controllers
{
    public interface IGenerciController<TDto>        
        where TDto : class, IDto
    {
        public Task<ActionResult<IEnumerable<TDto>>> GetAsync();
        public Task<ActionResult<TDto>> GetAsync(Guid id);
        public Task<IActionResult> PostAsync([FromBody] TDto dto);
        public Task<IActionResult> PutAsync(Guid id, [FromBody] TDto dto);
    }
}
