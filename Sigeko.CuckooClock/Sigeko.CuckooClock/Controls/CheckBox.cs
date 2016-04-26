
using System;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Controls
{
	/// <summary>
	/// The check box.
	/// </summary>
	public class CheckBox : View
	{
		#region CheckedText BindableProperty

		/// <summary>
		/// Get or Sets the CheckedText bindable property. 
		/// </summary>
		public string CheckedText
		{
			get { return (string)GetValue(CheckedTextProperty); }
			set { SetValue(CheckedTextProperty, value); }
		}

		/// <summary>
		/// Identifies the CheckedText bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty CheckedTextProperty =
			BindableProperty.Create(nameof(CheckedText), typeof(string), typeof(CheckBox),
				string.Empty, BindingMode.TwoWay, null,
				OnCheckedTextPropertyChanged);

		/// <summary>
		/// CheckedText changed handler.
		/// </summary>
		/// <param name="bindable">CheckBox that changed its CheckedText property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnCheckedTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as CheckBox;
			if (element == null)
				return;
		}

		#endregion CheckedText Bindable Property

		#region UncheckedText BindableProperty

		/// <summary>
		/// Get or Sets the UncheckedText bindable property. 
		/// </summary>
		public string UncheckedText
		{
			get { return (string)GetValue(UncheckedTextProperty); }
			set { SetValue(UncheckedTextProperty, value); }
		}

		/// <summary>
		/// Identifies the UncheckedText bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty UncheckedTextProperty =
			BindableProperty.Create(nameof(UncheckedText), typeof(string), typeof(CheckBox),
				string.Empty, BindingMode.TwoWay, null,
				OnUncheckedTextPropertyChanged);

		/// <summary>
		/// UncheckedText changed handler.
		/// </summary>
		/// <param name="bindable">CheckBox that changed its UncheckedText property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnUncheckedTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as CheckBox;
			if (element == null)
				return;
		}

		#endregion UncheckedText Bindable Property

		#region DefaultText BindableProperty

		/// <summary>
		/// Get or Sets the DefaultText bindable property. 
		/// </summary>
		public string DefaultText
		{
			get { return (string)GetValue(DefaultTextProperty); }
			set { SetValue(DefaultTextProperty, value); }
		}

		/// <summary>
		/// Identifies the DefaultText bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty DefaultTextProperty =
			BindableProperty.Create(nameof(DefaultText), typeof(string), typeof(CheckBox),
				string.Empty, BindingMode.TwoWay, null,
				OnDefaultTextPropertyChanged);

		/// <summary>
		/// DefaultText changed handler.
		/// </summary>
		/// <param name="bindable">CheckBox that changed its DefaultText property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnDefaultTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as CheckBox;
			if (element == null)
				return;
		}

		#endregion DefaultText Bindable Property

		#region TextColor BindableProperty

		/// <summary>
		/// Get or Sets the TextColor bindable property. 
		/// </summary>
		public Color TextColor
		{
			get { return (Color)GetValue(TextColorProperty); }
			set { SetValue(TextColorProperty, value); }
		}

		/// <summary>
		/// Identifies the TextColor bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CheckBox),
				default(Color), BindingMode.TwoWay, null,
				OnTextColorPropertyChanged);

		/// <summary>
		/// TextColor changed handler.
		/// </summary>
		/// <param name="bindable">CheckBox that changed its TextColor property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as CheckBox;
			if (element == null)
				return;
		}

		#endregion TextColor Bindable Property

		#region FontSize BindableProperty

		/// <summary>
		/// Get or Sets the FontSize bindable property. 
		/// </summary>
		public double FontSize
		{
			get { return (double)GetValue(FontSizeProperty); }
			set { SetValue(FontSizeProperty, value); }
		}

		/// <summary>
		/// Identifies the FontSize bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty FontSizeProperty =
			BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CheckBox),
				default(double), BindingMode.TwoWay, null,
				OnFontSizePropertyChanged);

		/// <summary>
		/// FontSize changed handler.
		/// </summary>
		/// <param name="bindable">CheckBox that changed its FontSize property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnFontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as CheckBox;
			if (element == null)
				return;
		}

		#endregion FontSize Bindable Property

		#region FontName BindableProperty

		/// <summary>
		/// Get or Sets the FontName bindable property. 
		/// </summary>
		public string FontName
		{
			get { return (string)GetValue(FontNameProperty); }
			set { SetValue(FontNameProperty, value); }
		}

		/// <summary>
		/// Identifies the FontName bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty FontNameProperty =
			BindableProperty.Create(nameof(FontName), typeof(string), typeof(CheckBox),
				string.Empty, BindingMode.TwoWay, null,
				OnFontNamePropertyChanged);

		/// <summary>
		/// FontName changed handler.
		/// </summary>
		/// <param name="bindable">CheckBox that changed its FontName property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnFontNamePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as CheckBox;
			if (element == null)
				return;
		}

		#endregion FontName Bindable Property

		#region Checked BindableProperty

		/// <summary>
		/// Get or Sets the Checked bindable property. 
		/// </summary>
		public bool Checked
		{
			get { return (bool)GetValue(CheckedProperty); }
			set
			{
				if (Checked == value)
					return;

				SetValue(CheckedProperty, value);
				var eventArgs = new CheckBoxEventArgs(Checked);
				var onCheckedChanged = CheckedChanged;
				onCheckedChanged?.Invoke(this, eventArgs);
			}
		}

		/// <summary>
		/// Identifies the Checked bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty CheckedProperty =
			BindableProperty.Create(nameof(Checked), typeof(bool), typeof(CheckBox),
				default(bool), BindingMode.TwoWay, null,
				OnCheckedPropertyChanged);

		/// <summary>
		/// Checked changed handler.
		/// </summary>
		/// <param name="bindable">CheckBox that changed its Checked property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as CheckBox;
			if (element == null)
				return;

			element.Checked = (bool)newValue;
		}

		/// <summary>
		/// The checked changed event.
		/// </summary>
		public event EventHandler<CheckBoxEventArgs> CheckedChanged;

		#endregion Checked BindableProperty

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text => Checked
			? (string.IsNullOrEmpty(CheckedText) ? DefaultText : CheckedText)
			: (string.IsNullOrEmpty(UncheckedText) ? DefaultText : UncheckedText);

	}
}