using System.Diagnostics;
using CoreGraphics;
using Foundation;
using SWBSales.Controls;
using SWBSales.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WebViewEx), typeof(WebViewExRenderer))]

namespace SWBSales.iOS.Renderer
{
	/// <summary>
	/// Custom iOS renderer for the Xamarin.Forms.WebView
	/// </summary>
	/// <remarks>
	/// https://developer.xamarin.com/guides/xamarin-forms/user-interface/webview/
	/// https://developer.xamarin.com/guides/xamarin-forms/working-with/webview/
	/// https://developer.xamarin.com/recipes/cross-platform/xamarin-forms/controls/display-pdf/
	/// </remarks>
	public class WebViewExRenderer : WebViewRenderer
	{
		private WebViewEx FormsControl => Element as WebViewEx;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="eventArgs"></param>
		protected override void OnElementChanged(VisualElementChangedEventArgs eventArgs)
		{
			if (Element == null || NativeView == null)
				return;

			base.OnElementChanged(eventArgs);

			// Instantiate the native control and assign it to the Control property
			if (NativeView == null)
			{
			}

			// Unsubscribe from event handlers and cleanup any resources
			if (eventArgs.OldElement != null)
			{
				FormsControl.OnFinished -= OnFinished;
			}

			// Configure the control and subscribe to event handlers
			if (eventArgs.NewElement != null)
			{
				// Eigenschaften setzen
				SetProperties();

				// Events setzen
				FormsControl.OnFinished += OnFinished;
			}
		}

		private void OnFinished(object sender, System.EventArgs eventArgs)
		{
			SetProperties();
		}

		public override void LoadRequest(NSUrlRequest request)
		{
			// Basisimplementierung
			base.LoadRequest(new NSUrlRequest(NSUrl.FromFilename(request.Url.ToString())));

			// Eigenschaften setzen
			//SetProperties();
		}

		private void SetProperties()
		{
			FormsControl.IsVisible = false;

			var webView = (UIWebView)NativeView;
			webView.Frame = new CGRect(FormsControl.X, FormsControl.Y, FormsControl.Width, FormsControl.Height);
			Debug.WriteLine(FormsControl.Width + " - " + FormsControl.Height);

			// if this is false, page will be 'zoomed in' to normal size
			webView.ScalesPageToFit = FormsControl.ScalesToFit;
			webView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

			webView.ContentMode = UIViewContentMode.ScaleAspectFit;
			//webView.ScrollView.ZoomScale = webView.Bounds.Size.Width / webView.ScrollView.ContentSize.Width;

			webView.ScrollView.Frame = new CGRect(0, 0, FormsControl.Width, FormsControl.Height);
			webView.ScrollView.MaximumZoomScale = 20;
			webView.ScrollView.MinimumZoomScale = 1;

			webView.ScrollView.ZoomScale = 2;
			webView.ScrollView.ZoomScale = 1;

			// setting bouncing
			webView.ScrollView.Bounces = FormsControl.CanBounce;
			webView.ScrollView.BouncesZoom = FormsControl.CanBounce;

			// setting scrolling
			webView.ScrollView.ScrollEnabled = FormsControl.IsScrollable;

			// setting zooming
			if (FormsControl.CanZoom == false)
			{
				webView.ScrollView.MaximumZoomScale = 1;
				webView.ScrollView.MinimumZoomScale = 1;
				if (FormsControl.IsScrollable == false)
				{
					webView.UserInteractionEnabled = false;
					webView.MultipleTouchEnabled = false;
				}
			}

			FormsControl.IsVisible = true;
		}

		public override void LoadHtmlString(string htmlString, NSUrl baseUrl)
		{
			if (string.IsNullOrEmpty(baseUrl?.ToString()) == true)
				baseUrl = new NSUrl(NSBundle.MainBundle.BundlePath, true);

			// Basisimplementierung
			base.LoadHtmlString(htmlString, baseUrl);
		}
	}
}