using System;
using System.Collections.Generic;
using BluetoothLE.Core;

namespace Sigeko.CuckooClock.Services
{
	public interface IBluetoothService
	{
		void StartScanning();

		void StopScanning();

		void ConnectToDevice(Guid deviceId);

		IEnumerable<IDevice> GetScannnedDevices();

		IDevice ConnectedDevice();

		event EventHandler<BluetoothService.DeviceEventArgs<IDevice>> DeviceDiscovered;

		event EventHandler<BluetoothService.DeviceEventArgs<IDevice>> DeviceConnected;
	}
}