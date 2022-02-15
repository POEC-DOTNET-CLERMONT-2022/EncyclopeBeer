﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Dtos
{
    public class UserDto : IDto
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int HashCode { get; set; } 
        public bool IsCertified { get; set; }
        public CountryDto? Country { get; set; }
        public IEnumerable<BeerDto>? FavoriteBeers { get; set; }
    }
}
