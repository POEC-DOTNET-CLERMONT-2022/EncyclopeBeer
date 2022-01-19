using AutoMapper;
using Dtos;
using Entities;
/// <summary>
/// Attention on suppose un nommage identique dans les Dtos et les Entity (sa évite l'appel explicite au .ForMember)
/// </summary>
namespace Models
{
    public class DtoEntityProfile : Profile
    {
        //var map = new Mapper(BreweryProfile);
        /// <summary>
        /// Exemple explicite (nommage et type potentiellement différent)
        /// </summary>
        public DtoEntityProfile()
        {
            CreateMap<BeerDto, BeerEntity>().ReverseMap();
                //.ForMember(
                //dest => dest.Id,
                //opt => opt.MapFrom(src => src.Id)
                //)
                //.ForMember(
                //dest => dest.Name,
                //opt => opt.MapFrom(src => src.Name)
                //)
                //.ForMember(
                //dest => dest.Ibu,
                //opt => opt.MapFrom(src => src.Ibu)
                //)
                //.ForMember(
                //dest => dest.Degree,
                //opt => opt.MapFrom(src => src.Degree)
                //)
                //.ForMember(
                //dest => dest.Brewery,
                //opt => opt.MapFrom(src => src.Brewery)
                //)
                //.ForMember(
                //dest => dest.Style,
                //opt => opt.MapFrom(src => src.Style)
                //)
                //.ForMember(
                //dest => dest.Color,
                //opt => opt.MapFrom(src => src.Color)
                //)
                //.ForMember(
                //dest => dest.Ingredients,
                //opt => opt.MapFrom(src => src.Ingredients)
                //);

            /// Exemples implicites : 
            #region Types simples (Brassseur, Style, Couleur, Pays)
            CreateMap<BreweryDto, BreweryEntity>().ReverseMap();
            CreateMap<BeerColorDto, BeerColorEntity>().ReverseMap();
            CreateMap<BeerStyleDto, BeerStyleEntity>().ReverseMap();
            CreateMap<CountryDto, CountryEntity>().ReverseMap();
            #endregion

            #region Ingredients : abstract puis dérivées
            /// voir https://docs.automapper.org/en/stable/Lists-and-arrays.html#
            CreateMap<IngredientDto, IngredientEntity>().ReverseMap().IncludeAllDerived(); // IncludeAllDerived() pour inclure les types dérivées
            #region Dérivées
            CreateMap<HopDto, HopEntity>().ReverseMap();
            CreateMap<CerealDto, CerealEntity>().ReverseMap();
            CreateMap<AdditiveDto, AdditiveEntity>().ReverseMap();
            #endregion
            #endregion

        }
    }
}