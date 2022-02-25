using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities
{
    public class ImageEntity : IEntity
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; }
    }
}
