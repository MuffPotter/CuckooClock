using System;
using Foundation;
using UIKit;

using Sigeko.CuckooClock.iOS.Services;
using Sigeko.CuckooClock.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(OrientationService))]

namespace Sigeko.CuckooClock.iOS.Services
{
	/// <summary>
	/// 
	/// </summary>
	public class OrientationService : IOrientation
	{
		public DeviceOrientation GetOrientation()
		{
			var currentOrientation = UIApplication.SharedApplication.StatusBarOrientation;

			switch (currentOrientation)
			{
				case UIInterfaceOrientation.Portrait:
				case UIInterfaceOrientation.PortraitUpsideDown:
					return DeviceOrientation.Portrait;
				case UIInterfaceOrientation.LandscapeLeft:
					return DeviceOrientation.LandscapeLeft;
				case UIInterfaceOrientation.LandscapeRight:
					return DeviceOrientation.LandscapeRight;
			}

			return DeviceOrientation.LandscapeLeft;
		}

		public void SetOrientation(DeviceOrientation orientation)
		{
			switch (orientation)
			{
				case DeviceOrientation.Auto:
					var currentOrientation = UIApplication.SharedApplication.StatusBarOrientation;
					SetOrientation(currentOrientation);
					break;
				case DeviceOrientation.Undefined:
					SetLandscape(DeviceOrientation.Landscape);
					break;
				case DeviceOrientation.Landscape:
				case DeviceOrientation.LandscapeLeft:
					SetLandscape(DeviceOrientation.LandscapeLeft);
					break;
				case DeviceOrientation.LandscapeRight:
					SetLandscape(DeviceOrientation.LandscapeRight);
					break;
				case DeviceOrientation.Portrait:
				case DeviceOrientation.PortraitUpsideDown:
					SetPortrait();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
			}
		}

		public void SetOrientation(UIInterfaceOrientation orientation)
		{
			switch (orientation)
			{
				case UIInterfaceOrientation.LandscapeLeft:
					SetLandscape(DeviceOrientation.LandscapeLeft);
					break;
				case UIInterfaceOrientation.LandscapeRight:
					SetLandscape(DeviceOrientation.LandscapeRight);
					break;
				case UIInterfaceOrientation.Portrait:
				case UIInterfaceOrientation.PortraitUpsideDown:
					SetPortrait();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
			}
		}

		public void SetLandscape(DeviceOrientation orientation)
		{
			((AppDelegate)UIApplication.SharedApplication.Delegate).CurrentOrientation = UIInterfaceOrientationMask.Landscape;


			var device = UIDevice.CurrentDevice;
			var newOrientation = UIInterfaceOrientation.LandscapeLeft;

			switch (orientation)
			{
				case DeviceOrientation.Landscape:
				case DeviceOrientation.LandscapeLeft:
					newOrientation = UIInterfaceOrientation.LandscapeLeft;
					break;
				case DeviceOrientation.LandscapeRight:
					newOrientation = UIInterfaceOrientation.LandscapeRight;
					break;
			}

			device.SetValueForKey(new NSNumber((int)newOrientation), (NSString)"orientation");
			UIApplication.SharedApplication.SetStatusBarOrientation(newOrientation, false);
		}

		public void SetPortrait()
		{
			((AppDelegate)UIApplication.SharedApplication.Delegate).CurrentOrientation = UIInterfaceOrientationMask.Portrait;

			var device = UIDevice.CurrentDevice;
			var orientation = new NSNumber((int)UIInterfaceOrientation.Portrait);
			device.SetValueForKey(orientation, (NSString)"orientation");

			UIApplication.SharedApplication.SetStatusBarOrientation(UIInterfaceOrientation.Portrait, false);
		}

		private void SetDefaultorientation()
		{
			// Programmatically triggers rotation of views.
			// If UIViewController.ShouldAutorotateToInterfaceOrientation returns false, 
			// this method can be used to programmatically trigger view rotation.

			var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
			rootViewController.ShouldAutorotate();

			UIViewController.AttemptRotationToDeviceOrientation();

			var device = UIDevice.CurrentDevice;
			NSNumber orientation = (int)UIInterfaceOrientation.LandscapeLeft;
			device.SetValueForKey(orientation, (NSString)"orientation");
		}
	}
}