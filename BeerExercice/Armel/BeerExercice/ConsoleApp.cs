using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiBeer.Models;

namespace WikiBeer.ConsoleApp
{
    internal class ConsoleApp
    {

        private enum MenuOption
        {
            addbeer = 1,
            removebeer = 2,
            updatebeer = 3,
            listallbeer = 4,
            exit = 5
        }

        public void Run()
        {
            var BeerManager = new BeerManager(new ConsoleReader(), new ConsoleWriter());

            bool exit = false;

            do
            {
                var userChoice = GetUserOption();

                switch (userChoice)
                {
                    case MenuOption.addbeer:
                        BeerManager.Create();
                        break;
                    case MenuOption.removebeer:
                        BeerManager.Delete();
                        break;
                    case MenuOption.updatebeer:
                        BeerManager.Update();
                        break;
                    case MenuOption.listallbeer:
                        BeerManager.ReadAll();
                        break;
                    case MenuOption.exit:
                        exit = true;
                        break;
                }
            } while (!exit);

        }

        private MenuOption GetUserOption()
        {
            MenuOption response;
            bool optionSuccess;

            do
            {
                //Console.Clear(); /Clear le terminal, mais le fait très vite, pas pratique pour les messages d'erreur ou de mauvaises utilisation

                Console.WriteLine("Please choose one of the following option :");
                Console.WriteLine("1 - Add a beer");
                Console.WriteLine("2 - Remove a beer");
                Console.WriteLine("3 - Update a beer");
                Console.WriteLine("4 - Display avaible beers");
                Console.WriteLine("5 - Exit\n");
                Console.WriteLine("You choice :");

                var responseString = Console.ReadLine();

                optionSuccess = Enum.TryParse(responseString, out response);

                if (!optionSuccess)
                    Console.WriteLine("Wrong option, please enter 1");

            } while (!optionSuccess);

            return response;
        }


    }
}
