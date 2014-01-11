using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace HelloWorld
{
	public class TestBedViewController : UIViewController
	{
		public override void LoadView()
		{
			View = new UIView() {
				BackgroundColor = UIColor.White
			};

			var subview = NSBundle.MainBundle.LoadNib("View", this, null).GetItem<UIView>(0);
			View.AddSubview(subview);

			subview.TranslatesAutoresizingMaskIntoConstraints = false;

			//Center it
			var ctrXConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterX, 1, 0);
			View.AddConstraint(ctrXConstraint);
			var ctrYConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, View, NSLayoutAttribute.CenterY, 1, 0);
			View.AddConstraint(ctrYConstraint);

			//Set its aspect
			var aspConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.Width, NSLayoutRelation.Equal, subview, NSLayoutAttribute.Height, 1, 0);
			View.AddConstraint(aspConstraint);

			//Constrain it to the superview's size
			var szwConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.Width, NSLayoutRelation.LessThanOrEqual, View, NSLayoutAttribute.Width, 1, -40);
			View.AddConstraint(szwConstraint);
			var szhConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.Height, NSLayoutRelation.LessThanOrEqual, View, NSLayoutAttribute.Height, 1, -40);
			View.AddConstraint(szhConstraint);

			//Add a weak "match size" constraint
			var szwWeakConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1, -40);
			szwWeakConstraint.Priority = 100;
			View.AddConstraint(szwWeakConstraint);
			var szhWeakConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.Height, NSLayoutRelation.Equal, View, NSLayoutAttribute.Height, 1, -40);
			szhWeakConstraint.Priority = 100;
			View.AddConstraint(szhWeakConstraint);
		}
	}

	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		TestBedViewController tbvc;

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			window = new UIWindow(UIScreen.MainScreen.Bounds);

			tbvc = new TestBedViewController();
			tbvc.EdgesForExtendedLayout = UIRectEdge.None;

			var nav = new UINavigationController(tbvc);
			window.RootViewController = nav;

			window.MakeKeyAndVisible();

			return true;
		}
	}

	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}
