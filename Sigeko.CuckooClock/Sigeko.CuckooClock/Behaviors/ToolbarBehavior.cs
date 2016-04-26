using Sigeko.CuckooClock.Models;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Behaviors
{
	/// <summary>
	/// http://www.michaelridland.com/xamarin/xaml-attached-properties-tricks-in-xamarin-forms/
	/// </summary>
	public class ToolbarBehavior
    {
		#region Button1 BindableProperty

		public static ToolbarButton GetButton1(BindableObject obj)
		{
			return (ToolbarButton)obj.GetValue(Button1Property);
		}

		public static void SetButton1(BindableObject obj, ToolbarButton value)
		{
			obj.SetValue(Button1Property, value);
		}

		/// <summary>
		/// Identifies the Button1 bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty Button1Property =
			BindableProperty.Create("Button1", typeof(ToolbarButton), typeof(ToolbarBehavior),
				default(ToolbarButton), BindingMode.TwoWay, null,
				OnButton1PropertyChanged);

		/// <summary>
		/// Button1 changed handler.
		/// </summary>
		/// <param name="bindable">ToolbarBehavior that changed its Button1 property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnButton1PropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var page = bindable as Page;
			if (page == null)
				return;

			var button = (ToolbarButton)newValue;
			if (button != null)
			{
				AddButton(page, button);
			}
			else
			{
				RemoveButton(page, button);
			}
		}

		#endregion Button1 Bindable Property

		#region Button2 BindableProperty

		public static ToolbarButton GetButton2(BindableObject obj)
		{
			return (ToolbarButton)obj.GetValue(Button2Property);
		}

		public static void SetButton2(BindableObject obj, ToolbarButton value)
		{
			obj.SetValue(Button2Property, value);
		}

		/// <summary>
		/// Identifies the Button2 bindable property. This enables animation, styling, binding, etc...
		/// </summary>
		public static readonly BindableProperty Button2Property =
			BindableProperty.Create("Button2", typeof(ToolbarButton), typeof(ToolbarBehavior),
				default(ToolbarButton), BindingMode.TwoWay, null,
				OnButton2PropertyChanged);

		/// <summary>
		/// Button2 changed handler.
		/// </summary>
		/// <param name="bindable">ToolbarBehavior that changed its Button2 property.</param>
		/// <param name="oldValue">The old value of the property</param>
		/// <param name="newValue">The new value of the property</param>
		private static void OnButton2PropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var page = bindable as Page;
			if (page == null)
				return;

			var button = (ToolbarButton)newValue;
			if (button != null)
			{
				AddButton(page, button);
			}
			else
			{
				RemoveButton(page, button);
			}
		}

		#endregion Button2 Bindable Property

		private static void AddButton(Page page, ToolbarButton button)
        {
            page.ToolbarItems.Add(new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                Icon = button.ImageName,
				
                Command = button.Command
            });
        }

        private static void RemoveButton(Page page, ToolbarButton button)
        {
            for (int i = 0; i < page.ToolbarItems.Count; i++)
            {
                var item = page.ToolbarItems[i];
                if (item.Icon == button.ImageName)
                {
                    page.ToolbarItems.Remove(item);
                }
            }
        }
    }
}
