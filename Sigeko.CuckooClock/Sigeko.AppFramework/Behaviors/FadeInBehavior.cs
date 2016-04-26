using Xamarin.Forms;

namespace Sigeko.AppFramework.Behaviors
{
    public class FadeInBehavior
	{
		#region FadeIn BindableProperty

		public static bool GetFadeIn(BindableObject obj)
		{
			return (bool)obj.GetValue(FadeInProperty);
		}

		public static void SetFadeIn(BindableObject obj, string value)
		{
			obj.SetValue(FadeInProperty, value);
		}

		/// <summary>
		/// Identifies the FadeIn bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty FadeInProperty =
		    BindableProperty.Create("FadeIn", typeof(bool), typeof(FadeInBehavior),
			    default(bool), BindingMode.TwoWay, null,
			    OnFadeInPropertyChanged);

	    /// <summary>
	    /// FadeIn changed handler.
	    /// </summary>
	    /// <param name="bindable">FadeInBehavior that changed its FadeIn property.</param>
	    /// <param name="oldValue">The old value of the property</param>
	    /// <param name="newValue">The new value of the property</param>
	    private static void OnFadeInPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	    {
			var view = bindable as View;
			if (view == null)
				return;

			view.Opacity = 0.0;

			if ((bool)newValue == true)
			{
				view.FadeTo(1.0, 2000, Easing.Linear);
			}
		}

	    #endregion FadeIn Bindable Property
	}
}
