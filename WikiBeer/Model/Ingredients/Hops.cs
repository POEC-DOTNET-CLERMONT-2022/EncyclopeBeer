using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Model.Ingredients
{
    public class Hops : Ingredient
    {
        public float AlphaAcid { get; internal set; }
        public Hops(string name, float alphaacid) : base(name)
        {
            AlphaAcid = alphaacid;
        }
    }
}
