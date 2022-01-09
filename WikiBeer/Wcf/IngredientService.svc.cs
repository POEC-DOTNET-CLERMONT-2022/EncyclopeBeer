using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Persistance;
using Ipme.WikiBeer.Extension.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Ipme.WikiBeer.Wcf
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "IngredientService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez IngredientService.svc ou IngredientService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class IngredientService : IIngredientService
    {
        public IIngredientManager IngredientManager { get; }

        public IngredientService()
        {
            IngredientManager = new IngredientManager();
        }

        public IEnumerable<IngredientDto> GetIngredients()
        {
            return IngredientManager.GetAllIngredient().ToDto();
        }
    }
}
