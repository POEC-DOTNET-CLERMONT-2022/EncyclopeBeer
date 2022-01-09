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
        internal const int DEFAULT_HOP_NUMBER = 1;
        internal const int DEFAULT_CEREAL_NUMBER = 1;
        internal const int DEFAULT_ADDITIVE_NUMBER = 1;

        // Brewery
        internal const int DEFAULT_BEER_NUMBER_BY_BREWERY = 5;
        internal const int DEFAULT_BEERSTYLE_NUMBER = 10;
        internal const int DEFAULT_BEERCOLOR_NUMBER = 10;
    }
}
