using System;
using System.Globalization;
using Sigeko.CuckooClock.Common;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Converters
{
	public class ImageSourceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				if (value is string == false)
					return null;

				Logger.Current.LogInfo($"ImageSourceConverter: {(string)value}");
				var imageSource = ImageSource.FromResource((string)value);
				return imageSource;

				//string imageResourceName = "BestBlu.ErCode.Resources.Images.message_typ_{0}.png";
				//value = (int)value;
				//imageResourceName = string.Format(imageResourceName, value.ToString());
				//Stream stream = ResourceLoader.GetEmbeddedResourceStream(imageResourceName);
				//ImageSource imageSource = ImageSource.FromStream(() => stream);
				//return imageSource;
			}
			catch (Exception exception)
			{
				Logger.Current.LogException(exception);
				return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}