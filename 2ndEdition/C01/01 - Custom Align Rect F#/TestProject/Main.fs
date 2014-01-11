namespace TestProject

open System
open MonoMac.AppKit
open System.Drawing
open MonoMac.Foundation

type CustomView () = 
    inherit NSView () 

    let OrangeColor = NSColor.FromDeviceRgba(1.0f, 0.6f, 0.0f, 1.0f)

    override this.DrawRect (dirtyRect : RectangleF) = 
        // Calculate offset from frame for 170 x 170 art
        let dx = (this.Frame.Size.Width - 170.0f) / 2.0f
        let dy = (this.Frame.Size.Height - 170.0f)

        //Draw a shadow
        let mutable rect = new RectangleF(8.0f + dx, -8.0f + dy, 160.0f, 160.0f)
        let mutable path = NSBezierPath.FromRoundedRect(rect, 32.0f, 32.0f)
        NSColor.Black.ColorWithAlphaComponent(0.3f).Set()
        path.Fill()

        //Draw shape with outline
        rect.Location <- new PointF(dx, dy)
        path <- NSBezierPath.FromRoundedRect(rect, 32.0f, 32.0f)
        NSColor.Black.Set()
        path.Fill()
        path.LineWidth <- 6.0f
        path.Stroke()

        OrangeColor.Set()
        path.Fill()
    
    override this.IntrinsicContentSize : SizeF = new SizeF(170.0f,  170.0f)

    override this.GetFrameForAlignmentRect(alignmentRect : RectangleF) =
        // 10 / 160 = 1.0625
        let width = alignmentRect.Size.Width * 1.0625f
        let height = alignmentRect.Size.Height * 1.0625f
        let size = new SizeF(width, height)
        let rect = new RectangleF(alignmentRect.Location, size)
        rect
     
    override this.GetAlignmentRectForFrame(frame : RectangleF) = 
        // 160 / 170 = 0.94117
        let width = frame.Size.Width * 0.94117f
        let height = frame.Size.Height * 0.94117f
        let size = new SizeF(width, height)
        let dy = (frame.Size.Height - 170.0f) / 2.0f //Account for vertical flippage
        let rect = new RectangleF(new PointF(frame.Location.X, frame.Location.Y + dy), size)
        rect

[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit NSApplicationDelegate ()
       
    override this.AwakeFromNib() = 
        let window = NSApplication.SharedApplication.Windows.[0]
        let mainView = window.ContentView
        let view = new CustomView()
        view.TranslatesAutoresizingMaskIntoConstraints <- false

        mainView.AddSubview(view)
        let constraintX = NSLayoutConstraint.Create(mainView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, view, NSLayoutAttribute.CenterX, 1.0f, 0.0f)
        mainView.AddConstraint(constraintX)
        let constraintY = NSLayoutConstraint.Create(mainView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, view, NSLayoutAttribute.CenterY, 1.0f, 0.0f)
        mainView.AddConstraint(constraintY);    
 
module Main =
    [<EntryPoint>]
    let main args =
        NSApplication.Init()
        NSApplication.Main (args)
        0


    