using System.Threading.Tasks;
using Sigeko.AppFramework.Models;
using Sigeko.AppFramework.Services;

namespace Sigeko.AppFramework.ViewModels
{
    public abstract class ViewModelBase : ModelBase, IServicePool
    {
        private bool _isBusy;
        private bool _hasChanges;
        private bool _hasErrors;
        private bool _canGoBack;
        private string _state;

		protected bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        protected bool HasChanges
        {
            get { return _hasChanges; }
			set { SetProperty(ref _hasChanges, value); }
		}

		protected bool HasErrors
        {
            get { return _hasErrors; }
			set { SetProperty(ref _hasErrors, value); }
		}

		protected bool CanGoBack
        {
            get { return _canGoBack; }
			set { SetProperty(ref _canGoBack, value); }
		}

		protected string State
        {
            get { return _state; }
			set { SetProperty(ref _state, value); }
		}

		/// <summary>
		/// Not used
		/// </summary>
		protected internal virtual void Initialize()
        {
        }

		/// <summary>
		/// Not used
		/// </summary>
		protected internal virtual void BindData()
		{
		}

		/// <summary>
		/// Not used
		/// </summary>
		protected internal virtual async Task CleanUp()
		{
			// Just for compiler warnings
			await Task.Delay(10);
		}

		/// <summary>
		/// not used
		/// </summary>
		/// <returns></returns>
		protected virtual bool Validate()
        {
            return true;
        }

        protected virtual void SaveChanges()
        {
        }

        protected virtual void GotoState(string stateName)
        {
            this.State = stateName;
        }

		/// <summary>
		/// not used
		/// </summary>
        protected virtual void GoBack()
        {
        }

		/// <summary>
		/// not used
		/// </summary>
        protected virtual void OnNavigateTo()
        {
            if (this.HasChanges || !this.HasErrors)
            {
                if (this.Validate())
                {
                    this.SaveChanges();
                }
            }
        }

        public void AddService<T>(T service) where T : class
        {
            ServicePool.Current.AddService(service);
        }

        public T GetService<T>() where T : class
        {
            return ServicePool.Current.GetService<T>();
        }

        public void RemoveService<T>() where T : class
        {
            ServicePool.Current.RemoveService<T>();
        }
    }
}
