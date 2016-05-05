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
		private bool _continueTimer;

		private readonly IAdapter _bluetoothAdapter;

		private IList<IDevice> _devices;

		private IEnumerable<IDevice> DiscoveredDevices 
			=> _bluetoothAdapter?.DiscoveredDevices;

		private IDevice DeviceFromId(Guid deviceId)
		{
			return DiscoveredDevices?.FirstOrDefault(l => l.Id == deviceId);
		}

		public BluetoothService()
		{
			//_bluetoothAdapter = DependencyService.Get<IAdapter>();
			_bluetoothAdapter = App.BluetoothAdapter;
			if (_bluetoothAdapter == null)
				return;

			//_bluetoothAdapter.ScanTimeout = TimeSpan.FromSeconds(10);
			//_bluetoothAdapter.ConnectionTimeout = TimeSpan.FromSeconds(10);

			//_bluetoothAdapter.DeviceDiscovered -= OnDiscovered;
			//_bluetoothAdapter.DeviceConnected -= OnConnected;
			_bluetoothAdapter.DeviceDiscovered += OnDiscovered;
			_bluetoothAdapter.DeviceConnected += OnConnected;
			_bluetoothAdapter.StopScanningForDevices();
		}

		public void StartScanning()
		{
			StartScanning(300);
		}

		public void StartTestScanning()
		{
			Device.StartTimer(new TimeSpan(0, 0, 3), OnTimer);
			_continueTimer = true;
		}

		public void StartScanning(int scanTimeout)
		{
			if (_bluetoothAdapter == null)
				return;

			_bluetoothAdapter.ScanTimeout = TimeSpan.FromSeconds(scanTimeout);
			_bluetoothAdapter.ScanTimeoutElapsed += OnScanTimeoutElapsed;

			_devices = new List<IDevice>();

			OnDeviceDiscovered(null);
			_bluetoothAdapter.StartScanningForDevices();
		}

		private void OnScanTimeoutElapsed(object sender, EventArgs e)
		{
			_bluetoothAdapter.ScanTimeoutElapsed -= OnScanTimeoutElapsed;
			StartScanning(300);
		}

		private bool OnTimer()
		{
			OnDeviceDiscovered(null);
			return _continueTimer;
		}

		public void StopScanning()
		{
			_continueTimer = false;

			if (_bluetoothAdapter == null)
				return;

			_bluetoothAdapter.DeviceDiscovered -= OnDiscovered;
			_bluetoothAdapter.DeviceConnected -= OnConnected;
			_bluetoothAdapter.ScanTimeoutElapsed -= OnScanTimeoutElapsed;
			_bluetoothAdapter.StopScanningForDevices();	
		}

		public void ConnectToDevice(Guid deviceId)
		{
			if (_bluetoothAdapter == null)
				return;

			var device = DeviceFromId(deviceId);
			if(device == null)
				return;

			_bluetoothAdapter.ConnectToDevice(device);
		}

		public IEnumerable<IDevice> GetScannnedDevices()
		{
			return _bluetoothAdapter?.DiscoveredDevices;
		}

		public IDevice ConnectedDevice()
		{
			return DiscoveredDevices?.FirstOrDefault(l => l.State == DeviceState.Connected);
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