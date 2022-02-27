using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Models
{
    public class ConnectionInfosModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }

        public ConnectionInfosModel(string id, string email, bool isVerified)
        {
            Id = id;
            Email = email;
            IsVerified = isVerified;
        }
    }
}
