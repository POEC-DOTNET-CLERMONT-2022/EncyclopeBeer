namespace WikiBeer.ConsoleApp.Beers.Ingredients
{
    internal class Hop : Ingredient
    {
        public float AlphaAcid { get; }

        public Hop(string name, float alphaAcid) : base(name, IngredientType.Hop)
        {
            AlphaAcid = alphaAcid;
        }
    }
}
