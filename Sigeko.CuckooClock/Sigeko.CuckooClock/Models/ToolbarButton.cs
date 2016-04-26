using System.Windows.Input;

namespace Sigeko.CuckooClock.Models
{
	public class ToolbarButton
	{
		public string ImageName { get; set; }

		public ICommand Command { get; set; }
	}
}
