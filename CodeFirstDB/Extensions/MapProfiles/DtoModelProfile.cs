using AutoMapper;
using Dtos;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.MapProfiles
{
    public class DtoModelProfile : Profile
    {
        //var map = new Mapper(BreweryProfile);
        /// <summary>
        /// Exemple explicite (nommage et type potentiellement différent)
        /// </summary>
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

            #region Dérivées
            CreateMap<IngredientDto, IngredientModel>().IncludeAllDerived(); // IncludeAllDerived() pour inclure les types dérivées
            CreateMap<IngredientModel, IngredientDto>().IncludeAllDerived(); // IncludeAllDerived() pour inclure les types dérivées
            CreateMap<HopDto, HopModel>().ReverseMap();

            //CreateMap<CerealDto, CerealModel>().ReverseMap();
            //CreateMap<AdditiveDto, AdditiveModel>().ReverseMap();
            #endregion
            #endregion

        }
    }
}
