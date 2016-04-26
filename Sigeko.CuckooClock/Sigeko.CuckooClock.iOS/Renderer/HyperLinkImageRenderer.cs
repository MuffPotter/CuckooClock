using Foundation;
using SWBSales.Controls;
using SWBSales.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HyperLinkImage), typeof(HyperLinkImageRenderer))]

namespace SWBSales.iOS.Renderer
{
	/// <summary>
	/// Class HyperLinkImageRenderer.
	/// </summary>
	public class HyperLinkImageRenderer : ImageRenderer
	{
		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="eventArgs">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Image> eventArgs)
		{
			base.OnElementChanged(eventArgs);

			if (eventArgs.OldElement != null)
				return;

			var image = Control;
			image.UserInteractionEnabled = true;

			var tapXamarin = new UITapGestureRecognizer();
			tapXamarin.AddTarget(() =>
			{
				var hyperLinkImage = Element as HyperLinkImage;
				if (hyperLinkImage?.NavigateUri != null)
				{
					var uri = new NSUrl(GetNavigationUri(hyperLinkImage.NavigateUri));
					UIApplication.SharedApplication.OpenUrl(uri);
				}
			});
			tapXamarin.NumberOfTapsRequired = 1;
			tapXamarin.DelaysTouchesBegan = true;

			image.AddGestureRecognizer(tapXamarin);
		}

		/// <summary>
		/// Gets the navigation URI.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns>System.String.</returns>
		private static string GetNavigationUri(string uri)
		{
			if (uri.Contains("@") && !uri.StartsWith("mailto:"))
			{
				return $"mailto:{uri}";
			}
			if (uri.StartsWith("www.")) 
			{
				return $"http://{uri}";
			}
			return uri;
		}
	}
}