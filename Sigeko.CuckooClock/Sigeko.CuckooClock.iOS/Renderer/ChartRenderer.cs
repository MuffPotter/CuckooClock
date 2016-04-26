using System.Linq;
using SWBSales.Controls.Chart;
using SWBSales.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Chart), typeof(ChartRenderer))]

namespace SWBSales.iOS.Renderer
{
	/// <summary>
	/// Class ChartRenderer.
	/// https://github.com/XLabs/Xamarin-Forms-Labs/tree/master/src/Forms/Charting
	/// </summary>
	public class ChartRenderer : ViewRenderer<Chart, ChartSurface>
	{
		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Chart> e)
		{
			base.OnElementChanged(e);
			if (e.OldElement != null || Element == null)
			{
				return;
			}

			// Use color specified at DataPoints if it is a Pie Chart
			UIColor[] colors;

			var pieSeries = Element.Series.FirstOrDefault(s => s.Type == ChartType.Pie);

			if (pieSeries != null)
			{
				colors = new UIColor[pieSeries.Points.Count];
				for (int i = 0; i < pieSeries.Points.Count; i++)
				{
					colors[i] = pieSeries.Points[i].Color.ToUIColor();
				}
			}
			else
			{
				colors = new UIColor[Element.Series.Count];
				for (var i = 0; i < Element.Series.Count; i++)
				{
					colors[i] = Element.Series[i].Color.ToUIColor();
				}
			}

			var surfaceView = new ChartSurface(Element, Element.Color.ToUIColor(), colors);
			SetNativeControl(surfaceView);
		}
	}
}
