using System.Windows.Input;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Controls
{
	/// <summary>
	/// For custom renderer
	/// </summary>
	public class EntryEx : Entry
	{
		#region ExecuteCommand BindableProperty

		/// <summary>
		/// Get or Sets the ExecuteCommand bindable property. 
		/// </summary>
		public ICommand ExecuteCommand
		{
			get { return (ICommand) GetValue(ExecuteCommandProperty); }
			set { SetValue(ExecuteCommandProperty, value); }
		}

		/// <summary>
		/// Identifies the ExecuteCommand bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty ExecuteCommandProperty =
			BindableProperty.Create(nameof(ExecuteCommand), typeof(ICommand), typeof(EntryEx),
				default(ICommand), BindingMode.TwoWay);

		#endregion ExecuteCommand Bindable Property

		#region FocusElementName BindableProperty

		/// <summary>
		/// Get or Sets the FocusElement bindable property. 
		/// </summary>
		public string FocusElementName
		{
			get { return (string) GetValue(FocusElementProperty); }
			set { SetValue(FocusElementProperty, value); }
		}

		/// <summary>
		/// Identifies the FocusElement bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty FocusElementProperty =
			BindableProperty.Create(nameof(FocusElementName), typeof(string), typeof(EntryEx),
				default(string), BindingMode.TwoWay);

		#endregion FocusElementName Bindable Property
	}
}
