using System;
using System.Collections.Generic;
using System.Linq;
using BluetoothLE.Core;
using BluetoothLE.Core.Events;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Services
{
	/// <summary>
	/// Xamarin.BluetoothLE
	/// https://github.com/tbrushwyler/Xamarin.BluetoothLE
	/// Monkey.Robotics - Beta
	/// https://github.com/xamarin/Monkey.Robotics
	/// https://github.com/xamarin/Monkey.Robotics/tree/master/Source
	/// Bluetooth TI Sensor Browser (Xamarin.Forms)
	/// https://github.com/conceptdev/xamarin-forms-samples/tree/master/BluetoothTISensor
	/// </summary>
	public class BluetoothService : IBluetoothService
	{
		private readonly IAdapter _bluetoothService;

		private IList<IDevice> _devices;

		private IEnumerable<IDevice> DiscoveredDevices 
			=> _bluetoothService?.DiscoveredDevices;

		private IDevice DeviceFromId(Guid deviceId)
		{
			return DiscoveredDevices?.FirstOrDefault(l => l.Id == deviceId);
		}

		public BluetoothService()
		{
			_bluetoothService = DependencyService.Get<IAdapter>();
			if (_bluetoothService == null)
				return;

			_bluetoothService.ScanTimeout = TimeSpan.FromSeconds(10);
			_bluetoothService.ConnectionTimeout = TimeSpan.FromSeconds(10);

			_bluetoothService.DeviceDiscovered -= OnDiscovered;
			_bluetoothService.DeviceConnected -= OnConnected;
			_bluetoothService.DeviceDiscovered += OnDiscovered;
			_bluetoothService.DeviceConnected += OnConnected;
		}

		private bool _continueTimer;
		public void StartScanning()
		{
			Device.StartTimer(new TimeSpan(0,0,3), OnTimer);
			_continueTimer = true;
			return;

			StartScanning(10);
		}

		private bool OnTimer()
		{
			OnDeviceDiscovered(null);
			return _continueTimer;
		}

		public void StartScanning(int scanTimeout)
		{
			if (_bluetoothService == null)
				return;

			_bluetoothService.ScanTimeout = TimeSpan.FromSeconds(scanTimeout);

			_devices = new List<IDevice>();
			_bluetoothService.StartScanningForDevices();
		}

		public void StopScanning()
		{
			_continueTimer = false;

			if (_bluetoothService == null)
				return;

			_bluetoothService.DeviceDiscovered -= OnDiscovered;
			_bluetoothService.DeviceConnected -= OnConnected;
			_bluetoothService.StopScanningForDevices();	
		}

		public void ConnectToDevice(Guid deviceId)
		{
			if (_bluetoothService == null)
				return;

			var device = DeviceFromId(deviceId);
			if(device == null)
				return;

			_bluetoothService.ConnectToDevice(device);
		}

		public IEnumerable<IDevice> GetScannnedDevices()
		{
			return _bluetoothService?.DiscoveredDevices;
		}


		private void OnConnected(object sender, DeviceConnectionEventArgs eventArgs)
		{
			OnDeviceConnected(eventArgs.Device);
		}

		private void OnDiscovered(object sender, DeviceDiscoveredEventArgs eventArgs)
		{
			_devices.Add(eventArgs.Device);
			OnDeviceDiscovered(eventArgs.Device);
		}

		#region events

		/// <summary>
		/// Event args for the Bluetooth events.
		/// </summary>
		/// <typeparam name="TDevice">A bluetooth device.</typeparam>
		public class DeviceEventArgs<TDevice> : EventArgs
		{
			/// <summary>
			/// The event coming from the device
			/// </summary>
			public TDevice Device { get; set; }
		}

		/// <summary>
		/// Fires when a device is discoverd
		/// </summary>
		public event EventHandler<DeviceEventArgs<IDevice>> DeviceDiscovered;

		private void OnDeviceDiscovered(IDevice device)
		{
			var eventArgs = new DeviceEventArgs<IDevice> { Device = device };
			DeviceDiscovered?.Invoke(this, eventArgs);
		}

		/// <summary>
		/// Fires when a device is connected
		/// </summary>
		public event EventHandler<DeviceEventArgs<IDevice>> DeviceConnected;

		private void OnDeviceConnected(IDevice device)
		{
			var eventArgs = new DeviceEventArgs<IDevice> { Device = device };
			DeviceConnected?.Invoke(this, eventArgs);
		}

		#endregion events
	}
}