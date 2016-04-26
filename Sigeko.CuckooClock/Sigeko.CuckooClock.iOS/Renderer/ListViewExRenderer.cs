using SWBSales.Controls;
using SWBSales.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListViewEx), typeof(ListViewExRenderer))]

namespace SWBSales.iOS.Renderer
{
    public class ListViewExRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> eventArgs)
        {
            base.OnElementChanged(eventArgs);

            if (Control == null)
                return;

            var tableView = Control as UITableView;
            tableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            tableView.CellLayoutMarginsFollowReadableWidth = false;
        }
    }
}
