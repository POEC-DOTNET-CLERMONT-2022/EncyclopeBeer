using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HopModel : IngredientModel
    {
        public float AlphaAcid { get; set; }
        public HopModel(string name, string description, float alphaAcid) : base(name, description)
        {
            AlphaAcid = alphaAcid;
        }
    }
}
