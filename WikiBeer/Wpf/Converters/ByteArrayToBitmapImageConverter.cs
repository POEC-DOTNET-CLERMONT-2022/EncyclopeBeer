using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Ipme.WikiBeer.Wpf.Converters
{
    [ValueConversion(typeof(byte[]), typeof(BitmapImage))]
    public class ByteArrayToBitmapImageConverter : IValueConverter
    {

        /// <summary>
        /// Convert a bytes array into a Bitmap image
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] image)
            {
                if (image.Length > 0)
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new System.IO.MemoryStream(image);
                    bitmap.EndInit();
                    return bitmap;
                }
            }
            return new BitmapImage(new Uri($"../Images/Image5.jpg", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}