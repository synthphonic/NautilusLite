using System;
using CoreAnimation;
using NautilusLite.Forms.Controls;
using NautilusLite.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace NautilusLite.iOS.Renderers
{
	public class ExtendedEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				var view = (ExtendedEntry)Element;

				switch (view.BorderStyle)
				{
					case BorderStyle.None:
						Control.BorderStyle = UITextBorderStyle.None;
						break;

					case BorderStyle.Underline:
						Control.BorderStyle = UITextBorderStyle.None;
						var borderLayer = new CALayer
						{
							MasksToBounds = true,
							Frame = new CoreGraphics.CGRect(0f, Frame.Height - 1, Frame.Width + 50f, 1f),
							BorderColor = view.BorderColor.ToCGColor(),
							BorderWidth = view.BorderWidth
						};

						Control.Layer.AddSublayer(borderLayer);
						break;
				}
			}
		}
	}
}