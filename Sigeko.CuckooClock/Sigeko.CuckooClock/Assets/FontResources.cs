using Xamarin.Forms;

namespace Sigeko.CuckooClock.Assets
{
	public static class FontResources
	{
		// Schriften:	TheSans, Sansa-Pro, Verdana
		// TheSans.		Semi-Light- Plain, Plain, Semi-Bold Plain, Bold Plain
		// Windows:		
		// iOS:
		//	-Verdana:	Verdana-Italic, Verdana-BoldItalic, Verdana, Verdana-Bold

		// Family Name 
		public static readonly string MainFont = "AppleSDGothicNeo-Regular";
		public static readonly string MainFontBold = "AppleSDGothicNeo-Bold";
		public static readonly string FontFamilyName = Device.OnPlatform("AppleSDGothicNeo-Regular", "", "");

		public static readonly double PageHeaderFontSize = 24;

		// BranchBar
		public static readonly double ButtonFontSize = 16;
		public static readonly double ButtonFontSizeSmall = 15;

		public static readonly double CheckBoxFontSize = 15;

		public static readonly double LabelFontSize = 18;
		public static readonly double LabelFontSizeSmall = 14;

		public static readonly double RoundImageFontSize = 24;

		public static readonly double PopUpHeaderFontSize = 32;
		public static readonly double PopUpCloseButtonFontSize = 28;
	}
}