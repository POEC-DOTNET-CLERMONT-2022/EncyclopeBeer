using Ipme.WikiBeer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Persistance.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }

        public async Task<UserEntity> GetByConnectionIdAsync(string connectionId)
        {
            return await Context.Set<UserEntity>().FirstOrDefaultAsync(u => u.ConnectionInfos.Id == connectionId)
                ?? throw new EntryNotFoundException($"{_errInfo} : GetByConnectionIdAsync. Entity {typeof(UserEntity).Name} Id : {connectionId} not found in base.");
        }

        public async Task<IEnumerable<BeerEntity>> GetFavoriteBeersAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            var favoriteBeerIds = user.UserBeers.Select(ub => ub.BeerId);
            var beers = Context.Set<BeerEntity>().Where(b => favoriteBeerIds.Contains(b.Id)); 
            return await beers.ToListAsync();
        }
    }
}
