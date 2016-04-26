using System;
using System.IO;
using UIKit;

namespace Sigeko.CuckooClock.iOS.Extensions
{
	public static class UiExtensions
	{
		public static void UpdateFont(this UITextField control, string familyName)
		{
			control.UpdateFont(familyName, control.Font.PointSize);
		}

		public static void UpdateFont(this UITextField control, string familyName, nfloat pointSize)
		{
			if (string.IsNullOrEmpty(familyName) == true)
				return;

			familyName = Path.GetFileNameWithoutExtension(familyName);
			if (control.Font.FamilyName == familyName)
				return;

			UIFont font = UIFont.FromName(familyName, pointSize);
			if (font != null)
			{
				control.Font = font;
			}
		}
	}
}