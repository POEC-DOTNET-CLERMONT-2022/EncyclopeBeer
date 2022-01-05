using System;
using System.Runtime.Serialization;

namespace Ipme.WikiBeer.Dtos
{
    [DataContract]
    public class BeerDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public float Ibu { get; set; }
        [DataMember]
        public float Degree { get; set; }
    }
}
