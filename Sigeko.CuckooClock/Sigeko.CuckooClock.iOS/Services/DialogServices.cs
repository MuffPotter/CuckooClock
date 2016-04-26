using System;
using System.Threading.Tasks;
using AudioToolbox;
using BigTed;
using Foundation;
using Sigeko.CuckooClock.Assets;
using Sigeko.CuckooClock.Common;
using Sigeko.CuckooClock.iOS.Extensions;
using Sigeko.CuckooClock.Services;
using UIKit;

namespace Sigeko.CuckooClock.iOS.Services
{
	/// <summary>
	/// Dialog services
	/// </summary>
	public class DialogServices : IDialogServices
	{
		#region Dialogs (UIAlertView)

		/// <summary>
		/// http://forums.xamarin.com/discussion/8519/yes-no-confirmation-uialertview
		/// </summary>
		/// <param name="titel"></param>
		/// <param name="message"></param>
		/// <param name="applyButtonContent"></param>
		/// <param name="cancelButtonContent"></param>
		/// <returns></returns>
		public Task<bool?> ShowAsync(string titel, string message, string applyButtonContent, string cancelButtonContent)
		{
			var tcs = new TaskCompletionSource<bool?>();

			UIApplication.SharedApplication.InvokeOnMainThread(() =>
			{
				var alert = new UIAlertView(
					string.IsNullOrEmpty(titel) ? null : titel,
					string.IsNullOrEmpty(message) ? null : message,
					null,
					string.IsNullOrEmpty(cancelButtonContent) ? null : cancelButtonContent,
					string.IsNullOrEmpty(applyButtonContent) ? null : applyButtonContent);

				var textField = alert.GetTextField(0);
				textField?.UpdateFont(FontResources.FontFamilyName);
				textField = alert.GetTextField(1);
				textField?.UpdateFont(FontResources.FontFamilyName);

				foreach (var subview in alert.Subviews)
				{
					(subview as UITextField)?.UpdateFont(FontResources.FontFamilyName);
				}
				alert.Dismissed += (sender, buttonArgs) =>
				{
					tcs.SetResult(buttonArgs.ButtonIndex != alert.CancelButtonIndex);
				};

				alert.Show();
			});

			return tcs.Task;
		}

		#endregion Dialogs (UIAlertView)

		#region Dialogs (UIActionSheet)

		/// <summary>
		/// http://developer.xamarin.com/recipes/ios/standard_controls/actionsheet/display_an_actionsheet/
		/// https://developer.apple.com/library/ios/documentation/UIKit/Reference/UIActionSheet_Class/
		/// </summary>
		/// <param name="titel"></param>
		/// <param name="cancelButtonContent"></param>
		/// <param name="buttons"></param>
		/// <param name="applyButtonContent"></param>
		/// <returns></returns>
		public Task<int> ShowActionAsync(string titel, string applyButtonContent, string cancelButtonContent, string[] buttons)
		{
			var tcs = new TaskCompletionSource<int>();

			UIApplication.SharedApplication.InvokeOnMainThread(() =>
			{
				var alert = new UIActionSheet(
					string.IsNullOrEmpty(titel) ? null : titel,
					null,
					string.IsNullOrEmpty(cancelButtonContent) ? null : cancelButtonContent,
					string.IsNullOrEmpty(applyButtonContent) ? null : applyButtonContent,
					buttons)
				{
					//DestructiveButtonIndex = -1,
					//CancelButtonIndex = 2
				};

				alert.Dismissed += (sender, buttonArgs) =>
				{
					tcs.SetResult((int)buttonArgs.ButtonIndex);
				};

				alert.ShowInView(UIApplication.SharedApplication.KeyWindow.RootViewController.View);
			});

			return tcs.Task;
		}
		#endregion Dialogs (UIActionSheet)

		#region Process (BTProgressHUD)

		public void ShowProcess(string titel, string message)
		{
			if (string.IsNullOrEmpty(titel) == false && string.IsNullOrEmpty(message) == false)
			{
				message = titel + "\r\n" + message;
			}
			else
			{
				message = titel + message;
			}

			BTProgressHUD.Show(message, maskType: ProgressHUD.MaskType.Gradient);
		}

		public void HideProcess()
		{
			BTProgressHUD.Dismiss();
		}

		#endregion Process (BTProgressHUD)

		#region Notifications (UILocalNotification)

		public Task<bool?> SetNotification(string titel, string message)
		{
			// Sets the retun value for the async method
			var tcs = new TaskCompletionSource<bool?>();

			try
			{
				var notification = new UILocalNotification
				{
					FireDate = (NSDate)DateTime.Now.AddSeconds(1),
					AlertAction = titel,
					AlertBody = message,
					ApplicationIconBadgeNumber = 0,
					SoundName = UILocalNotification.DefaultSoundName,
				};

				UIApplication.SharedApplication.ScheduleLocalNotification(notification);

				UIApplication.SharedApplication.InvokeOnMainThread(() =>
				{
					SystemSound.Vibrate.PlayAlertSound();
					SystemSound.Vibrate.PlaySystemSound();
				});

				UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
				notification.ApplicationIconBadgeNumber = 0;

				tcs.SetResult(true);
			}
			catch (Exception exception)
			{
				tcs.SetResult(false);
				Logger.Current.LogException(exception);
			}

			return tcs.Task;
		}

		public void ClearNotifications(object options)
		{
			var notificationOptions = options as NSDictionary;
			if (notificationOptions == null)
				return;

			// check for a local notification
			var notificationKey = UIApplication.LaunchOptionsLocalNotificationKey;

			if (notificationOptions.ContainsKey(notificationKey))
			{
				var localNotification = notificationOptions[notificationKey] as UILocalNotification;
				if (localNotification != null)
				{
					new UIAlertView(localNotification.AlertAction, localNotification.AlertBody, null, "OK", null).Show();
					// reset our badge
					UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
				}
			}

			// check for a remote notification
			notificationKey = UIApplication.LaunchOptionsRemoteNotificationKey;

			if (notificationOptions.ContainsKey(notificationKey))
			{
				var remoteNotification = notificationOptions[notificationKey] as NSDictionary;
				if (remoteNotification != null)
				{
					//new UIAlertView(remoteNotification.AlertAction, remoteNotification.AlertBody, null, "OK", null).Show();
				}
			}

			// TODO: check
			// http://stackoverflow.com/questions/23628709/how-to-get-the-badge-value-of-current-installation-in-parse-com-for-ios
		}

		#endregion Notifications (UILocalNotification)

		#region StatusBar

		// http://forums.xamarin.com/discussion/comment/57477/#Comment_57477

		public void SetStatusBarVisibility(bool canHide = false)
		{
			if (canHide == false)
				ShowStatusBar();
			else
				HideStatusBar();
		}

		public void HideStatusBar()
		{
			UIApplication.SharedApplication.StatusBarHidden = true;
			//UIApplication.SharedApplication.SetStatusBarHidden(true, true);
		}

		public void ShowStatusBar()
		{
			UIApplication.SharedApplication.StatusBarHidden = false;
			//UIApplication.SharedApplication.SetStatusBarHidden(false, true);
		}

		#endregion StatusBar
	}
}