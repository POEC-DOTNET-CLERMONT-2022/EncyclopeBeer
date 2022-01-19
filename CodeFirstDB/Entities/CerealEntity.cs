using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CerealEntity : IngredientEntity
    {
        public float Ebc { get; set; }

        public CerealEntity(string name, string description, float ebc): base(name, description)
        {
            Ebc = ebc;
        }
    }
}
