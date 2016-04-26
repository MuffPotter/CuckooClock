// ***********************************************************************
// Assembly         : XLabs.Forms
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="HyperLinkLabel.cs" company="XLabs Team">
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

using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sigeko.CuckooClock.Controls
{
	/// <summary>
	/// Class HyperLinkLabel.
	/// </summary>
	public class HyperLinkLabel : Label
	{
		/// <summary>
		/// The subject property
		/// </summary>
		public static readonly BindableProperty SubjectProperty = BindableProperty.Create("Subject", typeof(string),
			typeof(HyperLinkLabel), string.Empty, BindingMode.OneWay, null, null, null, null);

		/// <summary>
		/// The navigate URI property
		/// </summary>
		public static readonly BindableProperty NavigateUriProperty = BindableProperty.Create("NavigateUri", typeof(string),
			typeof(HyperLinkLabel), string.Empty, BindingMode.OneWay, null, null, null, null);

		/// <summary>
		/// The navigate command property
		/// </summary>
		public static readonly BindableProperty NavigateCommandProperty = BindableProperty.Create("NavigateCommand", typeof(ICommand),
			typeof(HyperLinkLabel), null, BindingMode.OneWay, null, null, null, null);

		private TapGestureRecognizer _tapGestureRecognizer;

		/// <summary>
		/// Initializes static members of the <see cref="HyperLinkLabel" /> class.
		/// </summary>
		static HyperLinkLabel()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HyperLinkLabel"/> class.
		/// </summary>
		public HyperLinkLabel()
		{
			NavigateCommand = new Command(() =>
			{
				if (string.IsNullOrEmpty(NavigateUri) == false)
				{
					var uri = GetNavigationUri(NavigateUri);
					Device.OpenUri(new Uri(uri));
				}
			});

			_tapGestureRecognizer = new TapGestureRecognizer { Command = NavigateCommand };
			GestureRecognizers.Add(_tapGestureRecognizer);
		}

		/// <summary>
		/// Gets or sets the subject.
		/// </summary>
		/// <value>The subject.</value>
		public string Subject
		{
			get { return (string)base.GetValue(SubjectProperty); }
			set { base.SetValue(SubjectProperty, value); }
		}

		/// <summary>
		/// Gets or sets the navigate URI.
		/// </summary>
		/// <value>The navigate URI.</value>
		public string NavigateUri
		{
			get { return (string)base.GetValue(NavigateUriProperty); }
			set { base.SetValue(NavigateUriProperty, value); }
		}

		/// <summary>
		/// Gets or sets the navigate command.
		/// </summary>
		/// <value>The navigate command.</value>
		public ICommand NavigateCommand
		{
			get { return (ICommand)base.GetValue(NavigateCommandProperty); }
			set { base.SetValue(NavigateCommandProperty, value); }
		}

		#region Overrides of BindableObject

		/// <summary>
		/// Call this method from a child class to notify that a change happened on a property.
		/// </summary>
		/// <remarks>
		/// <param name="propertyName">The name of the property that changed.</param>
		/// <para>
		/// A <see cref="T:Xamarin.Forms.BindableProperty"/> triggers this by itself. An inheritor 
		/// only needs to call this for properties without <see cref="T:Xamarin.Forms.BindableProperty"/> 
		/// as the backend store.
		/// </para>
		/// </remarks>
		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName != "NavigateCommand")
				return;

			this.GestureRecognizers.Remove(_tapGestureRecognizer);
			_tapGestureRecognizer = new TapGestureRecognizer { Command = NavigateCommand };
			GestureRecognizers.Add(_tapGestureRecognizer);
		}

		#endregion

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