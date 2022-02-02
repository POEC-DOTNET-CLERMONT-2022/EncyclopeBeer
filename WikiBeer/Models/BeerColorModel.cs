using AutoFixture;
using Ipme.WikiBeer.Models.Magic;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Gros Doute sur l'interet de cette classe : je remplacerai bien par une énum simple
/// </summary>
namespace Ipme.WikiBeer.Models
{
    public class BeerColorModel : ObservableObject
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

        public BeerColorModel(string name) : this(Guid.Empty, name)
        {
        }

        public BeerColorModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public BeerColorModel(BeerColorModel color)
            : this(color.Id, color.Name)
        {
        }
    }
}
