using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerExercice
{
    internal interface IWriter
    {
        public void Display(string my_str);

        public void DisplayBeer(Beer my_beer);
    }
}
