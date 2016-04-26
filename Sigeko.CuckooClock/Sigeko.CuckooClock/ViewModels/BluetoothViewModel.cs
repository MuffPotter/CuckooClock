using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BluetoothLE.Core;
using Sigeko.CuckooClock.Models;
using Sigeko.CuckooClock.Services;
using DeviceState = Sigeko.CuckooClock.Models.DeviceState;

namespace Sigeko.CuckooClock.ViewModels
{
	public partial class MainViewModel
	{
		private IBluetoothService _bluetoothService;

		#region fields (public)

		private ObservableCollection<BluetoothDevice> _deviceList;

		public ObservableCollection<BluetoothDevice> DeviceList
		{
			get { return _deviceList; }
			set { SetProperty(ref _deviceList, value); }
		}

		private bool _isScanning;

		public bool IsScanning
		{
			get { return _isScanning; }
			set { SetProperty(ref _isScanning, value); }
		}

		private double _progressValue;

		public double ProgressValue
		{
			get { return _progressValue; }
			set { SetProperty(ref _progressValue, value); }
		}

		#endregion fields (public)

		#region commands

		private ICommand _scanDevicesCommand;

		public ICommand ScanDevicesCommand
		{
			get { return _scanDevicesCommand; }
			set { SetProperty(ref _scanDevicesCommand, value); }
		}

		private async void ExecuteScanDevicesCommand(object parameter)
		{
			if (_bluetoothService == null)
			{
				return;
			}

			DeviceList = new ObservableCollection<BluetoothDevice>();
			IsScanning = true;
			ProgressValue = 0;

			_bluetoothService.DeviceConnected -= OnDeviceDiscovered;
			_bluetoothService.DeviceDiscovered -= OnDeviceDiscovered;
			_bluetoothService.DeviceConnected += OnDeviceDiscovered;
			_bluetoothService.DeviceDiscovered += OnDeviceDiscovered;
			_bluetoothService.StartScanning();
		}

		private void OnDeviceDiscovered(object sender, BluetoothService.DeviceEventArgs<IDevice> e)
		{
			ProgressValue += .1;
			DeviceList.Add(CreateDevice(null, DeviceList.Count));
		}

		private void OnDeviceConnected(object sender, BluetoothService.DeviceEventArgs<IDevice> eventArgs)
		{
		}

		private ICommand _stopScanCommand;

		public ICommand StopScanCommand
		{
			get { return _stopScanCommand; }
			set { SetProperty(ref _stopScanCommand, value); }
		}

		private async void ExecuteStopScanCommand(object parameter)
		{
			IsScanning = false;

			if (_bluetoothService == null)
			{
				return;
			}

			_bluetoothService.DeviceConnected -= OnDeviceDiscovered;
			_bluetoothService.DeviceDiscovered -= OnDeviceDiscovered;
			_bluetoothService.StopScanning();
		}

		private ICommand _connectToDeviceCommand;

		public ICommand ConnectToDeviceCommand
		{
			get { return _connectToDeviceCommand; }
			set { SetProperty(ref _connectToDeviceCommand, value); }
		}

		private async void ExecuteConnectToDeviceCommand(object parameter)
		{
			IsScanning = false;

			if (_bluetoothService == null)
			{
				return;
			}

			_bluetoothService.DeviceConnected -= OnDeviceDiscovered;
			_bluetoothService.DeviceDiscovered -= OnDeviceDiscovered;
			_bluetoothService.StopScanning();

			_bluetoothService.ConnectToDevice(Guid.NewGuid());
		}

		#endregion commands

		private ObservableCollection<BluetoothDevice> GetBluetoothDevices()
		{
			if (_bluetoothService == null)
				return null;

			var devices = _bluetoothService.GetScannnedDevices();
			return CreateTestData();

			if (devices == null)
				return null;

			var bluetoothDevices = new ObservableCollection<BluetoothDevice>();
			foreach (var device in devices)
			{
				var bluetoothDevice = CreateDevice(device, 0);
				if (bluetoothDevice != null)
					bluetoothDevices.Add(bluetoothDevice);
			}

			return bluetoothDevices;
		}

		private ObservableCollection<BluetoothDevice> CreateTestData()
		{
			var bluetoothDevices = new ObservableCollection<BluetoothDevice>();

			for (var i = 0; i < 10; i++)
			{
				var device = CreateDevice(null, i);
				bluetoothDevices.Add(device);
			}

			return bluetoothDevices;
		}

		private BluetoothDevice CreateDevice(IDevice device, int number)
		{
			return new BluetoothDevice
			{
				Id = Guid.NewGuid(),
				Name = "Test " + DateTime.Now.ToString("T") + ": " + number,
				ImageSource = "Sigeko.CuckooClock.Assets.Images.Icons.Bluetooth50.png",
				State = DeviceState.Disconnected,
				SelectCommand = ConnectToDeviceCommand
			};
		}
	}
}
