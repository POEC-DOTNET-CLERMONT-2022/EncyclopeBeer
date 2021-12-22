using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerExercice
{
    internal class ConsoleReader : IReader
    {
        public IWriter Writer { get; set; }

        public Beer ReadBeer()
        {
            // Implémentation Beer ici
            return new Beer();
        }

        /// <summary>
        /// Ask the user for an integer and return it. Continue ask until user enter an int
        /// </summary>
        /// <returns></returns>
        internal int ReadIntFromUser()
        {
            Console.WriteLine("Choisissez un int : ");
            
            int index;
            if (int.TryParse(Console.ReadLine(), out index))
            {
                return index;
            }
            else
            {
                Console.WriteLine("La valeur entrée n'est pas un int. Réessayez.");
                return ReadIntFromUser();
            }

        }



    }
}
