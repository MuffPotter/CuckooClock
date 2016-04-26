using System;

namespace Sigeko.CuckooClock.Models
{
	public class BluetoothDevice : ListViewBase
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string ImageSource { get; set; }

		public DeviceState State { get; set; }	
	}
}