using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiBeer.Models
{
    internal class Hops : Ingredient
    {
        public float AlphaAcid { get; internal set; }     
        public Hops(string name, float alphaacid): base(name)
        {
            AlphaAcid = alphaacid;
        }
    }
}
