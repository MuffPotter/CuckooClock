using Sigeko.AppFramework.Views;
using Sigeko.CuckooClock.Services;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Views
{
	public class BasePage : MvvmContentPage
	{
		protected DeviceOrientation LastOrientation;


		public BasePage() : this(DeviceOrientation.Auto)
		{
		}

		public BasePage(DeviceOrientation orientation)
		{
			LastOrientation = DependencyService.Get<IOrientation>().GetOrientation();
			DependencyService.Get<IOrientation>().SetOrientation(orientation);
		}
	}
}