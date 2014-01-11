namespace HelloWorld
 
open System
open System.Collections.Generic
open MonoTouch.UIKit
open MonoTouch.Foundation

type TestBedViewController () =
 inherit UIViewController()

    override this.LoadView () = 
        this.View <- new UIView()
        this.View.BackgroundColor <- UIColor.White

        let subview = NSBundle.MainBundle.LoadNib("View", this, null).GetItem<UIView>(0)
        this.View.AddSubview(subview)

        subview.TranslatesAutoresizingMaskIntoConstraints <- false

        //Center it
        let ctrXConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.CenterX, 1.0F, 0.0F)
        this.View.AddConstraint(ctrXConstraint)
        let ctrYConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.CenterY, 1.0F, 0.0F)
        this.View.AddConstraint(ctrYConstraint);

        //Set its aspect
        let aspConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.Width, NSLayoutRelation.Equal, subview, NSLayoutAttribute.Height, 1.0F, 0.0F)
        this.View.AddConstraint(aspConstraint)

        //Constrain it to the superview's size
        let szwConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.Width, NSLayoutRelation.LessThanOrEqual, this.View, NSLayoutAttribute.Width, 1.0F, -40.0F)
        this.View.AddConstraint(szwConstraint)
        let szhConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.Height, NSLayoutRelation.LessThanOrEqual, this.View, NSLayoutAttribute.Height, 1.0F, -40.0F)
        this.View.AddConstraint(szhConstraint);

        //Add a weak "match size" constraint
        let szwWeakConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Width, 1.0F, -40.0F)
        szwWeakConstraint.Priority <- 100.0F
        this.View.AddConstraint(szwWeakConstraint)
        let szhWeakConstraint = NSLayoutConstraint.Create(subview, NSLayoutAttribute.Height, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Height, 1.0F, -40.0F)
        szhWeakConstraint.Priority <- 100.0F
        this.View.AddConstraint(szhWeakConstraint);
  
[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit UIApplicationDelegate ()

    let window = new UIWindow (UIScreen.MainScreen.Bounds)
 
    override this.FinishedLaunching (app, options) =
        let tbvc = new TestBedViewController()
        tbvc.EdgesForExtendedLayout <- UIRectEdge.None


        let navController = new UINavigationController(tbvc)
        window.RootViewController <- navController
        window.MakeKeyAndVisible ()
        true
 
module Main =
    [<EntryPoint>]
    let main args =
        UIApplication.Main (args, null, "AppDelegate")
        0

