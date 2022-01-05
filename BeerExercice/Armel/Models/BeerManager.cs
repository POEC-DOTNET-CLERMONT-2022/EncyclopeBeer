using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiBeer.Models
{
    public class BeerManager
    {
        private IWriter Writer { get; }
        private IReader Reader { get; }

        private IList<Beer> Beers { get; set; }

        public BeerManager(IReader reader, IWriter writer)
        {
            Beers = new List<Beer>();
            Writer = writer;
            Reader = reader;
            Reader.Writer = writer;
        }

        public void Create()
        {
            try
            {
                Beers.Add(Reader.ReadBeer());
            }
            catch (Exception ex) 
            {
                // a voir selon les possible exceptions dans ReadBeer
            }
        }
        public void Delete()
        {
            if (Convert.ToBoolean(Beers.Count())) // Convert.ToBoolean renvoie false si 0 ou null, true sinon
            {
                Writer.Display("Which beer to delete?");
                ReadAll();
                try
                {
                    var index = Reader.ReadIntFromUser();
                    Beers.RemoveAt(index);
                    // On pourrait rajouter une validation utilisateur ici...
                }
                catch (ArgumentOutOfRangeException e) // exception levé si l'index n'existe pas
                {
                    Writer.Display($"This beer does not exist");
                    Writer.Display($"Back to main menu.");
                }

            }
            else
            {
                Writer.Display("No beer in stock, back to main menu");
            }
        }

        public void Update()
        {
            if (Convert.ToBoolean(Beers.Count())) // Convert.ToBoolean renvoie false si 0 ou null, true sinon
            {
                Writer.Display("Which beer to update?");
                ReadAll();
                try
                {
                    var index = Reader.ReadIntFromUser();
                    var beer_to_change = Beers[index]; // pour déclencher une erreur si mauvaise valeur

                    Writer.Display("Reenter the beer");
                    var new_beer = Reader.ReadBeer();

                    beer_to_change.TransferId(new_beer);
                    Beers[index] = new_beer;
                }
                catch (ArgumentOutOfRangeException e) // exception levé si l'index n'existe pas
                {
                    Writer.Display($"This beer does not exist");
                    Writer.Display($"Back to main menu.");
                }



            }
            else
            {
                Writer.Display("No beer in stock, back to main menu");
            }
        }

        /// <summary>
        /// Appel DisplayBeer sur Beer[index]
        /// </summary>
        /// <param name="index"></param>
        public void ReadOne(int index)
        {
             Writer.DisplayBeer(Beers[index]);
        }

        public void ReadAll()
        {
            if (Convert.ToBoolean(Beers.Count())) // Convert.ToBoolean renvoie false si 0 ou null, true sinon
            {
                for (int i =0; i<Beers.Count(); i++)
                {
                    Writer.Display($"Beer number {i} - {Beers[i]}");
                }

            }
            else
            {
                Writer.Display("No beer in stock, back to main menu");
            }
        }   


    }
}
