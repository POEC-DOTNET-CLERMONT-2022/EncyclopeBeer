using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Model.Ingredients
{
    public class Cereal : Ingredient
    {
        public float EBC { get; internal set; }

        public Cereal(string name, float ebc) : base(name)
        {
            EBC = ebc;
        }
    }
}
