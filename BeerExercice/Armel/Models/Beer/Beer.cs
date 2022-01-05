using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiBeer.Models
{
    //public enum UpdateAble
    //{
    //    name = 1,
    //    ibu = 2,
    //    degree = 3,
    //    style = 4, 
    //    color = 5
    //}
    public class Beer
    {
        public Guid Id { get; private set; }
        public string Name { get; internal set; }
        public float Ibu { get; internal set; }
        public float Degree { get; internal set; }
 //       public string Flavour { get; private set; }
   //     public string Taste { get; private set; }
     //   public string Appearance { get; private set; }
       // public string Brewery { get; private set; }

        public List<Ingredient> Ingredients { get; internal set; }// = new List<Ingredient>();

        // private List<BeerStyle> BeerStyles = new List<BeerStyle>();
        public BeerStyle BeerStyle { get; internal set; }
        public BeerColor BeerColor { get; internal set; }

        public Beer(string name, float ibu, float degree, BeerColor color, BeerStyle style, List<Ingredient> ingredients)
        {
            Id = Guid.NewGuid();
            Name = name;
            Ibu = ibu;  
            Degree = degree;
            BeerColor = color;
            BeerStyle = style;
            Ingredients = ingredients;
            //Flavour = flavour;
            //Taste = taste;
            //Appearance = appearance;
            //Brewery = brewery;
        }

        //public void Update(int index)
        //{
        //    switch (index)
        //    {
        //        case 1:
        //    }
        //}

        public override string? ToString()
        {
            return $" Name: {Name} - Color: {BeerColor.GetString()} - Style {BeerStyle.GetString()} - IBU: {Ibu} - Degree: {Degree}%";
            //return $" - Nom: {Name}\n - IBU: {Ibu}\n - Degrés: {Degree}%\n - Arôme: {Flavour}\n - Gout: {Taste}\n - Apparence: {Appearance}\n - Brasserie : {Brewery}";
        }

        public void TransferId(Beer new_beer)
        {
            if (new_beer == null)
            {
                return; 
            }
            else
            {
                new_beer.Id = Id;
            }
        }

    }
}
