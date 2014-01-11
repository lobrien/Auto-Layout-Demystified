
namespace Simple
 
open System
open MonoTouch.UIKit
open MonoTouch.Foundation
open System.Drawing
 
type ContentView ( color : UIColor ) as self = 
    inherit UIView ()
    do
        self.BackgroundColor <- color
 
type SimpleController ( ) =
    inherit UIViewController ()
 
    override this.ViewDidLoad () = 
        this.View <- new ContentView(UIColor.Blue)
 
[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit UIApplicationDelegate ()
 
    let window = new UIWindow (UIScreen.MainScreen.Bounds)
 
    // This method is invoked when the application is ready to run.
    override this.FinishedLaunching (app, options) =
        let viewController = new SimpleController()
        viewController.Title <- "F# Rocks"
 
        let navController = new UINavigationController(viewController)
        window.RootViewController <- navController
        window.MakeKeyAndVisible ()
        true
 
module Main =
    [<EntryPoint>]
    let main args =
        UIApplication.Main (args, null, "AppDelegate")
        0

