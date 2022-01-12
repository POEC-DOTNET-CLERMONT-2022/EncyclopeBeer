using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Model
{
    public class Image
    {
        public Guid Id { get; }
        public string File { get; set; }

        public Image(string file)
        {
            Id = Guid.NewGuid();
            File = file;
        }
    }
}
