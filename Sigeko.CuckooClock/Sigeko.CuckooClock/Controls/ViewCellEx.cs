using Xamarin.Forms;

namespace Sigeko.CuckooClock.Controls
{
	/// <summary>
	/// For custom renderer
	/// </summary>
	public class ViewCellEx : ViewCell
	{
		public ViewCellEx()
		{
			Tapped += OnTapped;
		}

		private void OnTapped(object sender, System.EventArgs eventArgs)
		{
			//var viewCell = sender as ViewCellEx;
			//viewCell?.View?.FadeTo(0, 500);
		}
	}
}
