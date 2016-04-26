using Sigeko.AppFramework.ViewModels;
using Sigeko.CuckooClock.Services;

namespace Sigeko.CuckooClock.ViewModels
{
	public partial class BaseViewModel : ViewModelBase
	{
		/// <summary>
		/// Eigener plattformspefischer DialogService
		/// </summary>
		private IDialogServices _dialogServices;

 		/// <summary>
		/// Eigener plattformspefischer DialogService
		/// </summary>
		public IDialogServices DialogServices 
			=> _dialogServices ?? (_dialogServices = GetService<IDialogServices>());

		#region ctor / overrides

		public BaseViewModel()
		{
		}

		#endregion ctor / overrides

		#region fields (public)

		private string _pageTitel;

		public string PageTitel
		{
			get { return _pageTitel; }
			set { SetProperty(ref _pageTitel, value); }
		}

		#endregion fields (public)
	}
}
