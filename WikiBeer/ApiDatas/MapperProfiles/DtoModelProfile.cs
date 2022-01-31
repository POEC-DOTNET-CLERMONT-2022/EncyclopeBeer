﻿using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Voir comment spécifier un constructeur particulier à AutoMapper via 
/// https://docs.automapper.org/en/stable/Construction.html
/// Pour choisir le constructeur à utiliser : 
/// https://stackoverflow.com/questions/2239143/automapper-how-to-map-to-constructor-parameters-instead-of-property-setters
///</summary>
namespace Ipme.WikiBeer.ApiDatas.MapperProfiles
{
    public class DtoModelProfile : Profile
    {
        public DtoModelProfile()
        {
            CreateMap<BeerDto, BeerModel>().ReverseMap();

            /// Exemples implicites : 
            #region Types simples (Brassseur, Style, Couleur, Pays)
            CreateMap<BreweryDto, BreweryModel>().ReverseMap();
            CreateMap<BeerColorDto, BeerColorModel>().ReverseMap();
            CreateMap<BeerStyleDto, BeerStyleModel>().ReverseMap();
            CreateMap<CountryDto, CountryModel>().ReverseMap();
            #endregion

            #region Ingredients : abstract puis dérivées
            /// voir https://docs.automapper.org/en/stable/Lists-and-arrays.html#
            CreateMap<IngredientDto, IngredientModel>().IncludeAllDerived(); // IncludeAllDerived() pour inclure les types dérivées
            CreateMap<IngredientModel, IngredientDto>().IncludeAllDerived(); // IncludeAllDerived() pour inclure les types dérivées
            #region Dérivées
            CreateMap<HopDto, HopModel>().ReverseMap();
            CreateMap<CerealDto, CerealModel>().ReverseMap();
            CreateMap<AdditiveDto, AdditiveModel>().ReverseMap();
            #endregion
            #endregion

        }
    }
}
