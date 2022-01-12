using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ipme.WikiBeer.Dtos.Ingredients
{
    [DataContract]
    internal class AdditiveDto : IngredientDto
    {
        [DataMember]
        public string Use { get; set; }
    }
}
