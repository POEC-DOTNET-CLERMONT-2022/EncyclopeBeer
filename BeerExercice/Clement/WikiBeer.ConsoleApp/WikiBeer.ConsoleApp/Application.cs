
using WikiBeer.ConsoleApp.Beers;

namespace WikiBeer.ConsoleApp
{
    internal enum MenuOption
    {
        AddBeer = 1,
        ListBeers = 2,
        UpdateBeer = 3,
        DeleteBeer = 4,
        Exit = 5
    }

    internal enum BeerProperties
    {
        name = 1,
        brewery = 2,
        degree = 3,
        ibu = 4,
        color = 5,
        style = 6
    }

    internal class Application
    {
        /// <summary>
        /// Run the main program loop
        /// </summary>
        public void Run()
        {
            bool exit = false;
            BeerManager manager = new BeerManager();

            do
            {
                var userResponse = GetUserOption();

                switch (userResponse)
                {
                    case MenuOption.AddBeer:
                        var name = GetBeerProperty("name");
                        var brewery = GetBeerProperty("brewery");
                        var degree = Double.Parse(GetBeerProperty("degree"));
                        var ibu = Double.Parse(GetBeerProperty("ibu"));
                        var color = GetBeerProperty("color");
                        var style = GetBeerProperty("style");
                        Beer beer = new Beer(name, ibu, degree, brewery, style);
                        manager.AddBeer(beer);
                        break;
                    case MenuOption.ListBeers:
                        IEnumerable<Beer> beers = manager.Beers;
                        foreach (var b in beers)
                        {
                            Console.WriteLine($"Name : {b.Name}");
                            Console.WriteLine($"Brewery : {b.Brewery}");
                            Console.WriteLine($"Degree : {b.Degree}");
                            Console.WriteLine($"Ibu : {b.Ibu}");
                            Console.WriteLine($"Style : {b.Style}");
                            Console.WriteLine($"\n" +
                                $"_________________" +
                                $"\n");
                        }

                        var toto = Console.ReadLine();
                        break;
                    case MenuOption.UpdateBeer:
                        break;
                    case MenuOption.DeleteBeer:
                        break;
                    case MenuOption.Exit:
                        exit = true;
                        break;
                }

            } while (!exit);
        }

        private static MenuOption GetUserOption()
        {
            MenuOption response;
            bool optionSuccess;

            do
            {
                Console.Clear();

                Console.WriteLine("Please choose one of the following option :");
                Console.WriteLine("1 - Add a beer");
                Console.WriteLine("2 - List all beer");
                Console.WriteLine("3 - Update a beer");
                Console.WriteLine("4 - Delete a beer");
                Console.WriteLine("5 - Exit\n");
                Console.WriteLine("You choice :");

                var responseString = Console.ReadLine();

                optionSuccess = Enum.TryParse(responseString, out response);

                if (!optionSuccess)
                    Console.WriteLine("Wrong option, please enter 1");

            } while (!optionSuccess);

            return response;
        }

        private static string GetBeerProperty(string prop)
        {
            bool optionSuccess;
            string response;

            do
            {
                Console.Clear();
                Console.WriteLine($"Beer {prop} :");

                var userResponse = Console.ReadLine();

                optionSuccess = IsNotNullOrWhiteSpace(userResponse, out response);

                if (!optionSuccess) 
                {
                    Console.WriteLine("Your answer is not valid");
                }

            } while (!optionSuccess);

            return response;
        }

        private static bool IsNotNullOrWhiteSpace(string value, out string returnValue)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                returnValue = value;
                return true;
            }
            else
            {
                returnValue = null;
                return false;
            }
        }
    }
}
