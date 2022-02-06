using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Models
{
    public interface IDeepClonable<T> where T : class
    {
        T DeepClone();        
    }
}
