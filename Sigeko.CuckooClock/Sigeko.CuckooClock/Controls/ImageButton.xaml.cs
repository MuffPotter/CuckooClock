using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Controls
{
    public partial class ImageButton
    {
        public ImageButton()
        {
            InitializeComponent();

	        this.WidthRequest = 340;
			this.BindingContextChanged += ImageButtonBindingContextChanged;

			this.AddTouchHandler(this, this.OnClick);
        }

		private void ImageButtonBindingContextChanged(object sender, EventArgs eventArgs)
		{
			var element = sender as ImageButton;
			if (element == null)
				return;

			var bindingContext = this.BindingContext as Models.MenuItem;
			if (bindingContext == null)
				return;

			this.Text = bindingContext.DisplayName;
			this.LabelText.Text = bindingContext.DisplayName;
			this.Image.Source = bindingContext.ImageName;
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
		    BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ImageButton),
			    default(double), BindingMode.TwoWay, null, OnFontSizePropertyChanged);

	    /// <summary>
	    /// FontSize changed handler.
	    /// </summary>
	    /// <param name="bindable">ImageButton that changed its FontSize property.</param>
	    /// <param name="oldValue">The old value of the property</param>
	    /// <param name="newValue">The new value of the property</param>
	    private static void OnFontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
	    {
			var element = bindable as ImageButton;
			if (element == null)
				return;

			element.AdjustWithAndHeight();
			element.LabelText.FontSize = (double)newValue;
		}

	    #endregion FontSize Bindable Property

	    #region Text BindableProperty

	    /// <summary>
	    /// Get or Sets the Text bindable property. 
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
		    BindableProperty.Create(nameof(Text), typeof(string), typeof(ImageButton),
			    default(string), BindingMode.TwoWay, null, OnTextPropertyChanged);

	    /// <summary>
	    /// Text changed handler.
	    /// </summary>
	    /// <param name="bindable">ImageButton that changed its Text property.</param>
	    /// <param name="oldValue">The old value of the property</param>
	    /// <param name="newValue">The new value of the property</param>
	    private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	    {
			var element = bindable as ImageButton;
		    element?.AdjustWithAndHeight();
		}

	    #endregion Text Bindable Property

	    #region ImageButtonType BindableProperty

	    /// <summary>
	    /// Get or Sets the ImageButtonType bindable property. 
	    /// </summary>
	    public ImageButtonType ImageButtonType
	    {
		    get { return (ImageButtonType) GetValue(ImageButtonTypeProperty); }
		    set { SetValue(ImageButtonTypeProperty, value); }
	    }

	    /// <summary>
	    /// Identifies the ImageButtonType bindable property. This enables animation, styling, binding, etc...
	    /// </summary>
	    public static readonly BindableProperty ImageButtonTypeProperty =
		    BindableProperty.Create(nameof(ImageButtonType), typeof(ImageButtonType), typeof(ImageButton),
			    default(ImageButtonType), BindingMode.TwoWay, null, OnImageButtonTypePropertyChanged);

	    /// <summary>
	    /// ImageButtonType changed handler.
	    /// </summary>
	    /// <param name="bindable">ImageButton that changed its ImageButtonType property.</param>
	    /// <param name="oldValue">The old value of the property</param>
	    /// <param name="newValue">The new value of the property</param>
	    private static void OnImageButtonTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
	    {
			var element = bindable as ImageButton;
			if (element == null)
				return;

			element.AdjustWithAndHeight();

			var source = new FileImageSource();
			switch ((ImageButtonType)newValue)
			{
				default:
					source.File = string.Empty;
					element.LabelText.Text = "Default";
					break;
			}

			element.Image.Source = source;
		}


		private void AdjustWithAndHeight()
		{
			//element.Image.HeightRequest = element.HeightRequest;
			this.Image.SizeChanged += OnImageSizeChanged;
		}

		/// <summary>
		/// https://forums.xamarin.com/discussion/32974/how-to-get-an-original-image-size
		/// https://gist.github.com/rudyryk/6d2fb109d2df6873e207
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		private void OnImageSizeChanged(object sender, EventArgs eventArgs)
		{
			// Forcing Layout
			ForceLayout();

			this.HeightRequest = this.Image.HeightRequest;
		}

		#endregion ImageButtonType Bindable Property

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
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ImageButton	),
		    default(ICommand), BindingMode.TwoWay);

		#endregion Command Bindable Property

	    public event EventHandler Click;

	    private void OnClick()
	    {
		    Click?.Invoke(this, EventArgs.Empty);
		    this.Command?.Execute(this.ImageButtonType);
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

	public enum ImageButtonType
	{
		Unkown,
	}
}
