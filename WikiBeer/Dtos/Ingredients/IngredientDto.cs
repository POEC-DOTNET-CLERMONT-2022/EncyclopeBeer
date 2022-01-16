using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

/// <summary>
/// TODO : Faire des DTO spécifique aux classes dérivées de Ingredient
/// </summary>
namespace Ipme.WikiBeer.Dtos.Ingredients
{
    [DataContract]
    public class IngredientDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string TypeOfIngredient { get; set; } // Pour test d'affichage
    }
}
