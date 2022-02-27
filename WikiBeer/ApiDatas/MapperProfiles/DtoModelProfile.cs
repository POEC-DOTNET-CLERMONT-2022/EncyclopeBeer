using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;

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
            CreateMap<BreweryDto, BreweryModel>().ReverseMap();
            CreateMap<BeerColorDto, BeerColorModel>().ReverseMap();
            CreateMap<BeerStyleDto, BeerStyleModel>().ReverseMap();
            CreateMap<CountryDto, CountryModel>().ReverseMap();
            CreateMap<UserDto, UserModel>().ReverseMap();
            CreateMap<ImageDto, ImageModel>().ReverseMap();
            CreateMap<ConnectionInfosDto,ConnectionInfosModel>().ReverseMap();
            
            /// voir https://docs.automapper.org/en/stable/Lists-and-arrays.html#
            CreateMap<IngredientDto, IngredientModel>().IncludeAllDerived(); // IncludeAllDerived() pour inclure les types dérivées
            CreateMap<IngredientModel, IngredientDto>().IncludeAllDerived(); // IncludeAllDerived() pour inclure les types dérivées
            CreateMap<HopDto, HopModel>().ReverseMap();
            CreateMap<CerealDto, CerealModel>().ReverseMap();
            CreateMap<AdditiveDto, AdditiveModel>().ReverseMap();

        }
    }
}
