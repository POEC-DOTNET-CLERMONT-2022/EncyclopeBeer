using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Models.Ingredients
{
    public class CerealModel : IngredientModel
    {
        private float _ebc;
        public float Ebc 
        {
            get { return _ebc; }
            set
            {
                if (_ebc != value)
                {
                    _ebc = value;
                    OnNotifyPropertyChanged();
                }
            }
        }
        public CerealModel(string name, string description, float ebc)
            :this(Guid.Empty, name, description, ebc)
        {
        }

        public CerealModel(Guid id, string name, string description, float ebc) : base(id, name, description)
        {
            Ebc = ebc;
        }

        /// <summary>
        /// Nécessaire???
        /// </summary>
        /// <param name="hop"></param>
        public CerealModel(CerealModel cereal) : base(cereal.Id, cereal.Name, cereal.Description)
        {
            Ebc = cereal.Ebc;
        }

        public override object Clone()
        {
            return new CerealModel(this);
            //return this.MemberwiseClone();
        }

    }
}
