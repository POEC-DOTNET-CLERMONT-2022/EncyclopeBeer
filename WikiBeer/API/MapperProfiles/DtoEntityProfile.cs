using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Entities.Ingredients;

namespace Ipme.WikiBeer.API.MapperProfiles
{
    public class DtoEntityProfile : Profile
    {
        public DtoEntityProfile()
        {
            CreateMap<BeerDto, BeerEntity>().ReverseMap();

            /// Exemples implicites : 
            #region Types simples (Brassseur, Style, Couleur, Pays)
            CreateMap<BreweryDto, BreweryEntity>().ReverseMap();
            CreateMap<BeerColorDto, BeerColorEntity>().ReverseMap();
            CreateMap<BeerStyleDto, BeerStyleEntity>().ReverseMap();
            CreateMap<CountryDto, CountryEntity>().ReverseMap();
            #endregion

            #region Ingredients : abstract puis dérivées
            /// voir https://docs.automapper.org/en/stable/Lists-and-arrays.html#
            CreateMap<IngredientDto, IngredientEntity>().IncludeAllDerived();//.ReverseMap(); // IncludeAllDerived() pour inclure les types dérivées
            CreateMap<IngredientEntity, IngredientDto>().IncludeAllDerived();
            #region Dérivées
            CreateMap<HopDto, HopEntity>().ReverseMap();
            CreateMap<CerealDto, CerealEntity>().ReverseMap();
            CreateMap<AdditiveDto, AdditiveEntity>().ReverseMap();
            #endregion
            #endregion

        }
    }
}
