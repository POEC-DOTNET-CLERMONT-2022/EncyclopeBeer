namespace WikiBeer.ConsoleApp.Beers.Ingredients
{
    internal class Additive : Ingredient
    {
        public string Use { get; set; }

        public Additive(string name, string use) : base(name, IngredientType.Cereal)
        {
            Use = use;
        }
    }
}
