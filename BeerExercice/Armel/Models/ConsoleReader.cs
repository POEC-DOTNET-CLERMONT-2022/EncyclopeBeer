using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiBeer.Models
{
    public class ConsoleReader : IReader
    {
        public IWriter Writer { get; set; }

        public Beer ReadBeer()
        {
            Writer.Display("Beer name ? ");
            var name = Console.ReadLine();

            Writer.Display("Beer ibu ? ");
            var ibu = ReadFloatFromUser();

            Writer.Display("Beer degree ? ");
            var degree = ReadFloatFromUser();

            Writer.Display("Beer color ? ");
            var color = ReadColorFromUser();

            Writer.Display("Beer style ? ");
            var style = ReadStyleFromUser();

            // TODO
            var ingredients = new List<Ingredient>();

            return new Beer(name, ibu, degree, color, style, ingredients);
        }

        /// <summary>
        /// Ask the user for an integer and return it. Continue ask until user enter an int
        /// </summary>
        /// <returns></returns>
        public int ReadIntFromUser()
        {
            Writer.Display($"Choose an integer : ");
            int user_int;

            if (int.TryParse(Console.ReadLine(), out user_int))
            {
                return user_int;
            }
            else
            {
                Writer.Display($"Given value is not an integer. Retry.");
                return ReadIntFromUser();
            }
        }

        public float ReadFloatFromUser()
        {
            Writer.Display($"Choose a float : ");
            float user_float;

            if (float.TryParse(Console.ReadLine(), out user_float))
            {
                return user_float;
            }
            else
            {
                Writer.Display($"Given value is not a float. Retry.");
                return ReadFloatFromUser();
            }
        }

        public BeerColor ReadColorFromUser() // il faut creuser les méthodes générique dans les interfaces pour améliorer sa
        {
            BeerColor response; 
            bool optionSuccess;

            do
            {
                Writer.Display("Please choose one of the following option :");
                Writer.DisplayBeerColors();

                var responseString = Console.ReadLine();

                optionSuccess = Enum.TryParse(responseString, out response);

                if (!optionSuccess)
                    Console.WriteLine("Wrong option, please enter a valid choice");

            } while (!optionSuccess);

            return response;
        }

        public BeerStyle ReadStyleFromUser()
        {
            BeerStyle response;
            bool optionSuccess;

            do
            {
                Writer.Display("Please choose one of the following option :");
                Writer.DisplayBeerStyles();

                var responseString = Console.ReadLine();

                optionSuccess = Enum.TryParse(responseString, out response);

                if (!optionSuccess)
                    Console.WriteLine("Wrong option, please enter a valid choice");

            } while (!optionSuccess);

            return response;
        }

    }
}
