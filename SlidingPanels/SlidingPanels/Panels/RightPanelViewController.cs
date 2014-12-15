// /// Copyright (C) 2013 Pat Laplante & Franc Caico
// ///
// ///	Permission is hereby granted, free of charge, to  any person obtaining a copy 
// /// of this software and associated documentation files (the "Software"), to deal 
// /// in the Software without  restriction, including without limitation the rights 
// /// to use, copy,  modify,  merge, publish,  distribute,  sublicense, and/or sell 
// /// copies of the  Software,  and  to  permit  persons  to   whom the Software is 
// /// furnished to do so, subject to the following conditions:
// ///
// ///		The above  copyright notice  and this permission notice shall be included 
// ///     in all copies or substantial portions of the Software.
// ///
// ///		THE  SOFTWARE  IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
// ///     OR   IMPLIED,   INCLUDING  BUT   NOT  LIMITED   TO   THE   WARRANTIES  OF 
// ///     MERCHANTABILITY,  FITNESS  FOR  A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
// ///     IN NO EVENT SHALL  THE AUTHORS  OR COPYRIGHT  HOLDERS  BE  LIABLE FOR ANY 
// ///     CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT 
// ///     OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION  WITH THE SOFTWARE OR 
// ///     THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// /// -----------------------------------------------------------------------------
//
using System;
using System.Drawing;
using Foundation;
using UIKit;
using SlidingPanels.Lib;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;

namespace SlidingPanels.Panels
{
	public class RightPanelViewController : UIViewController
	{
		public SlidingPanelsNavigationViewController PanelsNavController {
			get;
			private set;
		}

		public RightPanelViewController (SlidingPanelsNavigationViewController controller) : base()
		{
			PanelsNavController = controller;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.Frame = new CGRect (View.Frame.Location, new CGSize (View.Frame.Width/2, View.Frame.Height));

			var info = new UILabel () {
				Text = "This is the right panel",
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines = 2,
				TranslatesAutoresizingMaskIntoConstraints = false,
				TextAlignment = UITextAlignment.Center
			};

			View.AddSubview (info);

			View.AddConstraints (
				info.WithSameTop(View),
				info.WithSameLeft(View),
				info.WithSameRight(View),
				info.WithSameBottom(View)
			);
		}
	}
}

