using System;
using System.Windows.Input;
using Sigeko.CuckooClock.Models;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Controls
{
	public partial class TabButton
	{
		#region ctor

		public TabButton()
		{
			InitializeComponent();

			// TODO: raus, wenn Activity Buttons im Container
			IsVisible = false;

			this.WidthRequest = 95;
			this.HeightRequest = 25;
			this.MinimumWidthRequest = 95;
			this.MinimumHeightRequest = 25;

			// TODO JN
			this.AddTouchHandler(this, this.OnClicked);
			this.AddTouchHandler(this.MainGrid, this.OnClicked);
			this.AddTouchHandler(this.BoxView, this.OnClicked);
			this.AddTouchHandler(this.MainPanel, this.OnClicked);
		}

		protected void AddTouchHandler(View view, Action action)
		{
			view.GestureRecognizers.Add(
				new TapGestureRecognizer
				{
					Command = new Command(() =>
					{
						view.Opacity = 0.6;
						view.FadeTo(1);
						action();
					})
				});
		}

		public event EventHandler<ButtonEventArgs> Clicked;

		private void OnClicked()
		{
			Clicked?.Invoke(this, new ButtonEventArgs(this.ButtonType));
			this.Command?.Execute(this.ButtonType);
		}

		#endregion ctor

		#region IsSelected BindableProperty

		/// <summary>
		/// Get or Sets the IsSelected bindable property. 
		/// </summary>
		public bool IsSelected
		{
			get { return (bool)GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}

		/// <summary>
		/// Identifies the IsSelected bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty IsSelectedProperty =
			BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(TabButton), false, BindingMode.TwoWay, null, OnIsSelectedPropertyChanged);

		/// <summary>
		/// IsSelected changed handler.
		/// </summary>
		/// <param name="bindable">AppButton that changed its name.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnIsSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as TabButton;
			if (element == null)
				return;

			element.BoxView.FillColor = (bool)newValue == true
				? element.SelectedBackgroundColor
				: element.BackgroundColor;
		}

		#endregion IsSelected BindableProperty

		#region ButtonType BindableProperty

		/// <summary>
		/// Get or Sets the ButtonType bindable property. 
		/// </summary>
		public ToolbarButtonType ButtonType
		{
			get { return (ToolbarButtonType) GetValue(ButtonTypeProperty); }
			set { SetValue(ButtonTypeProperty, value); }
		}

		/// <summary>
		/// Identifies the ButtonType bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty ButtonTypeProperty =
			BindableProperty.Create(nameof(ButtonType), typeof(ToolbarButtonType), typeof(TabButton),
				ToolbarButtonType.Undefined, BindingMode.TwoWay);

		#endregion ButtonType Bindable Property

		#region BackgroundColor BindableProperty

		/// <summary>
		/// Get or Sets the BackgroundColor bindable property. 
		/// </summary>
		public new Color BackgroundColor
		{
			get { return (Color)GetValue(BackgroundColorProperty); }
			set { SetValue(BackgroundColorProperty, value); }
		}

		/// <summary>
		/// Identifies the BackgroundColor bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public new static readonly BindableProperty BackgroundColorProperty =
			BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(TabButton),
				Color.Silver, BindingMode.TwoWay, null,
				OnBackgroundColorPropertyChanged);

		/// <summary>
		/// BackgroundColor changed handler.
		/// </summary>
		/// <param name="bindable">TabButton that changed its BackgroundColor property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnBackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as TabButton;
			if (element == null)
				return;

			element.BoxView.FillColor = element.IsSelected ? element.BackgroundColor : (Color)newValue;
		}

		#endregion BackgroundColor Bindable Property

		#region SelectedBackgroundColor BindableProperty

		/// <summary>
		/// Get or Sets the SelectedBackgroundColor bindable property. 
		/// </summary>
		public Color SelectedBackgroundColor
		{
			get { return (Color)GetValue(SelectedBackgroundColorProperty); }
			set { SetValue(SelectedBackgroundColorProperty, value); }
		}

		/// <summary>
		/// Identifies the SelectedBackgroundColor bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty SelectedBackgroundColorProperty =
			BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(TabButton),
				Color.Red, BindingMode.TwoWay, null,
				OnSelectedBackgroundColorPropertyChanged);

		/// <summary>
		/// SelectedBackgroundColor changed handler.
		/// </summary>
		/// <param name="bindable">TabButton that changed its SelectedBackgroundColor property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnSelectedBackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as TabButton;
			if (element == null)
				return;

			element.BoxView.FillColor = element.IsSelected ? (Color)newValue : element.BackgroundColor;
		}

		#endregion SelectedBackgroundColor Bindable Property

		#region Text BindableProperty

		/// <summary>
		/// Get or Sets the Text bindable property. 
		/// </summary>
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		/// <summary>
		/// Identifies the Text bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(TabButton),
				default(string), BindingMode.TwoWay, null,
				OnTextPropertyChanged);

		/// <summary>
		/// Text changed handler.
		/// </summary>
		/// <param name="bindable">TabButton that changed its Text property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as TabButton;
			if (element == null)
				return;

			var value = (string)newValue;
			var buttonText = value.Split('-');
			if (buttonText.Length == 2)
			{
				element.LabelText1.Text = buttonText[0];
				element.LabelText2.Text = buttonText[1];
			}
			else
			{
				element.LabelText1.Text = value;
				element.LabelText2.Text = string.Empty;
				element.LabelText2.IsVisible = false;
			}
		}

		#endregion Text Bindable Property

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
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(TabButton),
			default(ICommand), BindingMode.TwoWay);

		#endregion Command Bindable Property
	}
}
