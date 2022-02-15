
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Persistance.Contexts.Magics
{
    internal static class Rules
    {
        // Communs
        internal const int DEFAULT_NAME_MAX_LENGHT = 30;
        internal const int DEFAULT_DESCRIPTION_MAX_LENGTH = 400;

        // Ingredients
        internal const int INGREDIENT_TYPE_MAX_LENGTH = 20;

        // User
        internal const int DEFAULT_NICKNAME_MAX_LENGTH = 50;
        internal const int DEFAULT_MAIL_MAX_LENGTH = 100;
    }
}
