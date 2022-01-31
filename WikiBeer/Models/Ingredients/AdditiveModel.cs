using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Models.Ingredients
{
    public class AdditiveModel : IngredientModel
    {
        private string _use;
        public string Use
        {
            get { return _use; }
            set
            {
                if (_use != value)
                {
                    _use = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        public AdditiveModel(string name, string description, string use)
            : this(Guid.Empty, name, description, use)
        {
        }

        public AdditiveModel(Guid id, string name, string description, string use) : base(id, name, description)
        {
            Use = use;
        }

        public AdditiveModel(AdditiveModel additive) 
            : this(additive.Id, additive.Name, additive.Description, additive.Use)
        {
        }

        public override object Clone()
        {
            return new AdditiveModel(this);
        }
    }
}
