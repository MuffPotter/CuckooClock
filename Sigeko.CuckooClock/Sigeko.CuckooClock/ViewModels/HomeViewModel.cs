using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BluetoothLE.Core;
using Sigeko.AppFramework.Commands;
using Sigeko.AppFramework.Navigation;
using Sigeko.CuckooClock.Services;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		#region fields private

		private readonly IBluetoothService _bluetoothService;

		private readonly AlarmRepository _alarmRepository;

		#endregion fields private

		#region ctor

		public HomeViewModel()
		{
			_bluetoothService = GetService<IBluetoothService>();
			_alarmRepository = new AlarmRepository();

			GetSettings();

			this.TestCommand = new DelegateCommand(ExecuteTestCommand);
			this.NextPageCommand = new DelegateCommand(ExecuteNextPageCommand);

			Device.StartTimer(new TimeSpan(0, 0, 3), OnTimer);
			//_continueTimer = true;
		}

		private bool OnTimer()
		{
			Initialize();
			return true;
		}

		protected override void Initialize()
		{
			NextAlarmInfo = GetNextAlarm();
			CurrentDevice = GetCurrentBluetoothDevice();
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

		private ICommand _testCommand;

		public ICommand TestCommand
		{
			get { return _testCommand; }
			set { SetProperty(ref _testCommand, value); }
		}

		private static async void ExecuteTestCommand(object parameter)
		{
			// Create settings repository
			var settingsRepositoryy = new SettingsRepository();

			// Read some configuration settings
			var success = await settingsRepositoryy.DeleteSettings();
			var settings = await settingsRepositoryy.ReadSettings();
			App.Settings = settings;
			success = await settingsRepositoryy.SaveSettings();
			settings = await settingsRepositoryy.ReadSettings();
		}

		private ICommand _nextPageCommand;

		public ICommand NextPageCommand
		{
			get { return _nextPageCommand; }
			set { SetProperty(ref _nextPageCommand, value); }
		}

		private async void ExecuteNextPageCommand(object parameter)
		{
			if (parameter == null || (string)parameter == "Bluetooth")
				await NavigationService.Current.PushAsync(new MainViewModel(0));
			else
				await NavigationService.Current.PushAsync(new MainViewModel(1));
		}

		#endregion commands

		#region methods private

		private async Task GetSettings()
		{
			// Create settings repository
			var settingsRepositoryy = new SettingsRepository();

			// Read some configuration settings
			App.Settings = await settingsRepositoryy.ReadSettings();

			NextAlarmInfo = GetNextAlarm();
			CurrentDevice = GetCurrentBluetoothDevice();
		}

		private string GetNextAlarm()
		{
			var alarms = _alarmRepository.ReadAlarmList();
			if (alarms != null && alarms.Any() == true)
			{
				var alarm = alarms.OrderBy(l => l.NextAlarm).FirstOrDefault();
				if (alarm == null)
					return "Kein Alarm eingestellt!";

				var nextAlarm = alarm.NextAlarm;
				if (nextAlarm == DateTime.MaxValue)
					return "Kein Alarm eingestellt!";

				var textMessage = alarm.Name + Environment.NewLine +
				                  nextAlarm.ToString("f") + Environment.NewLine;

				var delta = nextAlarm - DateTime.Now;
				var days = (int)delta.TotalDays;
				var hours = (int)delta.TotalHours - days * 24;
				var minutes = (int)delta.TotalMinutes - days * 24 * 60 - hours * 60;
				if (days > 0)
				{
					textMessage += $"in {days} Tagen ";
					if (hours > 0)
						textMessage += $"{hours} Stunden ";
				}
				else if (delta.TotalMinutes > 60)
				{
					textMessage += $"in {hours} Stunden ";
				}

				if (minutes > 0)
				{
					textMessage += $"{minutes} Minuten";
				}

				return textMessage;

				//foreach (var alarm in alarms)
				//{
				//	var nextAlarm = alarm.NextAlarm;
				//}
			}

			return "Kein Alarm eingestellt!";
		}

		private IDevice GetCurrentBluetoothDevice()
		{
			var device = _bluetoothService?.GetScannnedDevices()?.FirstOrDefault();
			return device;
		}

		#endregion methods private
	}
}