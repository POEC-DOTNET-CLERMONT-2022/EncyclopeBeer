using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiBeer.Models
{
    internal class Cereal : Ingredient
    {
        public float EBC { get; internal set; }

        public Cereal(string name, float ebc) : base(name)
        {
            EBC = ebc;
        }
    }
}
