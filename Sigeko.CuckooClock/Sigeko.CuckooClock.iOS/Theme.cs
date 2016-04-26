using System;
using Sigeko.CuckooClock.Assets;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Sigeko.CuckooClock.iOS
{
	public class Theme
	{
		/// <summary>
		/// Apply this theme to everything that it can.
		/// </summary>
		public static void Apply(string options = null)
		{
			Apply(UIView.Appearance, options);
			Apply(UINavigationBar.Appearance, options);
			//Apply(UITabBar.Appearance, options);
			Apply(UIToolbar.Appearance, options);
			Apply(UIBarButtonItem.Appearance, options);
			//Apply(UISlider.Appearance, options);
			//Apply(UISegmentedControl.Appearance, options);
			//Apply(UIProgressView.Appearance, options);
			//Apply(UISearchBar.Appearance, options);
			Apply(UISwitch.Appearance, options);
			//Apply(UIRefreshControl.Appearance, options);
		}

		#region UIView

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		/// <param name="view">The view</param>
		/// <param name="options">Additional options</param>
		public static void Apply(UIView view, string options = null)
		{
			view.BackgroundColor = ColorResources.ViewBackgroundColor.ToUIColor();
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		/// <param name="appearance">The appearence of the view</param>
		/// <param name='options'>Can be "gunmetal", "mesh", or null</param>
		public static void Apply(UIView.UIViewAppearance appearance, string options = null)
		{
			//appearance.BackgroundColor = ColorResources.ViewBackgroundColor.ToUIColor();
		}

		#endregion UIView

		#region OK: UINavigationBar

		/// <summary>
		/// Custum navigation bar background
		/// </summary>
		private static readonly Lazy<UIImage> NavigationBarBackground = new Lazy<UIImage>(() => UIImage.FromBundle("TopNavBar.png"));

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		/// <param name="view">The view</param>
		/// <param name="options">Additional options</param>
		public static void Apply(UINavigationBar view, string options = null)
		{
			view.SetBackgroundImage(NavigationBarBackground.Value, UIBarMetrics.Default);
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		/// <param name="appearance">The appearence of the view</param>
		/// <param name='options'>Can be "gunmetal", "mesh", or null</param>
		public static void Apply(UINavigationBar.UINavigationBarAppearance appearance, string options = null)
		{
			// show the status bar
			UIApplication.SharedApplication.StatusBarHidden = false;
			UIApplication.SharedApplication.SetStatusBarHidden(false, true);

			appearance.SetBackgroundImage(NavigationBarBackground.Value, UIBarMetrics.Default);
			appearance.SetTitleTextAttributes(new UITextAttributes
			{
				TextColor = ColorResources.NavigationBarTextColor.ToUIColor(),
				Font = UIFont.FromName(FontResources.MainFontBold, 18),
				//TextShadowColor = UIColor.FromRGBA(0f, 0f, 0f, 0.8f),
				//TextShadowOffset = new UIOffset(0f, -1f)
			});
		}

		#endregion

		#region UITabBar

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		/// <param name="view">The view</param>
		/// <param name="options">Additional options</param>
		public static void Apply(UITabBar view, string options = null)
		{
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		/// <param name="appearance">The appearence of the view</param>
		/// <param name='options'>Can be "gunmetal", "mesh", or null</param>
		public static void Apply(UITabBar.UITabBarAppearance appearance, string options = null)
		{
		}

		#endregion

		#region UIToolbar

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		/// <param name="view">The view</param>
		/// <param name="options">"blue", or null</param>
		public static void Apply(UIToolbar view, string options = null)
		{
			view.SetBackgroundImage(NavigationBarBackground.Value, UIToolbarPosition.Any, UIBarMetrics.Default);
		}

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		/// <param name="appearance">The appearence of the view</param>
		/// <param name="options">"blue", or null</param>
		public static void Apply(UIToolbar.UIToolbarAppearance appearance, string options = null)
		{
			appearance.BackgroundColor = UIColor.Green;
			appearance.BarTintColor = ColorResources.ToolbarBackgroundColor.ToUIColor();
			appearance.TintColor = ColorResources.ToolbarTextColor.ToUIColor();
		}

		#endregion

		#region UIBarButtonItem

		private static readonly Lazy<UIImage> BarButtonBackground = new Lazy<UIImage>(
			() => UIImage.FromBundle("TopNavBar.png").CreateResizableImage(new UIEdgeInsets(0f, 4f, 0f, 4f)));

		private static readonly Lazy<UIImage> BackButtonBackBackground = new Lazy<UIImage>(
			() => UIImage.FromBundle("Icons/Delete22.png").CreateResizableImage(new UIEdgeInsets(0f, 14f, 0f, 4f)));

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		/// <param name="view">The view</param>
		/// <param name="options">"blue", or null</param>
		public static void Apply(UIBarButtonItem view, string options = null)
		{
			//view.SetBackgroundImage(barButtonBackground.Value, UIControlState.Normal, UIBarMetrics.Default);
			//view.SetBackButtonBackgroundImage(backButtonBackBackground.Value, UIControlState.Normal, UIBarMetrics.Default);
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		/// <param name="appearance">The appearence of the view</param>
		/// <param name="options">"blue", or null</param>
		public static void Apply(UIBarButtonItem.UIBarButtonItemAppearance appearance, string options = null)
		{
			//appearance.SetBackgroundImage(BarButtonBackground.Value, UIControlState.Normal, UIBarMetrics.Default);
			//appearance.SetBackButtonBackgroundImage(BackButtonBackBackground.Value, UIControlState.Normal, UIBarMetrics.Default);
		}

		#endregion

		#region OK: UIButton

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		public static void Apply(UIButton view, string options = null)
		{
			// Implematation in app.xaml
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		public static void Apply(UIButton.UIButtonAppearance appearance, string options = null)
		{
			// Implematation in app.xaml
		}

		#endregion

		#region UISlider

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		public static void Apply(UISlider view, string options = null)
		{
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		public static void Apply(UISlider.UISliderAppearance appearance, string options = null)
		{
		}

		#endregion

		#region OK: UILabel

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		public static void Apply(UILabel view, string options = null)
		{
			// Implematation in app.xaml
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		public static void Apply(UILabel.UILabelAppearance appearance, string options = null)
		{
			// Implematation in app.xaml
		}

		#endregion

		#region UITextField

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		public static void Apply(UITextField view, string options = null)
		{
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		public static void Apply(UITextField.UITextFieldAppearance appearance, string options = null)
		{
		}

		#endregion

		#region UISegmentedControl

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		public static void Apply(UISegmentedControl view, string options = null)
		{
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		public static void Apply(UISegmentedControl.UISegmentedControlAppearance appearance, string options = null)
		{
		}

		#endregion

		#region UIProgressView

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		public static void Apply(UIProgressView view, string options = null)
		{
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		public static void Apply(UIProgressView.UIProgressViewAppearance appearance, string options = null)
		{
		}

		#endregion

		#region UISearchBar

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		public static void Apply(UISearchBar view, string options = null)
		{
			view.TintColor = UIColor.DarkGray;
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		public static void Apply(UISearchBar.UISearchBarAppearance appearance, string options = null)
		{
			appearance.TintColor = UIColor.DarkGray;
		}

		#endregion

		#region OK: UISwitch

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		public static void Apply(UISwitch view, string options = null)
		{
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		public static void Apply(UISwitch.UISwitchAppearance appearance, string options = null)
		{
			// On color and thumb color
			appearance.OnTintColor = ColorResources.SwitchOnColor.ToUIColor();
			appearance.ThumbTintColor = ColorResources.SwitchColor.ToUIColor();

			// Rahmen um den Switch !?
			appearance.TintColor = ColorResources.SwitchTintColor.ToUIColor();

			// Scale
		}

		#endregion

		#region UIRefreshControl

		/// <summary>
		/// Apply this theme to a specific view.
		/// </summary>
		public static void Apply(UIRefreshControl view, string options = null)
		{
		}

		/// <summary>
		/// Apply this theme to all views with the given appearance.
		/// </summary>
		public static void Apply(UIRefreshControl.UIRefreshControlAppearance appearance, string options = null)
		{
		}

		#endregion
	}
}