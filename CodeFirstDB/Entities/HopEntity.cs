using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{ 
    public class HopEntity : IngredientEntity
    {
        public float AlphaAcid { get; set; }

        public HopEntity(string name, string description, float alphaAcid) : base(name, description)
        {
            AlphaAcid = alphaAcid;
        }
    }
}
