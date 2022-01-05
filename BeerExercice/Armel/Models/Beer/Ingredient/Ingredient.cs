namespace WikiBeer.Models
{
    public class Ingredient
    {
        public Guid Id { get; private set; }
        public string Name { get; internal set; }

        public Ingredient(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}