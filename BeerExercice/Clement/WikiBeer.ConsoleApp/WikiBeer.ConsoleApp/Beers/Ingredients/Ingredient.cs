namespace WikiBeer.ConsoleApp.Beers.Ingredients
{
    public abstract class Ingredient
    {
        public Guid Id { get; }
        public string Name { get; }
        public IngredientType Type { get; }

        public Ingredient(string name, IngredientType type)
        {
            Id = Guid.NewGuid();

            if (name == null)
                throw new ArgumentNullException("name cannot be null");
            else if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name cannot be an empty string or have space characters only.");
            }

            Name = name;
            Type = type;
        }
    }
}