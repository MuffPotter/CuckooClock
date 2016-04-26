using System;
using System.Diagnostics;
using Sigeko.AppFramework.ViewModels;
using Sigeko.AppFramework.Views;

namespace Sigeko.AppFramework.Resolvers
{
    public static class ViewResolver
    {
		public static IView ResolveView<TViewModel>(TViewModel viewModel) where TViewModel : ViewModelBase
		{
			try
			{
				var typeName = viewModel.GetType().AssemblyQualifiedName;
				var viewTypeName = typeName.Replace(".ViewModels", ".Views").Replace("ViewModel", "Page");
				var viewType = Type.GetType(viewTypeName);
				if (viewType == null)
					return null;

				var view = Activator.CreateInstance(viewType) as IView;
				if (view == null)
					return null;

				view.BindingContext = viewModel;
				return view;
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
				return null;
			}
		}

		public static Type ResolveViewType(Type viewModelType) 
		{
			try
			{
				var typeName = viewModelType.AssemblyQualifiedName;
				var viewTypeName = typeName.Replace(".ViewModels", ".Views").Replace("ViewModel", "Page");
				return Type.GetType(viewTypeName);
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
				return null;
			}
		}

		public static ViewModelBase ResolveViewModel<TView>(TView view) where TView : IView
		{
			try
			{
				var typeName = view.GetType().AssemblyQualifiedName;
				var viewModelTypeName = typeName.Replace("Page", "ViewModel").Replace(".Views", ".ViewModels");
				var viewModelType = Type.GetType(viewModelTypeName);
				if (viewModelType != null)
				{
					var viewModel = Activator.CreateInstance(viewModelType) as ViewModelBase;
					if (viewModel != null)
					{
						view.BindingContext = viewModel;
					}
					return viewModel;
				}
				return null;
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
				return null;
			}
		}

		public static Type ResolveViewModelType<TView>(TView view) where TView : IView
		{
			try
			{
				var typeName = view.GetType().AssemblyQualifiedName;
				var viewModelTypeName = typeName.Replace("Page", "ViewModel").Replace(".Views", ".ViewModels");
				return Type.GetType(viewModelTypeName);
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
				return null;
			}
		}
	}
}
