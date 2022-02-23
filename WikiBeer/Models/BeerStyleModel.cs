using AutoFixture;
using Ipme.WikiBeer.Models.Magic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Models
{
    public class BeerStyleModel : ObservableObject, IDeepClonable<BeerStyleModel>
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

        public BeerStyleModel(string name, string description) : this(Guid.Empty, name, description)
        {
        }

        public BeerStyleModel(Guid id, string name, string description)
        {
            Id = id;
            Name = name ?? String.Empty;
            Description = description ?? String.Empty;
        }

        private BeerStyleModel(BeerStyleModel style)
            : this(style.Id, style.Name, style.Description)
        {
        }

        public BeerStyleModel DeepClone()
        {            
            return new BeerStyleModel(this);
        }
    }
}
