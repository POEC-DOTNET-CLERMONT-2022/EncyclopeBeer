using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities
{
    public class UserBeer
    {
        public Guid UserId { get; private set; }
        public UserEntity User { get; private set; }

        public Guid BeerId { get; private set; }
        public BeerEntity Beer { get; private set; }

        public UserBeer(Guid userId, Guid beerId)
        {
            UserId = userId;
            BeerId = beerId;        }

        public UserBeer(Guid userId, Guid beerId, UserEntity user, BeerEntity beer)
            : this(userId, beerId)
        {
            User = user;
            Beer = beer;
        }
    }
}
