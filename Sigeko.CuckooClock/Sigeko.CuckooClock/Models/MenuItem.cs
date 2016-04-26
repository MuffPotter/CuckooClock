namespace Sigeko.CuckooClock.Models
{
	public class MenuItem
	{
		public MenuItem(int id, string displayName, string imageName)
		{
			this.Id = id;

			this.DisplayName = displayName;

			this.ImageName = imageName;
		}

		public int Id { get; set; }

		public string DisplayName { get; set; }

		public string ImageName { get; set; }
	}
}