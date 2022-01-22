using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Models.Ingredients
{
    public class HopModel : IngredientModel
    {
        public float AlphaAcid { get; internal set; }
        public HopModel(string name, float alphaacid) : base(name)
        {
            AlphaAcid = alphaacid;
        }
    }
}
