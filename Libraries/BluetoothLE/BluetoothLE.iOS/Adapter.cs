using System;
using BluetoothLE.Core;
using CoreBluetooth;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using BluetoothLE.Core.Events;
using Foundation;

namespace BluetoothLE.iOS
{
	/// <summary>
	/// Concrete implementation of <see cref="BluetoothLE.Core.IAdapter"/> interface.
	/// https://github.com/tbrushwyler/Xamarin.BluetoothLE/blob/master/BluetoothLE.iOS/Adapter.cs
	/// </summary>
	public class Adapter : IAdapter // CBCentralManagerDelegate, IAdapter
	{
		private readonly CBCentralManager _central;

		private readonly AutoResetEvent _stateChanged;

		private static Adapter _current;

		/// <summary>
		/// Gets the current Adpater instance
		/// </summary>
		/// <value>The current Adapter instance</value>
		public static Adapter Current => _current;

		/// <summary>
		/// Initializes a new instance of the <see cref="BluetoothLE.iOS.Adapter"/> class.
		/// </summary>
		public Adapter()
		{
			// The dispatch queue to use to dispatch the central role events. 
			// If the value is nil, the central manager dispatches central 
			// role events using the main queue.
			// https://developer.xamarin.com/api/type/MonoTouch.CoreFoundation.DispatchQueue/
			//_central = new CBCentralManager();
			//_central = new CBCentralManager(CoreFoundation.DispatchQueue.MainQueue);
			//_central = new CBCentralManager(CoreFoundation.DispatchQueue.DefaultGlobalQueue);

			//var myDelegate = new CentralManagerDelegate();
			//_central = new CBCentralManager(myDelegate, null);
			_central = new CBCentralManager(CoreFoundation.DispatchQueue.DefaultGlobalQueue);
			//_central.Delegate = this;

			_central.DiscoveredPeripheral += DiscoveredPeripheral;
			_central.UpdatedState += UpdatedState;
			_central.ConnectedPeripheral += ConnectedPeripheral;
			_central.DisconnectedPeripheral += DisconnectedPeripheral;
			_central.FailedToConnectPeripheral += FailedToConnectPeripheral;

			ConnectedDevices = new List<IDevice>();
			_stateChanged = new AutoResetEvent(false);

			_current = this;
		}

		private async Task WaitForState(CBCentralManagerState state)
		{
			while (_central.State != state)
			{
				await Task.Run(() => _stateChanged.WaitOne());
			}
		}

		#region IAdapter implementation

		/// <summary>
		/// Occurs when device discovered.
		/// </summary>
		public event EventHandler<DeviceDiscoveredEventArgs> DeviceDiscovered = delegate { };

		/// <summary>
		/// Occurs when device connected.
		/// </summary>
		public event EventHandler<DeviceConnectionEventArgs> DeviceConnected = delegate { };

		/// <summary>
		/// Occurs when device disconnected.
		/// </summary>
		public event EventHandler<DeviceConnectionEventArgs> DeviceDisconnected = delegate { };

		/// <summary>
		/// Occurs when scan timeout elapsed.
		/// </summary>
		public event EventHandler ScanTimeoutElapsed = delegate { };

		/// <summary>
		/// Occurs when a device failed to connect.
		/// </summary>
		public event EventHandler<DeviceConnectionEventArgs> DeviceFailedToConnect = delegate { };

		/// <summary>
		/// Gets or sets the amount of time to scan for devices.
		/// </summary>
		/// <value>The scan timeout.</value>
		public TimeSpan ScanTimeout { get; set; }

		/// <summary>
		/// Gets or sets the amount of time to attempt to connect to a device.
		/// </summary>
		/// <value>The connection timeout.</value>
		public TimeSpan ConnectionTimeout { get; set; }

		/// <summary>
		/// Start scanning for devices.
		/// </summary>
		public void StartScanningForDevices()
		{
			StartScanningForDevices(new string[0]);
		}

		/// <summary>
		/// Start scanning for devices.
		/// </summary>
		/// <param name="serviceUuids">White-listed service UUIDs</param>
		public async void StartScanningForDevices(params string[] serviceUuids)
		{
			await WaitForState(CBCentralManagerState.PoweredOn);

			var uuids = new List<CBUUID>();
			foreach (var guid in serviceUuids)
			{
				uuids.Add(CBUUID.FromString(guid));
			}

			DiscoveredDevices = new List<IDevice>();
			IsScanning = true;

			//var device = new Device(Guid.NewGuid(), DateTime.Now.ToLongTimeString());
			//DiscoveredDevices.Add(device);
			//DeviceDiscovered(this, new DeviceDiscoveredEventArgs(device));

			//_central.ScanForPeripherals(uuids.ToArray());
			_central.ScanForPeripherals(peripheralUuids: null);

			await Task.Delay(ScanTimeout);

			if (IsScanning)
			{
				StopScanningForDevices();
				ScanTimeoutElapsed(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Stop scanning for devices.
		/// </summary>
		public void StopScanningForDevices()
		{
			IsScanning = false;
			_central.StopScan();
		}

		/// <summary>
		/// Connect to a device.
		/// </summary>
		/// <param name="device">The device.</param>
		public async void ConnectToDevice(IDevice device)
		{
			var peripheral = device.NativeDevice as CBPeripheral;
			_central.ConnectPeripheral(peripheral);

			await Task.Delay(ConnectionTimeout);

			if (ConnectedDevices.All(x => x.Id != device.Id))
			{
				_central.CancelPeripheralConnection(peripheral);
				var args = new DeviceConnectionEventArgs(device)
				{
					ErrorMessage = "The device connection timed out."
				};

				DeviceFailedToConnect(this, args);
			}
		}

		/// <summary>
		/// Discconnect from a device.
		/// </summary>
		/// <param name="device">The device.</param>
		public void DisconnectDevice(IDevice device)
		{
			var peripheral = device.NativeDevice as CBPeripheral;
			_central.CancelPeripheralConnection(peripheral);
		}

		/// <summary>
		/// Gets a value indicating whether this instance is scanning.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		public bool IsScanning { get; set; }

		/// <summary>
		/// Gets the discovered devices.
		/// </summary>
		/// <value>The discovered devices.</value>
		public IList<IDevice> DiscoveredDevices { get; set; }

		/// <summary>
		/// Gets the connected devices.
		/// </summary>
		/// <value>The connected devices.</value>
		public IList<IDevice> ConnectedDevices { get; set; }

		#endregion

		#region CBCentralManager delegate methods

		private void DiscoveredPeripheral(object sender, CBDiscoveredPeripheralEventArgs e)
		{
			var deviceId = Device.DeviceIdentifierToGuid(e.Peripheral.Identifier);
			Debug.WriteLine("Discovered " + deviceId);
			if (DiscoveredDevices.All(x => x.Id != deviceId))
			{
				var device = new Device(e.Peripheral);
				DiscoveredDevices.Add(device);
				DeviceDiscovered(this, new DeviceDiscoveredEventArgs(device));
			}
		}

		private void UpdatedState(object sender, EventArgs e)
		{
			Debug.WriteLine("State Changed to " + ((CBCentralManager)sender).State);
			_stateChanged.Set();
		}

		private void ConnectedPeripheral(object sender, CBPeripheralEventArgs e)
		{
			var deviceId = Device.DeviceIdentifierToGuid(e.Peripheral.Identifier);
			if (ConnectedDevices.All(x => x.Id != deviceId))
			{
				var device = new Device(e.Peripheral);
				ConnectedDevices.Add(device);
				DeviceConnected(this, new DeviceConnectionEventArgs(device));
			}
		}

		private void DisconnectedPeripheral(object sender, CBPeripheralErrorEventArgs e)
		{
			var deviceId = Device.DeviceIdentifierToGuid(e.Peripheral.Identifier);
			var connectedDevice = ConnectedDevices.FirstOrDefault(x => x.Id == deviceId);

			if (connectedDevice != null)
			{
				ConnectedDevices.Remove(connectedDevice);
				DeviceDisconnected(this, new DeviceConnectionEventArgs(connectedDevice));
			}
		}

		private void FailedToConnectPeripheral(object sender, CBPeripheralErrorEventArgs e)
		{
			var device = new Device(e.Peripheral);
			var args = new DeviceConnectionEventArgs(device)
			{
				ErrorMessage = e.Error.Description
			};

			DeviceFailedToConnect(this, args);
		}

//		public override void RetrievedConnectedPeripherals(CBCentralManager central, CBPeripheral[] peripherals)
//		{
//			foreach (CBPeripheral peripheral in peripherals)
//				Debug.WriteLine("RetrievedConnectedPeripherals: " + peripheral.Name);
//		}
//
//		public override void RetrievedPeripherals(CBCentralManager central, CBPeripheral[] peripherals)
//		{
//			foreach (CBPeripheral peripheral in peripherals)
//				Debug.WriteLine("RetrievedPeripherals: " + peripheral.Name);
//		}
//
//		public override void UpdatedState(CBCentralManager central)
//		{
//			Debug.WriteLine("State Changed to " + central.State);
//			//_stateChanged.Set();
//		}

		#endregion
	}

	public class CentralManagerDelegate : CBCentralManagerDelegate
	{
		bool scanned = false;

		public override void UpdatedState(CBCentralManager central)
		{
			var state = central.State;
			if (state == CBCentralManagerState.PoweredOn && !scanned)
			{
				var uuids = new List<CBUUID>();
				central.ScanForPeripherals(uuids.ToArray());
				scanned = true;
			}
		}

		public override void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber RSSI)
		{
			Console.WriteLine("Found something");
		}
	}
}

