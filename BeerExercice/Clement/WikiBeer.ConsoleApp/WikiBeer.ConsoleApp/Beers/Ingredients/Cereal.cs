namespace WikiBeer.ConsoleApp.Beers.Ingredients
{
    internal class Cereal : Ingredient
    {
        public float EBC { get; private set; }

        public Cereal(string name, float ebc) : base(name, IngredientType.Cereal)
        {
            EBC = ebc;
        }
    }
}
