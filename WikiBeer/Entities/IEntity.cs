using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Interface devant être respecté par toutes les entities (pour le repo générique)
/// </summary>
namespace Ipme.WikiBeer.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
