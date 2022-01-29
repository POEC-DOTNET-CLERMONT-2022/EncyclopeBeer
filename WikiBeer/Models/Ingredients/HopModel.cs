using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Models.Ingredients
{
    public class HopModel : IngredientModel
    {
        public float AlphaAcid { get; internal set; }

        public HopModel(string name, string description, float alphaAcid)
            : this(Guid.Empty, name, description, alphaAcid)
        {
        }
        public HopModel(Guid id, string name, string description, float alphaAcid) : base(id, name, description)
        {
            AlphaAcid = alphaAcid;
        }
    }
}
