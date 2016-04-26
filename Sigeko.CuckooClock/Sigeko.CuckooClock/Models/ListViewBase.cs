using System.Windows.Input;
using Newtonsoft.Json;
using Sigeko.AppFramework.Models;

namespace Sigeko.CuckooClock.Models
{
	public class ListViewBase : ModelBase
	{
		[JsonIgnore]
		public ICommand SelectCommand { get; set; }

		[JsonIgnore]
		public ICommand DeleteCommand { get; set; }
	}
}