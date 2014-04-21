/// Copyright (C) 2013 Pat Laplante & Franc Caico
///
///	Permission is hereby granted, free of charge, to  any person obtaining a copy 
/// of this software and associated documentation files (the "Software"), to deal 
/// in the Software without  restriction, including without limitation the rights 
/// to use, copy,  modify,  merge, publish,  distribute,  sublicense, and/or sell 
/// copies of the  Software,  and  to  permit  persons  to   whom the Software is 
/// furnished to do so, subject to the following conditions:
///
///		The above  copyright notice  and this permission notice shall be included 
///     in all copies or substantial portions of the Software.
///
///		THE  SOFTWARE  IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
///     OR   IMPLIED,   INCLUDING  BUT   NOT  LIMITED   TO   THE   WARRANTIES  OF 
///     MERCHANTABILITY,  FITNESS  FOR  A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
///     IN NO EVENT SHALL  THE AUTHORS  OR COPYRIGHT  HOLDERS  BE  LIABLE FOR ANY 
///     CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT 
///     OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION  WITH THE SOFTWARE OR 
///     THE USE OR OTHER DEALINGS IN THE SOFTWARE.
/// -----------------------------------------------------------------------------

using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SlidingPanels.Lib;
using Cirrious.FluentLayouts.Touch;
using SlidingPanels.Lib.PanelContainers;

namespace SlidingPanels.Panels
{
	public class LeftPanelViewController : UIViewController
	{
		public SlidingPanelsNavigationViewController PanelsNavController {
			get;
			private set;
		}

		public LeftPanelViewController (SlidingPanelsNavigationViewController controller) : base ()
		{
			PanelsNavController = controller;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.Frame = new RectangleF (View.Frame.Location, new SizeF (View.Frame.Width/2, View.Frame.Height));

			var buttonA = new UIButton (UIButtonType.System) {

			};
			buttonA.SetTitle ("Screen A", UIControlState.Normal);
			buttonA.TouchUpInside += (sender, e) => {
				PanelsNavController.PopToRootViewController(false);
				PanelsNavController.TogglePanel(PanelType.LeftPanel);
			};

			var buttonB = new UIButton (UIButtonType.System);
			buttonB.SetTitle ("Screen B", UIControlState.Normal);
			buttonB.TouchUpInside += (sender, e) => {
				PanelsNavController.PushViewController(new ExampleContentB(), true);
				PanelsNavController.TogglePanel(PanelType.LeftPanel);
			};

			View.AddSubviews (
				buttonA,
				buttonB
			);

			View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints ();
			View.AddConstraints (
				buttonA.AtTopOf(View, 30),
				buttonA.WithSameCenterX(View),
				buttonB.Below(buttonA),
				buttonB.WithSameCenterX(buttonA)
			);
		}
	}
}

