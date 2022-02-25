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

        private string? _description;
        public string? Description
        {
            get { return _description; }
            set
            {
                if (_description != null)
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

        private byte[]? _rawPicture;
        public byte[]? RawPicture
        {
            get { return _rawPicture; }
            set
            {
                if (_rawPicture != value)
                {
                    _rawPicture = value;
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

        private ObservableCollection<IngredientModel>? _ingredients;
        public ObservableCollection<IngredientModel>? Ingredients 
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

        public BeerModel()
        {
        }

        public BeerModel(string name, string? description, float ibu, float degree, BeerStyleModel? style,
            BeerColorModel? color, BreweryModel? brewery, ObservableCollection<IngredientModel>? ingredients)
            : this(Guid.Empty, name, description, ibu, degree, style, color, brewery, ingredients)
        {
        }

        /// <summary>
        /// Constructeur qui permet de set un Id (pour Get depuis un Dto)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="ibu"></param>
        /// <param name="degree"></param>
        /// <param name="style"></param>
        /// <param name="color"></param>
        /// <param name="brewery"></param>
        /// <param name="ingredients"></param>
        public BeerModel(Guid id, string name, string? description, float ibu, float degree, BeerStyleModel? style, 
            BeerColorModel? color, BreweryModel? brewery, ObservableCollection<IngredientModel>? ingredients)
        {
            Id = id;
            Name = name;
            Description = description;
            Ibu = ibu;
            Degree = degree;
            Style = style;
            Color = color;
            Brewery = brewery;
            Ingredients = ingredients;
        }

        /// <summary>
        /// Copy constructor (pour BeerList du Wpf -> a revoir en profondeur ce truc...)
        /// </summary>
        /// <param name="beer"></param>
        public BeerModel(BeerModel beer)
        {
            // shallow copy mais sur des types valeurs (ou presque) donc OK
            Id = beer.Id;
            Name = beer.Name; // attention string est aussi un type référence... mais changer string
                              // revient à en créer une nouvelle donc peut etre traité ici comme un type value
            Ibu = beer.Ibu;
            Degree = beer.Degree;

            // Deep copy pour éviter les effets de bords
            Style = (BeerStyleModel?)beer.Style;
            Color = (BeerColorModel?)beer.Color;
            Brewery = (BreweryModel?)beer.Brewery;
            Ingredients = new ObservableCollection<IngredientModel>();
            if (beer.Ingredients != null)
            {
                foreach (var ingredient in beer.Ingredients)
                {
                    Ingredients.Add((IngredientModel)ingredient.DeepClone()); // Test via Clone
                }
            }
        }

        public BeerModel DeepClone()
        {
            return new BeerModel(this);
        }
    }
}