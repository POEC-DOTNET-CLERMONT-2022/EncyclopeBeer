using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Models.Ingredients
{
    public class HopModel : IngredientModel
    {
        private float _alphaAcid;
        public float AlphaAcid 
        {
            get { return _alphaAcid; }
            set
            {
                if (_alphaAcid != value)
                {
                    _alphaAcid = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        public HopModel(string name, string description, float alphaAcid)
            : this(Guid.Empty, name, description, alphaAcid)
        {
        }
        
        public HopModel(Guid id, string name, string description, float alphaAcid) : base(id, name, description)
        {
            AlphaAcid = alphaAcid;
        }

        public HopModel(HopModel hop)             
            : this(hop.Id, hop.Name, hop.Description, hop.AlphaAcid)
        {
        }

        public override object Clone()
        {            
            return new HopModel(this);
        }

    }
}
