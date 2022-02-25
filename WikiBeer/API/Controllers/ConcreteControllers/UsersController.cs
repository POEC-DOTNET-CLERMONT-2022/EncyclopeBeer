using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Persistance.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ipme.WikiBeer.API.Controllers.ConcreteControllers
{
    public class UsersController : GenericController<UserEntity, UserDto>
    {
        public UsersController(IGenericRepository<UserEntity> dbRepository, IMapper mapper, ILogger<UsersController> logger)
            : base(dbRepository, mapper, logger)
        {
        }
    }
}
