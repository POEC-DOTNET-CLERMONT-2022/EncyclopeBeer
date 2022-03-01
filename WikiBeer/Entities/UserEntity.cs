using Ipme.WikiBeer.Entities.AssociationTables;
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
        public string Nickname { get; private set; }
        public DateTime BirthDate { get; private set; }
       
        public bool IsCertified { get; private set; }

        public ConnectionInfosEntity ConnectionInfos { get; private set; }

        public CountryEntity? Country { get; private set; }

        public IEnumerable<UserBeer> UserBeers { get; private set; }

        private UserEntity(Guid id, string nickname, DateTime birthDate, bool isCertified)
        {
            Id = id;
            Nickname = nickname;
            BirthDate = birthDate;
            IsCertified = isCertified;
        }

        public UserEntity(Guid id, string nickname, DateTime birthDate, string email, int hashCode, bool isCertified,
            ConnectionInfosEntity connectionInfos, CountryEntity? country, IEnumerable<UserBeer> userBeers)
            : this(id, nickname, birthDate, isCertified)
        {
            ConnectionInfos = connectionInfos;
            Country = country;
            UserBeers = userBeers;
        }

    }
}
