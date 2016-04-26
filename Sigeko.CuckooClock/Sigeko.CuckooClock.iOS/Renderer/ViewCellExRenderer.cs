using SWBSales.Controls;
using SWBSales.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCellEx), typeof(ViewCellExRenderer))]

namespace SWBSales.iOS.Renderer
{
	public class ViewCellExRenderer : ViewCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tabelView)
		{
			var cell = base.GetCell(item, reusableCell, tabelView);
			if (cell == null)
				return null;

			// setting up TableView
			tabelView.SeparatorColor = UIColor.Clear;
			tabelView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			tabelView.AlwaysBounceVertical = false;

			// setting up the cell
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			return cell;
		}
	}
}