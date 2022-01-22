using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ipme.WikiBeer.Dtos.Ingredients
{
    internal class AdditiveDto : IngredientDto
    {
        public string Use { get; set; }
    }
}
