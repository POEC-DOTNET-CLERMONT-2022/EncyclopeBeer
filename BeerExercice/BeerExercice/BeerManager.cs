using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerExercice
{
    class BeerManager
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
            catch (Exception e) // a voir selon les possible exceptions dans create beer
            {

            }
        }
        public void Delete()
        {
            if (Convert.ToBoolean(Beers.Count())) // Convert.ToBoolean renvoie false si 0 ou null, true sinon
            {
                Writer.Display("Quelle bière voulez vous supprimer");
                ReadAll();
                try
                {
                    var index = Reader.ReadIntFromUser();
                    Beers.RemoveAt(index);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Writer.Display($"L'emplacement demandé n'existe pas.");
                    Writer.Display($"Voulez vous réessayer (o/n)");
                    var inpur = Reader. 
                }

            }
            else
            {
                Writer.Display("Aucunes bières en stock, retour au menu");
            }
        }

        public void Update()
        {

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
                for (int i =0; i<Beers.Count(); )
                {
                    Writer.Display($" {i} - {Beers[i]}");
                }

            }
            else
            {
                Writer.Display("Aucunes bières en stock!");
            }
        }   


    }
}
