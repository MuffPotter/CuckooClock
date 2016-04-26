using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLStorage;
using Sigeko.AppFramework.ViewModels;
using Sigeko.CuckooClock.Assets;
using Sigeko.CuckooClock.Models;
using Sigeko.CuckooClock.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Sigeko.CuckooClock
{
	/// <summary>
	/// 
	/// </summary>
	public partial class App : Application
	{
		public static int ActiveView { get; set; }

		public static Settings Settings { get; set; }

		public static string AppTitelVersion { get; set; }

		public static Size ScreenSize { get; set; }

		public static double ScreenScale { get; set; }

		public static string SystemFont { get; set; }

		private KeybaordStatus _keyboardStatus;

		public KeybaordStatus KeyboardStatus
		{
			get { return _keyboardStatus; }
			set
			{
				_keyboardStatus = value;
				MessagingCenter.Send(this, "Keyboard", value);
			}
		}

		public App()
		{
			InitializeComponent();

			BootStrapper.InitializeServices();

			var viewModel = new HomeViewModel();
			MainPage = new NavigationPage(new Views.HomePage { BindingContext = viewModel })
			{
				// TODO: Sigeko Farben
				BarBackgroundColor = ColorResources.MainYellow,
				BarTextColor = Color.White
			};
		}

		/// <summary>
		/// Handle when your app starts
		/// </summary>
		protected override void OnStart()
		{
		}

		/// <summary>
		/// Handle when your app sleeps
		/// </summary>
		protected override void OnSleep()
		{
			// Create settings repository
			var settingsRepositoryy = new SettingsRepository();

			// Read some configuration settings
			Task.Run(async () => await settingsRepositoryy.SaveSettings());
		}

		/// <summary>
		/// Handle when your app resumes
		/// </summary>
		protected override void OnResume()
		{
			// Create settings repository
			var settingsRepositoryy = new SettingsRepository();

			// Read some configuration settings
			Task.Run(async () => Settings = await settingsRepositoryy.ReadSettings());
		}
	}

	public class Settings
	{
		public DateTime SaveTime { get; set; }

		public List<Alarm> Alarms { get; set; }
	}
}

