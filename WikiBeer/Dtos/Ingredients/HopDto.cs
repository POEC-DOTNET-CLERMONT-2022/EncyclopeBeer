using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Dtos.Ingredients
{
    internal class HopDto
    {
        public float AlphaAcid { get; internal set; }

        public HopDto(float alpha_acid, string ttttt)
        {
            AlphaAcid = alpha_acid;
        }
    }
}
