using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ipme.WikiBeer.Extension.Factories
{
    public static class BeerFactory
    {
        public static IEnumerable<BeerDto> ToDto(this IEnumerable<Beer> beers)
        {
            foreach (var beer in beers)
            {
                yield return beer.ToDto();
            }
        }

        public static BeerDto ToDto(this Beer beer)
        {
            // ToList Nécessaire à cause de l'implémentation de Ingredients comme une liste!
            return new BeerDto {Id = beer.Id, Name = beer.Name , Degree = beer.Degree, Ibu = beer.Ibu, Ingredients = beer.Ingredients.ToDto().ToList()};  
        }

        //public static IEnumerable<Beer> ToModel(this IEnumerable<BeerDto> beers)
        //{
        //    foreach (var beer in beers)
        //    {
        //        yield return beer.ToModel();
        //    }
        //}

        //public static Beer ToModel(this BeerDto beer)
        //{
        //    return new Beer {Id = beer.Id, Name = beer.Name , Degree = beer.Degree, Ibu = beer.Ibu, Ingredients = beer.Ingredients};
        //}

    }

}