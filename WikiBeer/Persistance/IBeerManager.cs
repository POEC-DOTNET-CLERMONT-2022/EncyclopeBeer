﻿using Ipme.WikiBeer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Persistance
{
    public interface IBeerManager
    {
        IEnumerable<Beer> GetAllBeer();
    }
}
