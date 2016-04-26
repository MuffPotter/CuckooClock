// ***********************************************************************
// Assembly         : XLabs.Forms.iOS
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="HyperLinkLabelRenderer.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using Foundation;
using SWBSales.Controls;
using SWBSales.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HyperLinkLabel), typeof(HyperLinkLabelRenderer))]

namespace SWBSales.iOS.Renderer
{
	/// <summary>
	/// Class HyperLinkLabelRenderer.
	/// </summary>
	public class HyperLinkLabelRenderer : LabelRenderer
	{
		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
				return;

			var label = Control;
			label.TextColor = UIColor.Blue;
			label.BackgroundColor = UIColor.Clear;
			label.UserInteractionEnabled = true;
			var tapXamarin = new UITapGestureRecognizer();

			tapXamarin.AddTarget(() =>
			{
				var hyperLinkLabel = Element as HyperLinkLabel;
				if (hyperLinkLabel?.NavigateUri != null)
				{
					var uri = new NSUrl(GetNavigationUri(hyperLinkLabel.NavigateUri));
					UIApplication.SharedApplication.OpenUrl(uri);
				}
			});

			tapXamarin.NumberOfTapsRequired = 1;
			tapXamarin.DelaysTouchesBegan = true;
			label.AddGestureRecognizer(tapXamarin);
		}

		/// <summary>
		/// Gets the navigation URI.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns>System.String.</returns>
		private static string GetNavigationUri(string uri)
		{
			if (uri.Contains("@") && !uri.StartsWith("mailto:"))
			{
				return $"mailto:{uri}";
			}
			if (uri.StartsWith("www.")) 
			{
				return $"http://{uri}";
			}
			return uri;
		}
	}
}