using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities
{
    public class UserEntity : IEntity
    {
        public Guid Id { get; private set; }
        public string NickName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Email { get; private set; }
        public int HashCode { get; private set; } // pour le password (qui ne doit pas transiter en dehors du front)
        public bool IsCertified { get; private set; }

        public CountryEntity? Country { get; private set; }

        public IEnumerable<UserBeer> UserBeers { get; private set; }

        private UserEntity(Guid id, string nickName, DateTime birthDate, string email, int hashCode, bool isCertified)
        {
            Id = id;
            NickName = nickName;
            BirthDate = birthDate;
            Email = email;
            HashCode = hashCode;
            IsCertified = isCertified;
        }

        public UserEntity(Guid id, string nickName, DateTime birthDate, string email, int hashCode, bool isCertified,
            CountryEntity? country, IEnumerable<UserBeer> userBeers)
            : this(id, nickName, birthDate, email, hashCode, isCertified)
        {
            Country = country;
            UserBeers = userBeers;
        }

    }
}
