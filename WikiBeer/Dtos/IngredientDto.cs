using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ipme.WikiBeer.Dtos
{
    [DataContract]
    public class IngredientDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
