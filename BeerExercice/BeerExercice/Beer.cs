namespace BeerExercice
{
    internal class Beer
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public float Ibu { get; private set; }
        public float Degree { get; private set; }
        public string Flavour { get; private set; }
        public string Taste { get; private set; }
        public string Appearance { get; private set; }
        public string Brewery { get; private set; }

        private List<Ingredient> Ingredients = new List<Ingredient>();

        private List<BeerStyle> BeerStyles = new List<BeerStyle>();

        private List<BeerColor> BeerColors = new List<BeerColor>();

        public Beer(int id, string name, float ibu, float degree, string flavour, string taste, string appearance, string brewery)
        {
            Id = id;
            Name = name;
            Ibu = ibu;  
            Degree = degree;
            Flavour = flavour;
            Taste = taste;
            Appearance = appearance;
            Brewery = brewery;
        }

        public override string? ToString()
        {
            return $" - Nom: {Name}\n - IBU: {Ibu}\n - Degrés: {Degree}%\n - Arôme: {Flavour}\n - Gout: {Taste}\n - Apparence: {Appearance}\n - Brasserie : {Brewery}";
        }

    }
}
