using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities.AssociationTables
{
    public interface IAssociationTable : IEquatable<IAssociationTable> 
    {
        bool IsInCompositeKey(Guid id);

        (Guid,Guid) GetCompositeKey();
    }
}
