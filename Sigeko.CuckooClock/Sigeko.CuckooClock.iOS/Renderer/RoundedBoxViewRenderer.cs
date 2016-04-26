using System.ComponentModel;
using System.Drawing;
using CoreAnimation;
using CoreGraphics;
using UIKit;

using Sigeko.CuckooClock.Controls;
using Sigeko.CuckooClock.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedBoxView), typeof(RoundedBoxViewRenderer))]

namespace Sigeko.CuckooClock.iOS.Renderer
{
	public class RoundedBoxViewRenderer : ViewRenderer<RoundedBoxView, UIView>
	{
		UIView _childView;

		private CAGradientLayer _gradient;

		protected override void OnElementChanged(ElementChangedEventArgs<RoundedBoxView> eventArgs)
		{
			// Basisimplementierung
			base.OnElementChanged(eventArgs);

			var rbv = Element;
			if (rbv == null)
				return;

			var element = eventArgs.NewElement;

			if (rbv.UseGradient == true)
			{
				var rect = new CGRect(0, 0, element.WidthRequest, element.HeightRequest);
				_gradient = new CAGradientLayer
				{
					Frame = rect,
					StartPoint = new CGPoint(0.0, 0.5),
					EndPoint = new CGPoint(1.0, 0.5),
					Colors = new[] { rbv.StartColor.ToCGColor(), rbv.MidColor.ToCGColor(), rbv.EndColor.ToCGColor() },
					CornerRadius = (float)rbv.CornerRadius,
					BorderColor = rbv.Stroke.ToCGColor(),
					BorderWidth = (float)rbv.StrokeThickness,
					MasksToBounds = true
				};

				_childView = new UIView
				{
					AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight
				};
				_childView.Layer.InsertSublayer(_gradient, 0);
			}
			else
			{
				_childView = new UIView
				{
					BackgroundColor = rbv.FillColor.ToUIColor(),
					Layer =
					{
						CornerRadius = (float)rbv.CornerRadius,
						BorderColor = rbv.Stroke.ToCGColor(),
						BorderWidth = (float)rbv.StrokeThickness,
						MasksToBounds = true
					},
					AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight
				};
			}

			var shadowView = new UIView {_childView};

			if (rbv.HasShadow)
			{
				shadowView.Layer.ShadowColor = UIColor.Black.CGColor;
				shadowView.Layer.ShadowOffset = new SizeF(3, 3);
				shadowView.Layer.ShadowOpacity = 1;
				shadowView.Layer.ShadowRadius = 5;
			}

			SetNativeControl(shadowView);
		}

		/// <summary>
		/// TODO: Wird leider nicht gefeuert?
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
		{
			base.OnElementPropertyChanged(sender, eventArgs);

			if (eventArgs.PropertyName == RoundedBoxView.CornerRadiusProperty.PropertyName)
			{
				_childView.Layer.CornerRadius = (float)this.Element.CornerRadius;
			}
			else if (eventArgs.PropertyName == RoundedBoxView.StrokeProperty.PropertyName)
			{
				_childView.Layer.BorderColor = this.Element.Stroke.ToCGColor();
			}
			else if (eventArgs.PropertyName == RoundedBoxView.StrokeThicknessProperty.PropertyName)
			{
				_childView.Layer.BorderWidth = (float)this.Element.StrokeThickness;
			}
			else if (eventArgs.PropertyName == BoxView.ColorProperty.PropertyName)
			{
				_childView.BackgroundColor = this.Element.Color.ToUIColor();
			}
			else if (eventArgs.PropertyName == RoundedBoxView.FillColorProperty.PropertyName)
			{
				_childView.BackgroundColor = this.Element.FillColor.ToUIColor();
			}
			else if (eventArgs.PropertyName == RoundedBoxView.StartColorProperty.PropertyName)
			{
				var rbv = Element;
				_gradient.Colors = new[] {rbv.StartColor.ToCGColor(), rbv.MidColor.ToCGColor(), rbv.EndColor.ToCGColor()};
			}
			else if (eventArgs.PropertyName == RoundedBoxView.HasShadowProperty.PropertyName)
			{
				if (Element.HasShadow)
				{
					NativeView.Layer.ShadowColor = UIColor.Black.CGColor;
					NativeView.Layer.ShadowOffset = new SizeF(3, 3);
					NativeView.Layer.ShadowOpacity = 1;
					NativeView.Layer.ShadowRadius = 5;
				}
				else
				{
					NativeView.Layer.ShadowColor = UIColor.Clear.CGColor;
					NativeView.Layer.ShadowOffset = new SizeF();
					NativeView.Layer.ShadowOpacity = 0;
					NativeView.Layer.ShadowRadius = 0;
				}
			}
		}

	}
}

