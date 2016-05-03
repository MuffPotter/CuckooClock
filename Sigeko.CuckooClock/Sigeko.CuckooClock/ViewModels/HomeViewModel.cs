using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BluetoothLE.Core;
using Sigeko.AppFramework.Commands;
using Sigeko.AppFramework.Navigation;
using Sigeko.CuckooClock.Common;
using Sigeko.CuckooClock.Services;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		#region fields private

		private readonly IBluetoothService _bluetoothService;

		private readonly ISoundService _soundService;

		private readonly AlarmRepository _alarmRepository;

		private bool _continueTimer;

		#endregion fields private

		#region ctor

		public HomeViewModel()
		{
			_bluetoothService = GetService<IBluetoothService>();
			_soundService = GetService<ISoundService>();
			_alarmRepository = new AlarmRepository();

			GetSettings();

			this.NextPageCommand = new DelegateCommand(ExecuteNextPageCommand);
			this.ResetSettingsCommand = new DelegateCommand(ExecuteResetSettingsCommand);
			this.PlaySoundCommand = new DelegateCommand(async o => await ExecutePlaySoundCommand(o));
		}

		protected override void Initialize()
		{
			NextAlarmInfo = GetNextAlarm();
			CurrentDevice = GetCurrentBluetoothDevice();

			Device.StartTimer(new TimeSpan(0, 0, 10), OnTimer);
			_continueTimer = true;
		}

		protected override Task CleanUp()
		{
			_continueTimer = false;
			return null;
		}

		private bool OnTimer()
		{
			NextAlarmInfo = GetNextAlarm();
			CurrentDevice = GetCurrentBluetoothDevice();

			return _continueTimer;
		}

		#endregion ctor

		#region fields public

		public string AppInfo => App.AppTitelVersion;

		public IDevice CurrentDevice { get; set; }

		private string _nextAlarmInfo;

		public string NextAlarmInfo
		{
			get { return _nextAlarmInfo; }
			set { SetProperty(ref _nextAlarmInfo, value); }
		}

		public string BluetoothInfo
		{
			get
			{
				return CurrentDevice != null ? $"{CurrentDevice.Name}" : "Nicht verbunden!";
			}
		}

		#endregion fields public

		#region commands

		private ICommand _nextPageCommand;

		public ICommand NextPageCommand
		{
			get { return _nextPageCommand; }
			set { SetProperty(ref _nextPageCommand, value); }
		}

		private static async void ExecuteNextPageCommand(object parameter)
		{
			if (parameter == null || (string) parameter == "Bluetooth")
			{
				await NavigationService.Current.PushAsync(new MainViewModel(0));
			}
			else if ((string) parameter == "Alarm")
			{
				await NavigationService.Current.PushAsync(new MainViewModel(1));
			}
		}

		private ICommand _resetSettingsCommand;

		public ICommand ResetSettingsCommand
		{
			get { return _resetSettingsCommand; }
			set { SetProperty(ref _resetSettingsCommand, value); }
		}

		private static async void ExecuteResetSettingsCommand(object parameter)
		{
			// Create settings repository
			var settingsRepositoryy = new SettingsRepository();

			// Read some configuration settings
			await settingsRepositoryy.DeleteSettings();
			var settings = await settingsRepositoryy.ReadSettings();
			App.Settings = settings;
			await settingsRepositoryy.SaveSettings();
			settings = await settingsRepositoryy.ReadSettings();
			App.Settings = settings;
		}

		private ICommand _playSoundCommand;

		public ICommand PlaySoundCommand
		{
			get { return _playSoundCommand; }
			set { SetProperty(ref _playSoundCommand, value); }
		}

		private async Task ExecutePlaySoundCommand(object parameter)
		{
			if (_soundService == null)
				return;

			await _soundService.PlaySoundAsync("TestSound.mp3");
		}

		#endregion commands

		#region methods private

		private async void GetSettings()
		{
			// Create settings repository
			var settingsRepositoryy = new SettingsRepository();

			// Read configuration settings
			App.Settings = await settingsRepositoryy.ReadSettings();

			// Alarm und Device Informationen holen
			NextAlarmInfo = GetNextAlarm();
			CurrentDevice = GetCurrentBluetoothDevice();
		}

		/// <summary>
		/// Ermittelt den Text für den nächsten Alarm
		/// </summary>
		/// <returns>Den zusammengestellten Text</returns>
		private string GetNextAlarm()
		{
			// Konstante für: Keinen Alarm gefunden
			const string message = "Kein Alarm eingestellt!";

			try
			{
				// Alle eingestellten Alarme holen
				var alarms = _alarmRepository.ReadAlarmList();
				if (alarms == null || alarms.Any() == false)
					return message;

				// Sortieren, aufsteigend nach dem Zeitpunkt
				var alarm = alarms.OrderBy(l => l.NextAlarm).FirstOrDefault();
				if (alarm == null)
					return message;

				// Ist der Alarm überhaupt zutreffend?
				var nextAlarm = alarm.NextAlarm;
				if (nextAlarm == DateTime.MaxValue)
					return message;

				// Text zusammentellen
				// Alarmbezeichnung
				// Zeitpunkt: Mittwoch, 24.06.2016, 14:15
				var textMessage = alarm.Name + Environment.NewLine +
								  nextAlarm.ToString("f") + Environment.NewLine;

				// Nun die Tage, Stunden, Minuten bestimmen
				var textInfo = string.Empty;
				var delta = nextAlarm - DateTime.Now;
				var days = (int)delta.TotalDays;
				var hours = (int)delta.TotalHours - days * 24;
				var minutes = (int)delta.TotalMinutes - days * 24 * 60 - hours * 60;

				if (days > 0)
				{
					textInfo += $"{days} Tagen";
					if (hours > 0)
						textInfo += $" {hours} Stunden ";
				}
				else if (hours > 0)
				{
					textInfo += $"{hours} Stunden";
				}

				if (minutes > 0)
				{
					textInfo += $" {minutes} Minuten";
				}

				if (string.IsNullOrEmpty(textInfo) == false)
					textInfo = "in " + textInfo;

				// Text zurückgeben
				return textMessage + textInfo;
			}
			catch (Exception exception)
			{
				Logger.Current.LogException(exception);
				return "";
			}
		}

		/// <summary>
		/// Ermittelt das aktuell verbundene Bluetooth Device
		/// </summary>
		/// <returns>Null, wenn kein Gerät verbunden, sonst das Gerät</returns>
		private IDevice GetCurrentBluetoothDevice()
		{
			var device = _bluetoothService?.ConnectedDevice();
			return device;
		}

		#endregion methods private
	}
}