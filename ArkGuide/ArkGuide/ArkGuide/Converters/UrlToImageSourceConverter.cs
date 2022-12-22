using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ArkGuide.Converters
{
    public class UrlToImageSourceConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value is string)
            {
                var uriString = (string)value;
                Uri uri = new Uri(uriString);
                if (uri.AbsolutePath.ToLowerInvariant().EndsWith(".svg"))
                {
                    return SvgImageSource.FromFile(uriString, 200, 200);
                }
                else
                {
                    return ImageSource.FromUri(uri);
                }
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
