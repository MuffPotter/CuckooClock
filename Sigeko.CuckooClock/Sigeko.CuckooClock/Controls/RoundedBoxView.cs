using Xamarin.Forms;

namespace Sigeko.CuckooClock.Controls
{
	public class RoundedBoxView : BoxView
	{
		#region UseGradient BindableProperty

		/// <summary>
		/// Get or Sets the UseGradient bindable property. 
		/// </summary>
		public bool UseGradient
		{
			get { return (bool)GetValue(UseGradientProperty); }
			set { SetValue(UseGradientProperty, value); }
		}

		/// <summary>
		/// Identifies the UseGradient bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty UseGradientProperty =
			BindableProperty.Create(nameof(UseGradient), typeof(bool), typeof(RoundedBoxView),
				default(bool), BindingMode.TwoWay);

		#endregion UseGradient Bindable Property

		#region FillColor BindableProperty

		public new Color Color
		{
			get { return Color.Transparent; }
			set { FillColor = value; }
		}

		/// <summary>
		/// Get or Sets the FillColor bindable property. 
		/// </summary>
		public Color FillColor
		{
			get { return (Color)GetValue(FillColorProperty); }
			set { SetValue(FillColorProperty, value); }
		}

		/// <summary>
		/// Identifies the FillColor bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty FillColorProperty =
			BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(RoundedBoxView),
			Color.Transparent, BindingMode.TwoWay);

		#endregion FillColor Bindable Property

		//public static readonly BindableProperty FillColorProperty =
		//	BindableProperty.Create<RoundedBoxView, Color>(p => p.FillColor, Color.Transparent);

		//public Color FillColor
		//{
		//	get { return (Color)GetValue(FillColorProperty); }
		//	set { SetValue(FillColorProperty, value); }
		//}

		#region StartColor BindableProperty

		/// <summary>
		/// Get or Sets the StartColor bindable property. 
		/// </summary>
		public Color StartColor
		{
			get { return (Color)GetValue(StartColorProperty); }
			set { SetValue(StartColorProperty, value); }
		}

		/// <summary>
		/// Identifies the StartColor bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty StartColorProperty =
			BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(RoundedBoxView),
				default(Color), BindingMode.TwoWay);

		#endregion StartColor Bindable Property

		#region MidColor BindableProperty

		/// <summary>
		/// Get or Sets the MidColor bindable property. 
		/// </summary>
		public Color MidColor
		{
			get { return (Color)GetValue(MidColorProperty); }
			set { SetValue(MidColorProperty, value); }
		}

		/// <summary>
		/// Identifies the MidColor bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty MidColorProperty =
			BindableProperty.Create(nameof(MidColor), typeof(Color), typeof(RoundedBoxView),
				default(Color), BindingMode.TwoWay);

		#endregion MidColor Bindable Property

		#region EndColor BindableProperty

		/// <summary>
		/// Get or Sets the EndColor bindable property. 
		/// </summary>
		public Color EndColor
		{
			get { return (Color)GetValue(EndColorProperty); }
			set { SetValue(EndColorProperty, value); }
		}

		/// <summary>
		/// Identifies the EndColor bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty EndColorProperty =
			BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(RoundedBoxView),
				default(Color), BindingMode.TwoWay);

		#endregion EndColor Bindable Property

		#region CornerRadius BindableProperty

		/// <summary>
		/// Get or Sets the CornerRadius bindable property. 
		/// </summary>
		public double CornerRadius
		{
			get { return (double)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}

		/// <summary>
		/// Identifies the CornerRadius bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty CornerRadiusProperty =
			BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(RoundedBoxView),
			15.0, BindingMode.TwoWay);

		#endregion CornerRadius Bindable Property

		#region Stroke BindableProperty

		/// <summary>
		/// Get or Sets the Stroke bindable property. 
		/// </summary>
		public Color Stroke
		{
			get { return (Color)GetValue(StrokeProperty); }
			set { SetValue(StrokeProperty, value); }
		}

		/// <summary>
		/// Identifies the Stroke bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty StrokeProperty =
			BindableProperty.Create(nameof(Stroke), typeof(Color), typeof(RoundedBoxView),
			Color.Transparent, BindingMode.TwoWay);

		#endregion Stroke Bindable Property

		#region StrokeThickness BindableProperty

		/// <summary>
		/// Get or Sets the StrokeThickness bindable property. 
		/// </summary>
		public double StrokeThickness
		{
			get { return (double)GetValue(StrokeThicknessProperty); }
			set { SetValue(StrokeThicknessProperty, value); }
		}

		/// <summary>
		/// Identifies the StrokeThickness bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty StrokeThicknessProperty =
			BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(RoundedBoxView),
				default(double), BindingMode.TwoWay);

		#endregion StrokeThickness Bindable Property

		#region HasShadow BindableProperty

		/// <summary>
		/// Get or Sets the HasShadow bindable property. 
		/// </summary>
		public bool HasShadow
		{
			get { return (bool)GetValue(HasShadowProperty); }
			set { SetValue(HasShadowProperty, value); }
		}

		/// <summary>
		/// Identifies the HasShadow bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty HasShadowProperty =
			BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(RoundedBoxView),
				default(bool), BindingMode.TwoWay);

		#endregion HasShadow Bindable Property
	}
}

