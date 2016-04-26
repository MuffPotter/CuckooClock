using Sigeko.AppFramework.Views;
using Sigeko.CuckooClock.Services;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Views
{
	public class BaseTabbedPage : MvvmTabbedPage
	{
		protected DeviceOrientation LastOrientation;


		public BaseTabbedPage() : this(DeviceOrientation.Auto)
		{
		}

		public BaseTabbedPage(DeviceOrientation orientation)
		{
			LastOrientation = DependencyService.Get<IOrientation>().GetOrientation();
			DependencyService.Get<IOrientation>().SetOrientation(orientation);
		}

		#region ActiveView BindableProperty

		/// <summary>
		/// Get or Sets the ActiveView bindable property. 
		/// </summary>
		public int ActiveView
		{
			get { return (int)GetValue(ActiveViewProperty); }
			set { SetValue(ActiveViewProperty, value); }
		}

		/// <summary>
		/// Identifies the ActiveView bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty ActiveViewProperty =
			BindableProperty.Create(nameof(ActiveView), typeof(int), typeof(BaseTabbedPage),
				0, BindingMode.TwoWay, null, OnActiveViewPropertyChanged);

		/// <summary>
		/// ActiveView changed handler.
		/// </summary>
		/// <param name="bindable">AppButton that changed its ActiveView.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnActiveViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as TabbedPage;
			if (element == null)
				return;

			var viewIndex = (int) newValue;
			if (element.Children.Count > viewIndex)
			{
				element.SelectedItem = element.Children[viewIndex];
			}
		}

		#endregion ActiveView Bindable Property

	}
}