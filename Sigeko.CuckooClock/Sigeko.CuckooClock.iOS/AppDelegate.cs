using System;
using System.Diagnostics;
using BluetoothLE.Core;
using BluetoothLE.iOS;
using Foundation;
using MetroLog;
using MetroLog.Targets;
using Sigeko.AppFramework.Services;
using Sigeko.CuckooClock.Common;
using Sigeko.CuckooClock.iOS.Services;
using UIKit;

using Sigeko.CuckooClock.Models;
using Sigeko.CuckooClock.Services;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.iOS
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// The UIApplicationDelegate for the application. This class is 
	/// responsible for launching the User Interface of the application, 
	/// as well as listening (and optionally responding) to application 
	/// events from iOS.
	/// </remarks>
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		private App _mainApp;

		public override UIWindow Window
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// This method is invoked when the application has loaded and 
		/// is ready to run. In this  method you should instantiate the 
		/// window, load the UI into it and then make the window visible.
		/// You have 17 seconds to return from this method, or iOS will 
		/// terminate your application.
		/// </remarks>
		/// <param name="app"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			// FIX: The app delegate must implement the window property if it wants to use a main storyboard file.
			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			// Init Popup
			Rg.Plugins.Popup.IOS.Popup.Init();

			// Setting up xamarin forms application
			Forms.Init();
			//var renderer = new Renderer.ChartRenderer();

			// Register dependency services for xamarin
			DependencyService.Register<IAdapter, Adapter>();

			ServicePool.Current.AddService<IDialogServices>(new DialogServices());

			// Setting up app values
			var version = NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
			var appName = NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleDisplayName").ToString();
			var uiScreen = UIScreen.MainScreen.Bounds;

			//NSBundle.MainBundle.BundleUrl.PathComponents
			App.AppTitelVersion = $"{appName} - {version}";
			App.ScreenSize = new Size((int)uiScreen.Width, (int)uiScreen.Height);
			App.ScreenScale = UIScreen.MainScreen.Scale;

			// Same Code for iOS and Android
			//InitIoc();
			InitLogging();
			//InitDevice(options);

			_mainApp = new App();
			LoadApplication(_mainApp);

			//SetDefaultorientation();

			// base implementation
			var success = base.FinishedLaunching(app, options);

			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);

			App.SystemFont = CheckInstalledFonts();

			//Theme.Apply(UINavigationBar.Appearance);
			Theme.Apply();

			// start the application
			return success;
		}

		#region Orientation

		public UIInterfaceOrientationMask CurrentOrientation = UIInterfaceOrientationMask.Portrait;

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
		{
			return CurrentOrientation;
		}

		#endregion Orientation

		private void OnKeyboardNotification(NSNotification notification)
		{
			// Check if the keyboard is becoming visible
			if (notification.Name == UIKeyboard.WillShowNotification)
			{
				_mainApp.KeyboardStatus = KeybaordStatus.WillShow;
			}
			else if (notification.Name == UIKeyboard.WillHideNotification)
			{
				_mainApp.KeyboardStatus = KeybaordStatus.WillHide;
			}
		}

		private static string CheckInstalledFonts()
		{
			var installedFonts = UIFont.FamilyNames;
			foreach (var family in installedFonts)
			{
				Console.Write("Family - " + family + ": ");
				foreach (var name in UIFont.FontNamesForFamilyName(family))
				{
					Console.Write(name + ", ");
				}
				Console.WriteLine("");
			}
			Console.WriteLine("");
			Console.WriteLine(UIFont.FamilyNames.Length);

			var systemFont = UIFont.SystemFontOfSize(18);
			return systemFont.FamilyName;
		}

		/// <summary>
		/// Same Code for iOS and Android
		/// </summary>
		private static void InitLogging()
		{
			try
			{
				var settings = LogManagerFactory.CreateLibraryDefaultSettings();
				var target = new TraceTarget();

				settings.AddTarget(LogLevel.Debug, LogLevel.Fatal, target);
				settings.IsEnabled = true;

				Logger.Current.StartLogging(settings);
				Logger.Current.LogInfo(() => "InitLogging()");
			}
			catch (Exception exception)
			{
				Debug.WriteLine("InitLogging", exception.Message);
			}
		}
	}
}
