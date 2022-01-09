using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Ipme.WikiBeer.Wcf
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IIngredientService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IIngredientService
    {
        IIngredientManager IngredientManager { get; }

        [OperationContract]
        IEnumerable<IngredientDto> GetIngredients();
    }
}
