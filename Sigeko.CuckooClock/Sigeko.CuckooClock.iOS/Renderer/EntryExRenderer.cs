using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sigeko.CuckooClock.Assets;
using UIKit;

using Sigeko.CuckooClock.Controls;
using Sigeko.CuckooClock.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

// Renderer dem Framework bekannt machen
[assembly: ExportRenderer(typeof(EntryEx), typeof(EntryExRenderer))]

namespace Sigeko.CuckooClock.iOS.Renderer
{
	/// <summary>
	/// Custom renderer for Entry (iOS) --> UITextField
	/// https://forums.xamarin.com/discussion/22438/trying-to-make-next-and-prev-buttons-on-keyboard-through-custom-renderer-any-suggestions
	/// </summary>
	public class EntryExRenderer : EntryRenderer
	{
		IEnumerable<EntryEx> _entries = new List<EntryEx>();

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> eventArgs)
		{
			// Basisimplementierung aufrufen
			base.OnElementChanged(eventArgs);

			// Raus, wenn nicht nötig, RemovePage (!!!)
			if (Element == null)
				return;

			// Instantiate the native control and assign it to the Control property
			if (Control == null)
			{
				//SetNativeControl(tbd);
			}

			// Unsubscribe from event handlers and cleanup any resources
			if (eventArgs.OldElement != null)
			{
				if (Control != null)
					Control.ShouldReturn -= OnShouldReturn;
			}

			if (eventArgs.NewElement != null)
			{
				// Get the Xamarin Forms Control
				var xamarinView = eventArgs.NewElement as EntryEx;
				if (xamarinView == null)
					return;

				var textField = Control;
				if (textField == null)
					return;

				var parent = xamarinView.Parent.Parent as IViewContainer<View>;
				if (parent != null)
				{
					_entries = GetChildren<EntryEx>(parent);
				}

				textField.ShouldReturn += OnShouldReturn;

				// http://developer.xamarin.com/recipes/cross-platform/xamarin-forms/add-done-to-keyboard/
				if(Control == null)
					return;

				var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, (float)Control.Frame.Size.Width, 44.0f))
				{
					Items = new[]
					{
						new UIBarButtonItem("<", UIBarButtonItemStyle.Done, HandleNext),
						new UIBarButtonItem(">", UIBarButtonItemStyle.Done, HandleNext),
						new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
						new UIBarButtonItem("Fertig", UIBarButtonItemStyle.Done, OnClicked),
					},

					Translucent = false,

					//BackgroundColor = UIColor.FromRGB(232, 59, 54),
					BarTintColor = ColorResources.ToolbarBackgroundColor.ToUIColor(),
					TintColor = ColorResources.ToolbarTextColor.ToUIColor()
				};

				Control.InputAccessoryView = toolbar;
			}
		}

		private static IEnumerable<T> GetChildren<T>(IViewContainer<View> element) where T : class
		{
			var children = new List<T>();
			foreach (var child in element.Children)
			{
				var view = child as IViewContainer<View>;
				if (view != null)
				{
					children.AddRange(GetChildren<T>(view));
				}
				else if (child is T)
				{
					children.Add(child as T);
				}
			}
			return children;
		}

		private void HandleNext(object sender, EventArgs eventArgs)
		{
			if (_entries == null)
				return;

			var element = _entries.FirstOrDefault(l => l.FocusElementName == "NextElement");
			element?.Focus();
		}

		private bool OnShouldReturn(UITextField textField)
		{
			var xamarinView = Element as EntryEx;
			if (xamarinView == null)
				return true;

			xamarinView.ExecuteCommand?.Execute(null);

			Control.ResignFirstResponder();
			return true;
		}

		private void OnClicked(object sender, EventArgs eventArgs)
		{
			var xamarinView = Element as EntryEx;
			if (xamarinView == null)
				return;

			xamarinView.ExecuteCommand?.Execute(null);
			Control.ResignFirstResponder();
		}
	}
}