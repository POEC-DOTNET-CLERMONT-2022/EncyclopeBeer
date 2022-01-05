using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiBeer.Models
{
    public interface IReader
    {
        public IWriter Writer { get; set; }

        public Beer ReadBeer();
        public int ReadIntFromUser();
        public float ReadFloatFromUser();
        public BeerColor ReadColorFromUser();
        public BeerStyle ReadStyleFromUser();

    }
}
