namespace Models
{
    public class IngredientModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IngredientModel(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}