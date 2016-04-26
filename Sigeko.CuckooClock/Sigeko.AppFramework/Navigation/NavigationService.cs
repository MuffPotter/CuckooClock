using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Sigeko.AppFramework.Resolvers;
using Sigeko.AppFramework.ViewModels;
using Sigeko.AppFramework.Views;
using Xamarin.Forms;

namespace Sigeko.AppFramework.Navigation
{
	public class NavigationService : INavigationService
	{
		private static NavigationService _current;

		private NavigationService()
		{
			NavigationPage.Popped += OnNavigationPagePopped;
			NavigationPage.Pushed += OnNavigationPagePushed;
		}

		public static NavigationService Current
		{
			get { return _current ?? (_current = new NavigationService()); }
		}

		private NavigationPage NavigationPage
		{
			get
			{
				return Application.Current.MainPage != null
					? Application.Current.MainPage as NavigationPage
					: null;

			}
		}

		public INavigation Navigator
		{
			get
			{
				return Application.Current.MainPage != null
					? Application.Current.MainPage.Navigation : null;
			}
		}

		public IReadOnlyList<ViewModelBase> ModalStack
		{
			get
			{
				if (this.Navigator == null)
					return null;

				var list = new List<ViewModelBase>();
				foreach (var view in this.Navigator.ModalStack)
				{
					list.Add(ViewResolver.ResolveViewModel(view as IView));
				}
				return list;
			}
		}

		public IReadOnlyList<Type> NavigationStack
		{
			get
			{
				if (this.Navigator == null)
					return null;

				var list = new List<Type>();
				foreach (var view in this.Navigator.NavigationStack)
				{
					list.Add(ViewResolver.ResolveViewModelType(view as IView));
				}
				return list;
			}
		}

		public IReadOnlyList<IView> NavigationPageStack
		{
			get
			{
				if (this.Navigator == null)
					return null;

				var list = new List<IView>();
				foreach (var view in this.Navigator.NavigationStack)
				{
					list.Add(view as IView);
				}
				return list;
			}
		}

		public void InsertBefore(ViewModelBase viewModel, ViewModelBase beforeViewModel)
		{
			if (this.Navigator == null)
				return;

			var page = ViewResolver.ResolveView(viewModel) as Page;
			var before = ViewResolver.ResolveView(beforeViewModel) as Page;
			this.Navigator.InsertPageBefore(page, before);
		}

		public Task<ViewModelBase> PopAsync()
		{
			return PopAsync(true);
		}

		public async Task<ViewModelBase> PopAsync(bool animated)
		{
			if (this.Navigator == null)
				return null;

			var view = await this.Navigator.PopAsync(animated) as IView;
			return ViewResolver.ResolveViewModel(view);
		}

		public Task<ViewModelBase> PopModalAsync()
		{
			return this.PopModalAsync(true);
		}

		public async Task<ViewModelBase> PopModalAsync(bool animated)
		{
			if (this.Navigator == null)
				return null;

			var view = await this.Navigator.PopModalAsync(animated) as IView;
			return ViewResolver.ResolveViewModel(view);
		}

		public async Task PopToRootAsync()
		{
			await this.PopToRootAsync(true);
		}

		public async Task PopToRootAsync(bool animated)
		{
			if (this.Navigator == null)
				return;

			await this.Navigator.PopToRootAsync(animated);
		}

		public async Task PushAsync(IView view)
		{
			await this.PushAsync(view, true);
		}

		public async Task PushAsync(IView view, bool animated)
		{
			await this.Navigator.PushAsync(view as Page, animated);
		}

		public async Task PushAsync(ViewModelBase viewModel)
		{
			await this.PushAsync(viewModel, true);
		}

		public async Task PushAsync(ViewModelBase viewModel, bool animated)
		{
			var view = ViewResolver.ResolveView(viewModel);
			if (view == null)
				return;

			if (this.Navigator == null)
				return;

			await this.Navigator.PushAsync(view as Page, animated);
		}

		public async Task PushPopupAsync(ViewModelBase viewModel)
		{
			await this.PushPopupAsync(viewModel, true);
		}

		public async Task PushPopupAsync(ViewModelBase viewModel, bool animated)
		{
			var view = ViewResolver.ResolveView(viewModel);
			if (view == null)
				return;

			if (this.Navigator == null)
				return;

			var popUpPage = view as PopupPage;
			await PopupNavigation.PushAsync(view as PopupPage, true);
		}

		public async Task PushModalAsync(ViewModelBase viewModel)
		{
			await this.PushModalAsync(viewModel, true);
		}

		public async Task PushModalAsync(ViewModelBase viewModel, bool animated)
		{
			var view = ViewResolver.ResolveView(viewModel);
			if (view == null)
				return;

			if (this.Navigator == null)
				return;

			await this.Navigator.PushModalAsync(view as Page, animated);
		}

		/// <summary>
		/// Navigiert zurück zu einer View
		/// </summary>
		/// <param name="viewModelType">Der zugehörige Typ des ViewModels zur View</param>
		/// <returns></returns>
		public async Task PushBackToAsync(Type viewModelType)
		{
			await this.PushBackToAsync(viewModelType, true);
		}

		/// <summary>
		/// Navigiert zurück zu einer View
		/// TODO: bessere Lösung
		/// Use Navigation.RemovePage to remove all pages except the page 
		/// we want to go back to and the current page, and then using 
		/// Navigation.PopAsync() to go back.
		/// </summary>
		/// <param name="viewModelType">Der zugehörige Typ des VieModels zur View</param>
		/// <param name="animated">Flag für die Animatipon</param>
		/// <returns></returns>
		public async Task PushBackToAsync(Type viewModelType, bool animated)
		{
			// Stack ist leer, dann raus hier
			if (NavigationPageStack == null)
				return;

			// Page für ViewModel nicht gefunden, dann raus hier
			var viewType = ViewResolver.ResolveViewType(viewModelType);
			var view = NavigationPageStack.FirstOrDefault(l => l.GetType() == viewType);
			view = ViewResolver.ResolveView(view.BindingContext as ViewModelBase);
			if (view == null)
				return;

			// Seite nun aufrufen, ist jetzt aber zweimal vorhanden
			await PushAsync(view);

			do
			{
				// Index der vorletzten Seite
				var index = NavigationPageStack.Count - 2;
				if (index < 0)
					break;

				// View holen
				view = NavigationPageStack[index];
				if (view.GetType() == viewType)
				{
					Remove(view);
					break;
				}

				Remove(view);

			} while (true);
		}

		/// <summary>
		/// Entfernt eine View aus dem Stack der NavigationPage
		/// </summary>
		/// <param name="page">Die Page, die entfernt werden soll</param>
		public void Remove(IView page)
		{
			this.Navigator?.RemovePage(page as Page);
		}

		/// <summary>
		/// // TODO: JN ???
		/// </summary>
		/// <param name="viewModel"></param>
		public void Remove(ViewModelBase viewModel)
		{
			this.Navigator?.RemovePage(ViewResolver.ResolveView(viewModel) as Page);
		}

		#region Navigation Events

		/// <summary>
		/// Event that is raised when a page is pushed onto this NavigationPage element.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs">Page. Gets the page that was removed or is newly visible.</param>
		private static void OnNavigationPagePushed(object sender, NavigationEventArgs eventArgs)
		{
			// page: Seite von der weg navigiert worden ist!
			var page = eventArgs.Page as IView;
			var viewModel = page?.BindingContext as ViewModelBase;
			viewModel?.Initialize();
			viewModel?.BindData();
		}

		/// <summary>
		/// Event that is raised after a page is popped from this NavigationPage element.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs">Page. Gets the page that was removed or is newly visible.</param>
		private static void OnNavigationPagePopped(object sender, NavigationEventArgs eventArgs)
		{
			// page: Seite von der weg navigiert worden ist!
			var page = eventArgs.Page as IView;
			var viewModel = page?.BindingContext as ViewModelBase;
			viewModel?.CleanUp();
		}

		#endregion Navigation Events
	}
}
