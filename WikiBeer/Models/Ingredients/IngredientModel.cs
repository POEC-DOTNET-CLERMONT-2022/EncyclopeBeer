using Ipme.WikiBeer.Models.Magic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Models.Ingredients
{
    public abstract class IngredientModel : ObservableObject, ICloneable
    {
        public Guid Id { get; }

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

        //public IngredientModel(IngredientModel ingredient)
        //{
        //    Id = ingredient.Id;
        //    Name = ingredient.Name;
        //    Description = ingredient.Description;
        //}

        ///// <summary>
        ///// this.MemberWiseClone() car on a que des type valeurs. A modifier en cas de type références ajoutées
        ///// </summary>
        ///// <returns></returns>
        //public virtual Object Clone()
        //{
        //    return this.MemberwiseClone();
        //}

        public abstract Object Clone();
    }
}
