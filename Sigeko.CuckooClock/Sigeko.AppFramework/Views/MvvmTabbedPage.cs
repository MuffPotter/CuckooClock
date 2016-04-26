using Sigeko.AppFramework.ViewModels;
using Xamarin.Forms;

namespace Sigeko.AppFramework.Views
{
    public class MvvmTabbedPage : TabbedPage, IView
    {
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
