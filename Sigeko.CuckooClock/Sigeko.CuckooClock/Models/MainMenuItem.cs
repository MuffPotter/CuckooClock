using System.Collections.Generic;

namespace Sigeko.CuckooClock.Models
{
    public class MainMenuItem : MenuItem
    {
        public MainMenuItem(int id, string displayName, string imageName) 
			: base(id, displayName, imageName)
        {
            this.Children = new MainMenuItemCollection();
        }

        public MainMenuItem(int id, string displayName, string imageName, MainMenuItemCollection children)
            : this(id, displayName, imageName)
        {
            this.Children = children;
        }

		public MainMenuItemCollection Children { get; private set; }
    }

    public class MainMenuItemCollection : List<MainMenuItem> { }

	public class UnternehmenMenuItemCollection : List<MenuItem> { }
}
