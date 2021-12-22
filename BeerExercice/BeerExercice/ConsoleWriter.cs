using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerExercice
{
    internal class ConsoleWriter : IWriter
    {
        public void Display(string my_string)
        {
            Console.WriteLine(my_string);
        }

        public void DisplayBeer(Beer my_beer)
        {

        }

    }
}
