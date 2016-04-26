using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Controls
{
    public partial class AppButton
    {
        public AppButton()
        {
            InitializeComponent();

			this.AppButtonType = AppButtonType.Unkown;
			this.WidthRequest = 120;

	        //this.BoxView.FillColor = this.BackgroundColor;
			this.AddTouchHandler(this, this.OnClick);
			this.LayoutChanged += OnLayoutChanged;
        }

		private void OnLayoutChanged(object sender, EventArgs e)
		{
			AdjustWithAndHeight(sender as AppButton);
		}

		#region FontSize BindableProperty

		/// <summary>
		/// Get or Sets the FontSize bindable property. 
		/// </summary>
		public double FontSize
	    {
		    get { return (double) GetValue(FontSizeProperty); }
		    set { SetValue(FontSizeProperty, value); }
	    }

	    /// <summary>
	    /// Identifies the FontSize bindable property. This enables animation, styling, binding, etc...
	    /// </summary>
	    public static readonly BindableProperty FontSizeProperty =
		    BindableProperty.Create(nameof(FontSize), typeof (double), typeof (AppButton),
			    default(double), BindingMode.TwoWay, null,
			    OnFontSizePropertyChanged);

	    /// <summary>
	    /// FontSize changed handler.
	    /// </summary>
	    /// <param name="bindable">AppButton that changed its FontSize.</param>
	    /// <param name="oldValue">The old value of the property</param>
	    /// <param name="newValue">The new value of the property</param>
	    private static void OnFontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
	    {
		    var element = bindable as AppButton;
		    if (element == null)
			    return;

			AdjustWithAndHeight(element);
			element.ButtonLabel.FontSize = (double)newValue;
		}

		#endregion FontSize Bindable Property

	    #region Text BindableProperty

	    /// <summary>
	    /// Get or Sets the HeaderText bindable property. 
	    /// </summary>
	    public string Text
	    {
		    get { return (string) GetValue(TextProperty); }
		    set { SetValue(TextProperty, value); }
	    }

	    /// <summary>
	    /// Identifies the Text bindable property. This enables animation, styling, binding, etc...
	    /// </summary>
	    public static readonly BindableProperty TextProperty =
		    BindableProperty.Create(nameof(Text), typeof (string), typeof (AppButton),
			    default(string), BindingMode.TwoWay, null, OnTextPropertyChanged);

	    /// <summary>
	    /// Text changed handler.
	    /// </summary>
	    /// <param name="bindable">AppButton that changed its Text.</param>
	    /// <param name="oldValue">The old value of the property</param>
	    /// <param name="newValue">The new value of the property</param>
	    private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	    {
		    var element = bindable as AppButton;
		    if (element == null)
			    return;

			AdjustWithAndHeight(element);

			element.StackPanel.Spacing = 0;
			element.StackPanel.HorizontalOptions = LayoutOptions.CenterAndExpand;
			element.ButtonLabel.Text = (string)newValue;
			element.LeftImage.IsVisible = false;
			element.RightImage.IsVisible = false;
		}

		#endregion Text Bindable Property

		#region AppButtonType BindableProperty

		/// <summary>
		/// Get or Sets the HeaderText bindable property. 
		/// </summary>
		public AppButtonType AppButtonType
		{
		    get { return (AppButtonType) GetValue(AppButtonTypeProperty); }
		    set { SetValue(AppButtonTypeProperty, value); }
	    }

	    /// <summary>
	    /// Identifies the name bindable property. This enables animation, styling, binding, etc...
	    /// </summary>
	    public static readonly BindableProperty AppButtonTypeProperty =
		    BindableProperty.Create(nameof(AppButtonType), typeof (AppButtonType), typeof (AppButton),
				AppButtonType.Default, BindingMode.TwoWay, null, OnAppButtonTypePropertyChanged);

	    /// <summary>
	    /// name changed handler.
	    /// </summary>
	    /// <param name="bindable">AppButton that changed its name.</param>
	    /// <param name="oldValue">The old value of the property</param>
	    /// <param name="newValue">The new value of the property</param>
	    private static void OnAppButtonTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
	    {
		    var element = bindable as AppButton;
		    if (element == null)
			    return;

			AdjustWithAndHeight(element);

		    var buttonType = newValue as AppButtonType? ?? AppButtonType.Unkown;
			switch (buttonType)
			{
				case AppButtonType.Default:
					element.ButtonLabel.Text = element.Text;
					element.LeftImage.IsVisible = false;
					element.RightImage.IsVisible = false;
					break;
				default:
					element.WidthRequest = 150;
					element.ButtonLabel.Text = "Default";
					element.LeftImage.IsVisible = false;
					element.RightImage.IsVisible = false;
					break;
			}
		}

		#endregion AppButtonType Bindable Property

	    #region Command BindableProperty

	    /// <summary>
	    /// Get or Sets the HeaderText bindable property. 
	    /// </summary>
	    public ICommand Command
	    {
		    get { return (ICommand) GetValue(CommandProperty); }
		    set { SetValue(CommandProperty, value); }
	    }

	    /// <summary>
	    /// Identifies the Command bindable property. This enables animation, styling, binding, etc...
	    /// </summary>
	    public static readonly BindableProperty CommandProperty =
		    BindableProperty.Create(nameof(Command), typeof (ICommand), typeof (AppButton),
			    default(ICommand), BindingMode.TwoWay);

	    #endregion Command Bindable Property

	    #region CommandParameter BindableProperty

	    /// <summary>
	    /// Get or Sets the CommandParameter bindable property. 
	    /// </summary>
	    public string CommandParameter
	    {
		    get { return (string) GetValue(CommandParameterProperty); }
		    set { SetValue(CommandParameterProperty, value); }
	    }

	    /// <summary>
	    /// Identifies the CommandParameter bindable property. This enables animation, styling, binding, etc...
	    /// </summary>
	    public static readonly BindableProperty CommandParameterProperty =
		    BindableProperty.Create(nameof(CommandParameter), typeof(string), typeof(AppButton),
			    string.Empty, BindingMode.TwoWay);

	    #endregion CommandParameter Bindable Property

		private static void AdjustWithAndHeight(AppButton element)
		{
			element.WidthRequest = 120;
			if (element.HeightRequest > 0)
			{
				element.MainGrid.HeightRequest = element.HeightRequest;
				element.BoxView.HeightRequest = element.HeightRequest;
				element.StackPanel.HeightRequest = element.HeightRequest;
			}
		}

		public event EventHandler Click;

	    private void OnClick()
	    {
		    Click?.Invoke(this, EventArgs.Empty);
		    this.Command?.Execute(CommandParameter);
	    }

	    protected void AddTouchHandler(View view, Action action)
	    {
		    view.GestureRecognizers.Add(new TapGestureRecognizer
		    {
			    Command = new Command(() =>
			    {
				    view.Opacity = 0.6;
				    view.FadeTo(1);
				    action();
			    })
		    });
	    }
    }

	public enum AppButtonType
	{
		Unkown,

		Default,
	}
}
