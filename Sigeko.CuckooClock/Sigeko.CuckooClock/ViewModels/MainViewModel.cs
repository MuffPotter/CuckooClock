using System.Threading.Tasks;
using Sigeko.AppFramework.Commands;
using Sigeko.AppFramework.ViewModels;
using Sigeko.CuckooClock.Common;
using Sigeko.CuckooClock.Services;

namespace Sigeko.CuckooClock.ViewModels
{
	public partial class MainViewModel : ViewModelBase
	{
		private bool _processing;

		#region ctor / overrides

		public MainViewModel(int activeView)
		{
			_alarmRepository = new AlarmRepository();
			_settingsRepository = new SettingsRepository();

			ActiveView = activeView;
			App.ActiveView = activeView;
			DeviceList = GetBluetoothDevices();
			AlarmList = _alarmRepository.ReadAlarmList(EditAlarmCommand, DeleteAlarmCommand);
		}

		protected override void Initialize()
		{
			_bluetoothService = GetService<IBluetoothService>();
		}

		protected override void BindData()
		{
			Logger.Current.LogTrace("BindData: START");

			this.ScanDevicesCommand = new DelegateCommand(ExecuteScanDevicesCommand);
			this.StopScanCommand = new DelegateCommand(ExecuteStopScanCommand);
			this.ConnectToDeviceCommand = new DelegateCommand(ExecuteConnectToDeviceCommand);

			this.AddAlarmCommand = new DelegateCommand(ExecuteAddAlarmCommand);
			this.EditAlarmCommand = new DelegateCommand(ExecuteEditAlarmCommand);
			this.DeleteAlarmCommand = new DelegateCommand(ExecuteDeleteAlarmCommand);

			ActiveView = App.ActiveView;
			DeviceList = GetBluetoothDevices();
			AlarmList = _alarmRepository.ReadAlarmList(EditAlarmCommand, DeleteAlarmCommand);

			Logger.Current.LogTrace("BindData: END");
		}

		protected override async Task CleanUp()
		{
			if (_processing == true)
				return;

			_processing = true;

			if (_bluetoothService != null)
			{
				_bluetoothService.DeviceConnected -= OnDeviceDiscovered;
				_bluetoothService.DeviceConnected -= OnDeviceConnected;
				_bluetoothService.StopScanning();
			}

			// Create settings repository
			var settingsRepositoryy = new SettingsRepository();

			// Read some configuration settings
			await settingsRepositoryy.SaveSettings();
			_processing = false;
		}

		#endregion ctor / overrides

		private int _activeView;

		public int ActiveView
		{
			get { return _activeView; }
			set { SetProperty(ref _activeView, value); }
		}
	}
}
