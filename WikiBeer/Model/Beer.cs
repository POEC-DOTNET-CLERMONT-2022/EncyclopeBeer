using System;

namespace Ipme.WikiBeer.Model
{
    public class Beer
    {
        public Guid Id { get; private set; }
        public string Name { get; internal set; }
        public float Ibu { get; internal set; }
        public float Degree { get; internal set; }


        public Beer(string name, float ibu, float degree)
        {
            Id = Guid.NewGuid();
            Name = name;
            Ibu = ibu;
            Degree = degree;
        }

        public override string ToString()
        {
            return $" Name: {Name} - IBU: {Ibu} - Degree: {Degree}%";
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