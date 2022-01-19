using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class HopDto : IngredientDto
    {
        public float AlphaAcid { get; set; }
        public HopDto(string name, string description, float alphaAcid) : base(name, description)
        {
            AlphaAcid = alphaAcid;
        }
    }    
}
