using Ipme.WikiBeer.Models.Magic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Models.Ingredients
{
    public abstract class IngredientModel : ObservableObject, IDeepClonable<IngredientModel>
    {
        public Guid Id { get; init; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnNotifyPropertyChanged();
                }
            }
        }


        public IngredientModel(string name, string desription) : this(Guid.Empty, name, desription)
        {
        }

        public IngredientModel(Guid id, string name, string desription)
        {
            Id = id;
            Name = name;
            Description = desription;
        }

        public IngredientModel(IngredientModel ingredient)
            : this(ingredient.Id, ingredient.Name, ingredient.Description)
        {
        }

        public abstract IngredientModel? DeepClone();
    }
}
