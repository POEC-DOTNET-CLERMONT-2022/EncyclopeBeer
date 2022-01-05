using Ipme.WikiBeer.Model.Ingredients;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ipme.WikiBeer.Dtos
{
    [DataContract]
    public class BeerDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public float Ibu { get; set; }
        [DataMember]
        public float Degree { get; set; }
        [DataMember] 
        public List<Ingredient> Ingredients { get; set; }
    }
}
