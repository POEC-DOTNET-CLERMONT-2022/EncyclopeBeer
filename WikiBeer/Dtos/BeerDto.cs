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
        //[DataMember]
        //public List<Ingredient> Ingredients { get; set; } // Fonctionne jusqu'au wpf mais pose des problèmes au WCF Client Test
        //[DataMember]
        //public List<string> Ingredients { get; set; } // Fonctionne parfaitement
        [DataMember]
        public List<IngredientDto> Ingredients { get; set; } // Tentative de résolution du bug
    }
}
