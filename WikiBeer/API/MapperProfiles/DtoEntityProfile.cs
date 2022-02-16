using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Entities.Ingredients;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Ipme.WikiBeer.API.Tests")]
namespace Ipme.WikiBeer.API.MapperProfiles
{
    internal class DtoEntityProfile : Profile
    {
        public DtoEntityProfile()
        {
            CreateMap<BeerDto, BeerEntity>().ReverseMap();

            // Implicites : 
            #region Types simples (Brassseur, Style, Couleur, Pays)
            CreateMap<BreweryDto, BreweryEntity>().ReverseMap();
            CreateMap<BeerColorDto, BeerColorEntity>().ReverseMap();
            CreateMap<BeerStyleDto, BeerStyleEntity>().ReverseMap();
            CreateMap<CountryDto, CountryEntity>().ReverseMap();
            // Explicites
            CreateMap<UserDto, UserEntity>()
                .ForMember(dest => dest.UserBeers,
                opt => opt.MapFrom(src => src.FavoriteBeerIds.Select(id => new UserBeer(src.Id, id) ) ));
            CreateMap<UserEntity, UserDto>()
                .ForMember(dest => dest.FavoriteBeerIds, 
                opt => opt.MapFrom(src => src.UserBeers.Select(ub => ub.BeerId)));
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
