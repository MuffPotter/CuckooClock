using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Sigeko.AppFramework.Commands;
using Sigeko.AppFramework.ViewModels;

namespace Sigeko.CuckooClock.ViewModels
{
	public class PopUpBaseViewModel : ViewModelBase
	{
		private ICommand _closePopUpCommand;

		public ICommand ClosePopUpCommand
		{
			get { return _closePopUpCommand; }
			set { SetProperty(ref _closePopUpCommand, value); }
		}


		public PopUpBaseViewModel()
		{
			this.ClosePopUpCommand = new DelegateCommand(async o => await ExecuteClosePopUpCommand(o));
		}

		private static async Task ExecuteClosePopUpCommand(object paramter)
		{
			await PopupNavigation.PopAsync();
		}
	}
}
