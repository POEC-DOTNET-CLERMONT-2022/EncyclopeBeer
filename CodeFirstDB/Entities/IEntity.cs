using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Un Héritage en asbract serait peut être mieux (éviterai de coller des guid partout et serait donc implicitement présent)
/// </summary>
namespace Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
