using System;
using System.Diagnostics;

namespace Sigeko.CuckooClock.Views
{
	/// <summary>
	/// https://github.com/rotorgames/Rg.Plugins.Popup
	/// </summary>
	public partial class ImagePopUpPage
	{
		public ImagePopUpPage()
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
