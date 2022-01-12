using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ipme.WikiBeer.Dtos.Ingredients
{
    [DataContract]
    internal class CerealDto : IngredientDto
    {
        [DataMember]
        public float EBC { get; set; }
    }
}
