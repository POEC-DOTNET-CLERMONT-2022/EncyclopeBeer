using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Model.Ingredients
{
    public class Ingredient
    {
        public Guid Id { get; protected set; }
        public string Name { get; internal set; }

        public Ingredient(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
