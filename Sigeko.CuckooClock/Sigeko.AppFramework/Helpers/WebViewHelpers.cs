using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace Sigeko.AppFramework.Helpers
{
	public static class WebViewHelpers
	{
		public static void ShowLocalPage(WebView view, string htmlFile)
		{
			if (string.IsNullOrEmpty(htmlFile) == true)
			{
				view.Source = null;
				return;
			}

			var uriSource = new UrlWebViewSource
			{
				Url = htmlFile
			};
			view.Source = uriSource;
		}

		public static void ShowLocalHtmlPage(WebView view, string htmlFile)
		{
			var htmlSource = new HtmlWebViewSource
			{
				Html = htmlFile,
			};

			view.Source = htmlSource;
		}

		public static void ShowLocalPage(WebView view, string resourceUri, Assembly assembly)
		{
			var stream = assembly.GetManifestResourceStream(resourceUri);
			var reader = new StreamReader(stream);
			var htmlString = reader.ReadToEnd();
			ShowLocalHtmlPage(view, htmlString);
		}

		public static void ShowPdfFile(WebView view, string pdfFile)
		{
			if (string.IsNullOrEmpty(pdfFile) == true)
			{
				view.Source = null;
				return;
			}

			var uriSource = new UrlWebViewSource
			{
				Url = pdfFile
			};
			view.Source = uriSource;
		}
	}
}
