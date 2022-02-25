using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Models
{
    public class ImageModel : IDeepClonable<ImageModel>
    {
        public byte[] ByteImage { get; private set; }

        public ImageModel(byte[] byteImage)
        {
            ByteImage = byteImage;
        }

        public ImageModel(ImageModel image)
        {
            ByteImage = image.ByteImage ?? new byte[]{};
        }

        public ImageModel DeepClone()
        {
            return new ImageModel(this);
        }
    }
}
