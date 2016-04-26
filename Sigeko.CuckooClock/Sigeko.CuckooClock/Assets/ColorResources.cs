using Xamarin.Forms;

namespace Sigeko.CuckooClock.Assets
{
	public static class ColorResources
	{
		// Opaque:
		// 100% — FF, 95% — F2, 90% — E6, 85% — D9, 80% — CC, 75% — BF, 70% — B3
		// 65% — A6, 60% — 99, 55% — 8C, 50% — 80, 45% — 73, 40% — 66, 35% — 59
		// 30% — 4D, 25% — 40, 20% — 33, 15% — 26, 10% — 1A, 5% — 0D, 0% — 00

		// Yellow:	FFD300
		// Gray:	666666 / CDCDCD		

		// Rot:		75% = , 50% = , 25% =
		public static Color MainYellow = Color.FromHex("#FFFFD300");
		public static Color SelectedYellow = Color.FromHex("#80FFD300");
		public static Color Yellow25 = Color.FromHex("#40FFD300");
		public static Color Yellow05 = Color.FromHex("#0DFFD300");

		// Grau:	75% = , 50% = , 25% =
		public static Color MainGray = Color.FromHex("#FF666666");
		public static Color LightGray = Color.FromHex("#FFCDCDCD");

		// Schwarz:
		public static Color Black = Color.Black;

		// Farbverlauf:
		//	Alt:	StartColor = #A8CE38, MidColor = Yellow, EndColor = #EE2025
		public static Color StartColor = Color.FromHex("#FF62B32C");
		public static Color MidColor = Color.Yellow;
		public static Color EndColor = MainYellow;

		// Theme Colors
		public static Color ViewBackgroundColor = Yellow05;
		public static Color NavigationBarTextColor = MainGray;

		public static Color ToolbarBackgroundColor = MainYellow;
		public static Color ToolbarTextColor = MainGray;

		public static Color SwitchOnColor = Yellow25;
		public static Color SwitchColor = MainYellow;
		public static Color SwitchTintColor = LightGray;

		public static Color LabelTextColor = MainGray;

		public static Color ButtonBackgroundColor = MainYellow;
		public static Color ButtonTextColor = MainGray;
		//public static Color ButtonBackgroundColor = Color.White;
		//public static Color ButtonTextColor = MainYellow;
	}
}