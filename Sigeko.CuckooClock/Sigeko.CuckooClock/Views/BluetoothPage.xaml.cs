using System;
using System.Diagnostics;

namespace Sigeko.CuckooClock.Views
{
    public partial class BluetoothPage
	{
        public BluetoothPage()
        {
			try
			{
				InitializeComponent();
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
			}
		}
    }
}
