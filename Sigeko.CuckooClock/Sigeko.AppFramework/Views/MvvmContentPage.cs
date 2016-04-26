using Sigeko.AppFramework.ViewModels;
using Xamarin.Forms;

namespace Sigeko.AppFramework.Views
{
    public class MvvmContentPage : ContentPage, IView
    {
		/// <summary>
		/// 
		/// </summary>
	    public MvvmContentPage()
	    {
			// <ContentPage NavigationPage.BackButtonTitle="Back">
			NavigationPage.SetBackButtonTitle(this, "Zurück");

			// Attached property associated to a page (not the NavigationPage), 
			// it is only set when the page is initially constructed by the 
			// platform code (e.g. the UIViewController, or view).. that's why 
			// you have to set it as part of the constructor.
			//NavigationPage.SetTitleIcon(this, "Pages/Alarm.png");
		}

		/// <summary>
		/// 
		/// </summary>
	    protected override void OnAppearing()
	    {	
			// Basismplementierung
		    base.OnAppearing();

			var viewModel = this.BindingContext as ViewModelBase;
			viewModel?.Initialize();
	    }

		/// <summary>
		/// 
		/// </summary>
	    protected override void OnDisappearing()
	    {
			// Basismplementierung
		    base.OnDisappearing();

			var viewModel = this.BindingContext as ViewModelBase;
			viewModel?.CleanUp();
		}
	}
}
