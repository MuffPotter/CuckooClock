using System;
using Xamarin.Forms;

namespace Sigeko.AppFramework.Helpers
{
    public class TouchHelpers
    {
        public static void AddTouchHandler(View view, Action action)
        {
            view.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(() =>
                    {
                        view.Opacity = 0.6;
                        view.FadeTo(1);
                        action();
                    })
                });
        }
    }
}
