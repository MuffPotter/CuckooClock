using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
			try
			{
				InitializeComponent();
				NavigationPage.SetBackButtonTitle(this, "Cancel");

			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
			}
		}
    }
}
