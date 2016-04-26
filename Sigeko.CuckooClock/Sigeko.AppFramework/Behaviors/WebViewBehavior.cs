using Sigeko.AppFramework.Helpers;
using Xamarin.Forms;

namespace Sigeko.AppFramework.Behaviors
{
    public class WebViewBehavior
    {
		#region PdfUri BindableProperty

		/// <summary>
		/// Get the PdfUri bindable property. 
		/// </summary>
		public static string GetPdfUri(BindableObject obj)
		{
			return (string)obj.GetValue(PdfUriProperty);
		}

		/// <summary>
		/// Sets the PdfUri bindable property. 
		/// </summary>
		public static void SetPdfUri(BindableObject obj, string value)
		{
			obj.SetValue(PdfUriProperty, value);
		}

		/// <summary>
		/// Identifies the PdfUri bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty PdfUriProperty =
		    BindableProperty.Create("PdfUri", typeof(string), typeof(WebViewBehavior),
			    default(string), BindingMode.TwoWay, null,
			    OnPdfUriPropertyChanged);

	    /// <summary>
	    /// PdfUri changed handler.
	    /// </summary>
	    /// <param name="bindable">WebViewBehavior that changed its PdfUri property.</param>
	    /// <param name="oldValue">The old value of the property</param>
	    /// <param name="newValue">The new value of the property</param>
	    private static void OnPdfUriPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	    {
			var view = bindable as WebView;
			if (view == null)
				return;

			if (string.IsNullOrEmpty((string)newValue) == false)
				WebViewHelpers.ShowPdfFile(view, (string)newValue);
		}

		#endregion PdfUri Bindable Property

		#region ResourceUri Bindable Property

		public static string GetResourceUri(BindableObject obj)
        {
            return (string)obj.GetValue(ResourceUriProperty);
        }

        public static void SetResourceUri(BindableObject obj, string value)
        {
            obj.SetValue(ResourceUriProperty, value);
        }

		/// <summary>
		/// Identifies the ResourceUri bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty ResourceUriProperty =
			BindableProperty.Create("ResourceUri", typeof(string), typeof(WebViewBehavior),
				default(string), BindingMode.TwoWay, null, OnResourceUriPropertyChanged);

		/// <summary>
		/// ResourceUri changed handler.
		/// </summary>
		/// <param name="bindable">WebViewBehavior that changed its ResourceUri property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnResourceUriPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var view = bindable as WebView;
			if (view == null)
				return;

			if (string.IsNullOrEmpty((string)newValue) == false)
				WebViewHelpers.ShowLocalPage(view, (string)newValue);
		}

		#endregion ResourceUri Bindable Property
	}
}
