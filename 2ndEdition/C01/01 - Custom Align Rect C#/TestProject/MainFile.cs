#define USE_ALIGNMENT_RECTS

using System;
using MonoMac.AppKit;
using System.Drawing;
using MonoMac.Foundation;

namespace TestProject
{
	public class CustomView : NSView
	{
		readonly NSColor ORANGE_COLOR = NSColor.FromDeviceRgba(1, 0.6f, 0, 1);

		public override void DrawRect(System.Drawing.RectangleF dirtyRect)
		{
			// Calculate offset from frame for 170 x 170 art
			var dx = (Frame.Size.Width - 170) / 2.0f;
			var dy = (Frame.Size.Height - 170);

			//Draw a shadow
			var rect = new RectangleF(8 + dx, -8 + dy, 160, 160);
			var path = NSBezierPath.FromRoundedRect(rect, 32, 32);
			NSColor.Black.ColorWithAlphaComponent(0.3f).Set();
			path.Fill();

			//Draw shape with outline
			rect.Location = new PointF(dx, dy);
			path = NSBezierPath.FromRoundedRect(rect, 32, 32);
			NSColor.Black.Set();
			path.Fill();
			path.LineWidth = 6;
			path.Stroke();

			ORANGE_COLOR.Set();
			path.Fill();
		}

		// Fixed content size - base + frame
		SizeF intrinsic = new SizeF(170, 170);
		public override SizeF IntrinsicContentSize
		{
			get
			{
				return intrinsic;
			}
		}

#if USE_ALIGNMENT_RECTS
		public override RectangleF GetFrameForAlignmentRect(RectangleF alignmentRect)
		{
			// 10 / 160 = 1.0625
			var width = alignmentRect.Size.Width * 1.0625f;
			var height = alignmentRect.Size.Height * 1.0625f;
			var size = new SizeF(width, height);
			var rect = new RectangleF(alignmentRect.Location, size);
			return rect;
		}

		public override RectangleF GetAlignmentRectForFrame(RectangleF frame)
		{
			// 160 / 170 = 0.94117
			var width = frame.Size.Width * 0.94117f;
			var height = frame.Size.Height * 0.94117f;
			var size = new SizeF(width, height);
			var dy = (frame.Size.Height - 170) / 2.0f; //Account for vertical flippage
			var rect = new RectangleF(new PointF(frame.Location.X, frame.Location.Y + dy), size);
			return rect;
		}
#endif

	}

	[Register("AppDelegate")]
	public partial class AppDelegate : NSApplicationDelegate
	{
		public override void AwakeFromNib()
		{
			var window = NSApplication.SharedApplication.Windows[0];
			var mainView = window.ContentView;
			var view = new CustomView() {
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			mainView.AddSubview(view);
			var constraintX = NSLayoutConstraint.Create(
				mainView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, view, NSLayoutAttribute.CenterX, 1, 0);
			mainView.AddConstraint(constraintX);
			var constraintY = NSLayoutConstraint.Create(
				mainView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, view, NSLayoutAttribute.CenterY, 1, 0);
			mainView.AddConstraint(constraintY);
		}
	}

	public class MainClass
	{
		static void Main(string[] args)
		{
			NSApplication.Init();
			NSApplication.Main(args);
		}
	}
}

