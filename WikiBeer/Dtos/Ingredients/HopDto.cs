using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ipme.WikiBeer.Dtos.Ingredients
{
    internal class HopDto : IngredientDto
    {
        public float AlphaAcid { get; set; }
    }
}
