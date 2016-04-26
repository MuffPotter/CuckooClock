using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Views
{
    public partial class AlarmPage
	{
        public AlarmPage()
        {
			try
			{
				InitializeComponent();
				//Image img =new Image();
				//img.Source
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
			}
		}
    }
}
