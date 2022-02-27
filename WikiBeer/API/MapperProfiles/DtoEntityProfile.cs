using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Entities.AssociationTables;
using Ipme.WikiBeer.Entities.Ingredients;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Ipme.WikiBeer.API.Tests")]
namespace Ipme.WikiBeer.API.MapperProfiles
{
    internal class DtoEntityProfile : Profile
    {
        public DtoEntityProfile()
        {
            //CreateMap<BeerDto, BeerEntity>().ReverseMap();
            CreateMap<BeerDto, BeerEntity>()
                .ForMember(dest => dest.BeerIngredients,
                opt => opt.MapFrom(src => src.Ingredients.Select(i => new BeerIngredient(src.Id, i.Id))));
            CreateMap<BeerEntity, BeerDto>();

            // Implicites : 
            CreateMap<BreweryDto, BreweryEntity>().ReverseMap();
            CreateMap<BeerColorDto, BeerColorEntity>().ReverseMap();
            CreateMap<BeerStyleDto, BeerStyleEntity>().ReverseMap();
            CreateMap<CountryDto, CountryEntity>().ReverseMap();
            // Explicites
            CreateMap<UserDto, UserEntity>()
                .ForMember(dest => dest.UserBeers,
                opt => opt.MapFrom(src => src.FavoriteBeerIds.Select(id => new UserBeer(src.Id, id))));
            CreateMap<UserEntity, UserDto>()
                .ForMember(dest => dest.FavoriteBeerIds,
                opt => opt.MapFrom(src => src.UserBeers.Select(ub => ub.BeerId)));
            CreateMap<ConnectionInfosEntity, ConnectionInfosDto>().ReverseMap();
            CreateMap<ImageDto, ImageEntity>().ReverseMap();

                      
            /// voir https://docs.automapper.org/en/stable/Lists-and-arrays.html#
            CreateMap<IngredientDto, IngredientEntity>().IncludeAllDerived();//.ReverseMap(); // IncludeAllDerived() pour inclure les types dérivées
            CreateMap<IngredientEntity, IngredientDto>().IncludeAllDerived();
            


            CreateMap<HopDto, HopEntity>().ReverseMap();
            CreateMap<CerealDto, CerealEntity>().ReverseMap();
            CreateMap<AdditiveDto, AdditiveEntity>().ReverseMap();
        }
    }
}
