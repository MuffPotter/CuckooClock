using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Views
{
    public partial class AlarmEditPage
	{
        public AlarmEditPage()
        {
			try
			{
				InitializeComponent();

				// <ContentPage NavigationPage.BackButtonTitle="Back">
				NavigationPage.SetBackButtonTitle(this, "Cancel");
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
			}
		}
    }
}
