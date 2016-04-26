namespace Sigeko.CuckooClock.Assets
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// In Xamarin forms the way images work is you need to put them 
	/// in the native project for each platform.
	/// Android /Resources/Drawable
	/// iOS /Resources
	/// Windows / (just the root folder, yes its annoying but the only place it works)
	/// 
	/// In WP8SL or WP8.1SL you put the largest image you need an it will down scale
	/// In WinRT or UWP you put in the images with.scale-140 etc to scale the images as needed
	/// In iOS you put @2x and @3x at the end of the image name for the larger scales
	/// In Android you put the appropriate images in the various drawable folders e.g.xxhdpi for each size you need.
	/// 
	/// If you don't have the exact required image it always falls back to the default, 
	/// which is just an images without @2x or scale-140 or not in the designated folder 
	/// in the Resources/Drawable etc.
	/// 
	/// With that you can reference the images anywhere in Xamarin Forms by just 
	/// specifying its name 
	/// Image.Source = "image.png";
	/// The platform takes care of the rest.
	/// </remarks>
	public static class Images
    {
		// Sizes:
		// http://www.idev101.com/code/User_Interface/sizes.html
		// http://ivomynttinen.com/blog/the-ios-7-design-cheat-sheet/
		// https://designcode.io/iosdesign-guidelines
		// https://developer.apple.com/library/ios/documentation/UserExperience/Conceptual/MobileHIG/IconMatrix.html

		// iPhone 4S:		320 pts x 480 pts, Scale = 2
		// iPhone 5:		320 pts x 568 pts, Scale = 2
		// iPhone 4S:		375 pts x 667 pts, Scale = 2
		// iPhone 4S:		414 pts x 768 pts, Scale = 2

		// StatusBar:		20 pts
		// NavigationBar:	44 pts
		// https://www.iconfinder.com/icons/383079/arrow_double_right_icon#size=128

		private const string RootPath = "Images/";

		private const string RootPathIcons = RootPath + "Icons/";
		public const string Edit = RootPathIcons + "Edit22.png";
		public const string Delete = RootPathIcons + "Delete22.png";

		private const string RootPathPages = RootPath + "Pages/";
		public const string Bluetooth = RootPathPages + "Bluetooth.png";
		public const string Alarm = RootPathPages + "Alarm.png";

		private const string RootPathButtons = RootPath + "Buttons/";
		public const string ButtonArrowUp = RootPathButtons + "ButtonArrowUp.png";
		public const string ButtonArrowRight = RootPathButtons + "ButtonArrowRight.png";
		public const string ButtonArrowDown = RootPathButtons + "ButtonArrowDown.png";
	}
}
