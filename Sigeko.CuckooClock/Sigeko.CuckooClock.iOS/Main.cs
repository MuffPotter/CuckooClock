using UIKit;

namespace Sigeko.CuckooClock.iOS
{
	/// <summary>
	/// Sigeko.CuckooClock.iOS Application
	/// </summary>
	public class Application
	{
		/// <summary>
		/// This is the main entry point of the application.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			// if you want to use a different Application Delegate class 
			// from "AppDelegate", you can specify it here.
			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}
