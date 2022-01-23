using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class AdditiveDto : IngredientDto
    {
        public string Use { get; set; }

        //public AdditiveDto(string name, string description, string Use): base(name,description)
        //{
        //    Use = Use;
        //}
    }
}
