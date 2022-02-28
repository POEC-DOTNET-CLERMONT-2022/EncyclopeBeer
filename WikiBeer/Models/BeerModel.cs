using AutoFixture;
using Ipme.WikiBeer.Models.Ingredients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Pour de la deep copy 
/// voir : // voir : https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone?redirectedfrom=MSDN&view=net-6.0#System_Object_MemberwiseClone
/// TODO : adopter une seule pratique pour les copy : via constructeur ou via clonage 
/// voir : https://stackoverflow.com/questions/1573453/i-need-to-implement-c-sharp-deep-copy-constructors-with-inheritance-what-patter
/// </summary>
namespace Ipme.WikiBeer.Models
{
    public class BeerModel : ObservableObject, IDeepClonable<BeerModel>
    {
        public Guid Id { get; } // en readOnly car provient de la base

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

        private float _ibu;
        public float Ibu 
        {
            get { return _ibu; }
            set
            {
                if (_ibu != value)
                {
                    _ibu = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private float _degree;
        public float Degree 
        {
            get { return _degree; }
            set
            {
                if (_degree != value)
                {
                    _degree = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private BeerStyleModel? _style;
        public BeerStyleModel? Style
        {
            get { return _style; }
            set
            {
                if (_style != value)
                {
                    _style = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private BeerColorModel? _color;
        public BeerColorModel? Color 
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private BreweryModel? _brewery;
        public BreweryModel? Brewery 
        {
            get { return _brewery; }
            set
            {
                if (_brewery != value)
                {
                    _brewery = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<IngredientModel> _ingredients;
        public ObservableCollection<IngredientModel> Ingredients 
        {
            get { return _ingredients; }
            set
            {
                if (_ingredients != value)
                {
                    _ingredients = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private ImageModel? _image;
        public ImageModel? Image
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnNotifyPropertyChanged();
                }
            }
        }
        //public BeerModel()
        //{
        //    Ingredients = new ObservableCollection<IngredientModel>();
        //}

        public BeerModel(string name = "", string description = "", float ibu = float.NaN, float degree = float.NaN,
            BeerStyleModel? style = null, BeerColorModel? color = null, BreweryModel? brewery = null, ImageModel? image = null,
            ObservableCollection<IngredientModel>? ingredients = null)
            : this(Guid.Empty, name, description, ibu, degree, style, color, brewery, image, ingredients)
        {
        }

        public BeerModel(Guid id, string name, string description, float ibu, float degree, 
            BeerStyleModel? style, BeerColorModel? color, BreweryModel? brewery, ImageModel? image, 
            ObservableCollection<IngredientModel>? ingredients)
        {
            Id = id;
            Name = name ?? String.Empty;
            Description = description ?? String.Empty;
            Ibu = ibu;
            Degree = degree;
            Style = style;
            Color = color;
            Brewery = brewery;
            Image = image;
            Ingredients = ingredients ?? new ObservableCollection<IngredientModel>();
        }

        private BeerModel(BeerModel beer)
        {
            //if (beer is null) throw new ArgumentNullException(nameof(beer), "Impossible de copier une instance null");
            // shallow copy mais sur des types valeurs (ou presque) donc OK
            Id = beer.Id;
            Name = beer.Name ?? String.Empty;
            Description = beer.Description ?? String.Empty;
            Ibu = beer.Ibu;
            Degree = beer.Degree;
            
            // Deep copy pour éviter les effets de bords
            Style = beer.Style?.DeepClone();
            Color = beer.Color?.DeepClone();
            Brewery = beer.Brewery?.DeepClone();
            Image = beer.Image?.DeepClone();
            Ingredients = new ObservableCollection<IngredientModel>();
            if (beer.Ingredients != null)
            {
                foreach (var ingredient in beer.Ingredients)
                {
                    if (ingredient is not null) Ingredients.Add(ingredient.DeepClone()); 
                }
            }
        } 

        public BeerModel DeepClone()
        {
            return new BeerModel(this);
        }
    }
}