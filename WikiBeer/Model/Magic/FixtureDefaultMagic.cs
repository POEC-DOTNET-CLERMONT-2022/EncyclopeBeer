using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// All the magic constants used in AutoFixture calls (Model classes only)
/// </summary>
namespace Ipme.WikiBeer.Model.Magic
{
    internal static class FixtureDefaultMagic
    {
        // Ingredient
        internal const int DEFAULT_HOPS_NUMBER = 1;
        internal const int DEFAULT_CEREALS_NUMBER = 1;
        internal const int DEFAULT_ADDITIVES_NUMBER = 1;

        // Brewery
        internal const int DEFAULT_BEER_NUMBER_BY_BREWERY = 5;
        internal const int DEFAULT_BEERSTYLES_NUMBER = 10;
        internal const int DEFAULT_BEERCOLORS_NUMBER = 10;
    }
}
