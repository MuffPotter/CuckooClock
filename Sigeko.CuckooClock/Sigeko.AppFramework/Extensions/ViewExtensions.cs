using System;
using Xamarin.Forms;

namespace Sigeko.AppFramework.Extensions
{
	public static class ViewExtensions
	{
		public static void AddTouchHandler(this Layout<View> view, Action action = null)
		{
			view.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command(() =>
				{
					view.Opacity = 0.5;
					view.FadeTo(1);
					action?.Invoke();
				})
			});
		}
	}
}