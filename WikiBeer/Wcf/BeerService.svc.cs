using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Persistance;
using Ipme.WikiBeer.Extension.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Ipme.WikiBeer.Wcf
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class BeerService : IBeerService
    {
        //public IBeerManager BeerManager { get; private set; } = new BeerManager();
        //public IBeerManager BeerManager => new BeerManager();
        public IBeerManager BeerManager { get; }
        public IIngredientManager IngredientManager { get; }

        public BeerService()
        {
            BeerManager = new BeerManager();
            IngredientManager = new IngredientManager();
        }

        public IEnumerable<BeerDto> GetBeers()
        {
            //var beers = new List<BeerDto>() { new BeerDto { Id = Guid.NewGuid(), Name = "my_beer", Ibu = (float)0.5, Degree = 5 } };
            //foreach (var beer in beers)
            //{
            //    yield return beer;
            //}
            return BeerManager.GetAllBeer().ToDto();
        }

        public IEnumerable<IngredientDto> GetIngredients()
        {
            return IngredientManager.GetAllIngredient().ToDto();
        }
    }
}
