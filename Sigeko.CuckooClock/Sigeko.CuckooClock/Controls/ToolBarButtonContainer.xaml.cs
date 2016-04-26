using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Sigeko.CuckooClock.Assets;
using Sigeko.CuckooClock.Models;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Controls
{
    public partial class ToolBarButtonContainer
	{
		#region ctor

		public ToolBarButtonContainer()
		{
			InitializeComponent();

			this.ButtonPanel.Children.Clear();
			this.SelectedButton = ToolbarButtonType.Undefined;
			this.SetSelectedButton(this.SelectedButton);
		}

		#endregion ctor

		#region SelectedButton BindableProperty

		/// <summary>
		/// Get or Sets the HeaderText bindable property. 
		/// </summary>
		public ToolbarButtonType SelectedButton
		{
			get { return (ToolbarButtonType)GetValue(SelectedButtonProperty); }
			set { SetValue(SelectedButtonProperty, value); }
		}

		/// <summary>
		/// Identifies the SelectedButton bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty SelectedButtonProperty =
			BindableProperty.Create(nameof(SelectedButton), typeof(ToolbarButtonType), typeof(ToolBarButtonContainer),
				ToolbarButtonType.Undefined, BindingMode.TwoWay, null,
				OnSelectedButtonPropertyChanged);

		/// <summary>
		/// SelectedButton changed handler.
		/// </summary>
		/// <param name="bindable">ToolBarButtonContainer that changed its SelectedButton.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnSelectedButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as ToolBarButtonContainer;
			element?.SetSelectedButton((ToolbarButtonType)newValue);
		}

		private void SetSelectedButton(ToolbarButtonType newValue)
		{
			if (this.ButtonPanel.Children == null)
				return;

			foreach (var element in this.ButtonPanel.Children)
			{
				var button = element as TabButton;
				if (button == null)
					continue;

				button.IsSelected = button.ButtonType == newValue;
			}
		}

		#endregion SelectedButton Bindable Property

		#region ButtonList BindableProperty

		/// <summary>
		/// Get or Sets the HeaderText bindable property. 
		/// </summary>
		public List<ToolbarButtonType> ButtonList
		{
			get { return (List<ToolbarButtonType>)GetValue(ButtonListProperty); }
			set { SetValue(ButtonListProperty, value); }
		}

		/// <summary>
		/// Identifies the ButtonList bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty ButtonListProperty =
			BindableProperty.Create(nameof(ButtonList), typeof(List<ToolbarButtonType>), typeof(ToolBarButtonContainer),
				null, BindingMode.TwoWay, null,
				OnButtonListPropertyChanged);

		/// <summary>
		/// ButtonList changed handler.
		/// </summary>
		/// <param name="bindable">ToolBarButtonContainer that changed its ButtonList.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnButtonListPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as ToolBarButtonContainer;
			element?.CreateButtonList((List<ToolbarButtonType>)newValue);
		}

		private void CreateButtonList(IReadOnlyCollection<ToolbarButtonType> buttonList)
		{
			// Wenn keine Liste übergeben worden ist, dann Liste löschen
			if (buttonList == null || buttonList.Any() == false)
			{
				this.ButtonPanel.Children.Clear();
				return;
			}

			// Liste nun Löschen und neu Anlegen
			RemoveEvents();
			this.ButtonPanel.Children.Clear();

			foreach (var navButtonType in buttonList)
			{
				var button = CreateButton(navButtonType);
				button.IsVisible = true;
				button.BackgroundColor = ColorResources.SelectedYellow;
				button.SelectedBackgroundColor = ColorResources.MainYellow;
				button.Text = GetTextForButton(navButtonType);
				button.Clicked += OnButtonClicked;
				this.ButtonPanel.Children.Add(button);
			}

			SetSelectedButton(SelectedButton);
		}

		private static TabButton CreateButton(ToolbarButtonType buttonType)
		{
			var navButton = new TabButton
			{
				IsSelected = false,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				HeightRequest = 40,
				MinimumHeightRequest = 40,
				ButtonType = buttonType,
			};
			return navButton;
		}

		private static string GetTextForButton(ToolbarButtonType buttonType)
		{
			switch (buttonType)
			{
				case ToolbarButtonType.Undefined:
					return string.Empty;

				default:
					return string.Empty;
			}
		}

		private void RemoveEvents()
		{
			foreach (var element in this.ButtonPanel.Children)
			{
				var navButton = element as TabButton;
				if (navButton == null)
					continue;

				navButton.Clicked -= OnButtonClicked;
			}
		}

		private void OnButtonClicked(object sender, ButtonEventArgs eventArgs)
		{
			this.SelectedButton = eventArgs.NewType;
			SetSelectedButton(eventArgs.NewType);
			this.Command?.Execute(eventArgs.NewType);
		}

		#endregion ButtonList Bindable Property

		#region Command BindableProperty

		/// <summary>
		/// Get or Sets the Command bindable property. 
		/// </summary>
		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		/// <summary>
		/// Identifies the Command bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ToolBarButtonContainer),
				default(ICommand), BindingMode.TwoWay, null, OnCommandPropertyChanged);

		/// <summary>
		/// CommandProperty changed handler.
		/// </summary>
		/// <param name="bindable">AppButton that changed its name.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as ToolBarButtonContainer;
			if (element == null)
				return;
		}

		#endregion Command Bindable Property
	}
}
