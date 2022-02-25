using Ipme.WikiBeer.Entities.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities.AssociationTables
{
    public class BeerIngredient : IAssociationTable
    {
        public Guid BeerId { get; private set; }
        public BeerEntity Beer { get; private set; }

        public Guid IngredientId { get; private set; }
        public IngredientEntity Ingredient { get; private set; }

        public BeerIngredient(Guid beerId, Guid ingredientId)
        {
            BeerId = beerId;
            IngredientId = ingredientId;
        }

        public BeerIngredient(Guid beerId, Guid ingredientId, BeerEntity beer, IngredientEntity ingredient)
            : this(beerId, ingredientId)
        {
            Beer = beer;
            Ingredient = ingredient;
        }

        public bool IsInCompositeKey(Guid id)
        {
            return id == BeerId || id == IngredientId;
        }

        public (Guid, Guid) GetCompositeKey()
        {
            return (BeerId, IngredientId);
        }

        public bool Equals(IAssociationTable? other)
        {
            if (other is null || other is not BeerIngredient) return false;
            else return this.GetCompositeKey() == other.GetCompositeKey();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            var other = obj as IAssociationTable;
            if (other is null) return false;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return GetCompositeKey().GetHashCode();
        }
    }
}
