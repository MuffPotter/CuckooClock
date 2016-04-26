using System;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Controls
{
	/// <summary>
	/// Subclass of WebView for custom renderer
	/// </summary>
	public class WebViewEx : WebView
	{
		public WebViewEx()
		{
			this.Animate = false;

			this.HorizontalOptions = LayoutOptions.FillAndExpand;
			this.VerticalOptions = LayoutOptions.FillAndExpand;

			this.Navigated += OnNavigated;
			this.Navigating += OnNavigating; 
		}

		private void OnNavigating(object sender, WebNavigatingEventArgs e)
		{
			if (Animate == true)
			{
				this.Opacity = 0;
			}
		}

		private void OnNavigated(object sender, WebNavigatedEventArgs e)
		{
			//http://xfcomplete.net/animation/2016/01/18/compound-animations/
			if (Animate == true)
			{
				this.FadeTo(1, 500, Easing.Linear);
			}

			this.OnFinished?.Invoke(this, null);
		}

		public event EventHandler OnFinished;

		public bool IsScrollable { get; set; }

		public bool CanBounce { get; set; }

		public bool CanZoom { get; set; }

		public bool ScalesToFit { get; set; }

		private bool _animate;

		public bool Animate
		{
			get { return _animate; }
			set
			{
				this.Opacity = value == true ? 0 : 1;
				_animate = value;
			} 
		}
	}
}