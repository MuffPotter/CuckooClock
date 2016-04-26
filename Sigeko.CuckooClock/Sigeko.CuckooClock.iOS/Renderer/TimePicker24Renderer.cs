using Foundation;
using Sigeko.CuckooClock.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TimePicker), typeof(TimePicker24Renderer))]

namespace Sigeko.CuckooClock.iOS.Renderer
{
	/// <summary>
	/// http://hjerpbakk.com/blog/2014/7/16/xamarinforms-24-hour-timepicker-on-ios
	/// http://hjerpbakk.com/blog/2014/8/8/xamarinforms-app-store-rejection?rq=timepicker
	/// https://forums.xamarin.com/discussion/19657/timepicker-with-24-hour-format-dialog
	/// </summary>
	public class TimePicker24Renderer : TimePickerRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> eventArgs)
		{
			base.OnElementChanged(eventArgs);

			if (Control == null)
				return;

			var timePicker = (UIDatePicker)Control.InputView;
			timePicker.Locale = new NSLocale("no_nb");
		}
	}
}
