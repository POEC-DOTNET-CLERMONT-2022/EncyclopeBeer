using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities
{
    public class ConnectionInfosEntity
    {
        public string Id { get; private set; }
        public string Email { get; private set; }
        public bool IsVerified { get; private set; }
        //public UserEntity User { get; private set; }

        public ConnectionInfosEntity(string id, string email, bool isVerified)
        {
            Id = id;
            Email = email;
            IsVerified = isVerified;
        }

        //public ConnectionInfosEntity(string id, string email, bool isVerified, UserEntity user)
        //    : this(id,email,isVerified)
        //{
        //    User = user;
        //}
    }
}
