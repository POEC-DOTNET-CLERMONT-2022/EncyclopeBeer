using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerExercice
{
    internal interface IReader
    {
        public IWriter Writer { get; set; }

        public Beer ReadBeer();

    }
}
