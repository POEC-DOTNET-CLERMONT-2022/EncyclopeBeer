using WikiBeer.ConsoleApp.Beers.Ingredients;

namespace WikiBeer.ConsoleApp.Beers
{
    internal class Beer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Brewery { get; private set; }
        public double Ibu { get; private set; }
        public double Degree { get; private set; }
        public string Style { get; private set; }

        /* See later
        public string Flavour { get; private set; }
        public string Taste { get; private set; }
        public string Appearance { get; private set; }
        private List<Ingredient> Ingredients = new List<Ingredient>();
        private IEnumerable<BeerColor>  commence a me faire chier celle la !
        */

        /// <summary>
        /// Return a Beer object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ibu"></param>
        /// <param name="degree"></param>
        /// <param name="flavour"></param>
        /// <param name="taste"></param>
        /// <param name="appearance"></param>
        /// <param name="brewery"></param>
        public Beer(string name, double ibu, double degree, string brewery, string style)
        {
            Id = Guid.NewGuid();
            Name = name;
            Brewery = brewery;
            Ibu = ibu;
            Degree = degree;
            Style = style;

            /* See later
            Flavour = flavour;
            Taste = taste;
            Appearance = appearance;
            */
        }

        /// <summary>
        /// Return object as a string
        /// </summary>
        /// <returns>Return Object Beer as a string</returns>
        public override string ToString()
        {
            return $" - Nom: {Name}\n - IBU: {Ibu}\n - Degrés: {Degree}%";
            //return $" - Nom: {Name}\n - IBU: {Ibu}\n - Degrés: {Degree}%\n - Arôme: {Flavour}\n - Gout: {Taste}\n - Apparence: {Appearance}\n - Brasserie : {Brewery}";
        }
    }
}
