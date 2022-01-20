namespace Dtos
{
    public abstract class IngredientDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IngredientDto() { }

        //    public IngredientDto(string name, string description)
        //    {
        //        Name = name;
        //        Description = description;
        //    }
    }
}