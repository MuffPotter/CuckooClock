using System.Threading.Tasks;

namespace Sigeko.CuckooClock.Services
{
	/// <summary>
	/// http://enginecore.blogspot.de/2013/09/a-first-Implementation-for-alert-dialog.html
	/// </summary>
	public interface IDialogServices
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="titel"></param>
		/// <param name="message"></param>
		/// <param name="applyButtonContent"></param>
		/// <param name="cancelButtonContent"></param>
		/// <returns></returns>
		Task<bool?> ShowAsync(string titel, string message, string applyButtonContent, string cancelButtonContent = "");

		/// <summary>
		/// http://developer.xamarin.com/recipes/ios/standard_controls/actionsheet/display_an_actionsheet/
		/// </summary>
		/// <param name="titel"></param>
		/// <param name="cancelButtonContent"></param>
		/// <param name="buttons"></param>
		/// <param name="applyButtonContent"></param>
		/// <returns></returns>
		Task<int> ShowActionAsync(string titel, string applyButtonContent, string cancelButtonContent, string[] buttons);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="titel"></param>
		/// <param name="message"></param>
		void ShowProcess(string titel, string message);

		/// <summary>
		/// 
		/// </summary>
		void HideProcess();

		Task<bool?> SetNotification(string titel, string message);

		void ClearNotifications(object options);

		/// <summary>
		/// https://forums.xamarin.com/discussion/21028/cant-make-the-ios-app-full-screen
		/// </summary>
		/// <param name="canHide"></param>
		void SetStatusBarVisibility(bool canHide = false);

		void HideStatusBar();

		void ShowStatusBar();
	}

}