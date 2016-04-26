using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sigeko.AppFramework.ViewModels;
using Sigeko.AppFramework.Views;

namespace Sigeko.AppFramework.Navigation
{
    public interface INavigationService
    {
        IReadOnlyList<ViewModelBase> ModalStack { get; }
		IReadOnlyList<Type> NavigationStack { get; }
		IReadOnlyList<IView> NavigationPageStack { get; }
		void InsertBefore(ViewModelBase viewModel, ViewModelBase beforeViewModel);
        Task<ViewModelBase> PopAsync();
        Task<ViewModelBase> PopAsync(bool animated);
        Task<ViewModelBase> PopModalAsync();
        Task<ViewModelBase> PopModalAsync(bool animated);
        Task PopToRootAsync();
        Task PopToRootAsync(bool animated);
		Task PushAsync(ViewModelBase viewModel);
		Task PushAsync(ViewModelBase viewModel, bool animated);
		Task PushPopupAsync(ViewModelBase viewModel);
		Task PushPopupAsync(ViewModelBase viewModel, bool animated);
		Task PushModalAsync(ViewModelBase viewModel);
        Task PushModalAsync(ViewModelBase viewModel, bool animated);
		Task PushBackToAsync(Type viewModel);
		Task PushBackToAsync(Type viewModel, bool animated);
		void Remove(IView page);
		void Remove(ViewModelBase viewModel);
	}
}
