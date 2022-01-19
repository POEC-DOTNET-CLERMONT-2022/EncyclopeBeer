using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class CerealDto : IngredientDto
    {
        public float Ebc { get; set; }

        public CerealDto(string name, string description, float ebc) : base(name, description)
        {
            Ebc = Ebc;
        }
    }
}
