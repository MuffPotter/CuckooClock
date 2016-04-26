using System;
using Sigeko.CuckooClock.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sigeko.CuckooClock.Extensions
{
	/// <summary>
	/// http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/images/
	/// </summary>
	[ContentProperty("Source")]
	public class ImageResourceExtension : IMarkupExtension
	{
		public string Source { get; set; }

		public Binding BindingPara { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (Source == null)
				return null;

			try
			{
				// Do your translation lookup here, using whatever method you require
				ImageSource imageSource = ImageSource.FromResource(Source);
				return imageSource;
			}
			catch (Exception exception)
			{
				Logger.Current.LogException(exception);
				return null;
			}
		}
	}
}