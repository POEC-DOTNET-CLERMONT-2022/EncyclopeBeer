using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ipme.WikiBeer.Dtos.Ingredients
{
    public class IngredientDto : IDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}
