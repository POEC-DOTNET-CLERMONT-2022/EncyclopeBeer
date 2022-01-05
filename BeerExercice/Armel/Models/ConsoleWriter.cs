using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiBeer.Models
{
    public class ConsoleWriter : IWriter
    {
        public void Display(string my_string)
        {
            Console.WriteLine(my_string);
        }

        public void DisplayBeer(Beer my_beer)
        {
            Console.WriteLine(my_beer);
        }

        //public void DisplayMyEnum<MyEnum>() where MyEnum : Enum // une struc perso au lieu d'une enum est peut être la solution
        //{
        //    foreach (MyEnum val in Enum.GetValues(typeof(MyEnum)))
        //    {
        //        Console.WriteLine($"{val} - {val.getString()}");
        //    }
        //}

        public void DisplayBeerColors()
        {
            foreach (BeerColor colors in Enum.GetValues(typeof(BeerColor)))
            {
                Console.WriteLine($"{(int)colors} - {colors.GetString()}");
            }
        }

        public void DisplayBeerStyles()
        {
            foreach (BeerStyle style in Enum.GetValues(typeof(BeerStyle)))
            {
                Console.WriteLine($"{(int)style} - {style.GetString()}");
            }
        }

    }
}
