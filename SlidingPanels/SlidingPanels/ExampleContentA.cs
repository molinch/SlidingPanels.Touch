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
using SlidingPanels.Lib.PanelContainers;
using SlidingPanels.Panels;
using Cirrious.FluentLayouts.Touch;

namespace SlidingPanels
{
	public class ExampleContentA : UIViewController
	{
		public ExampleContentA (): base()
		{
			NavigationItem.LeftBarButtonItem = CreateSliderButton("Images/SlideRight40.png", PanelType.LeftPanel);
			NavigationItem.RightBarButtonItem = CreateSliderButton("Images/SlideLeft40.png", PanelType.RightPanel);
		}

		private UIBarButtonItem CreateSliderButton(string imageName, PanelType panelType)
		{
			var button = new UIButton(new RectangleF(0, 0, 40f, 40f));
			button.SetBackgroundImage(UIImage.FromBundle(imageName), UIControlState.Normal);
			button.TouchUpInside += delegate
			{
				var navController = NavigationController as SlidingPanelsNavigationViewController;
				navController.TogglePanel(panelType);
			};

			return new UIBarButtonItem(button);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//var navController = NavigationController as SlidingPanelsNavigationViewController;

			// Perform any additional setup after loading the view, typically from a nib.
			//LeftArrowImage.Image = UIImage.FromBundle("Images/LeftArrow.png");
			//UpArrowImage.Image = UIImage.FromBundle("Images/UpArrow.png");
			//RightArrowImage.Image = UIImage.FromBundle("Images/RightArrow.png");

			NavigationController.NavigationBar.TintColor = UIColor.Black;
			View.BackgroundColor = UIColor.LightGray;

			var title = new UILabel () {
				Text = "This is screen A",
			};

			var info = new UILabel () {
				Text = "If you want to switch the layout choose one here:"
			};
			info.Font = UIFont.FromDescriptor (info.Font.FontDescriptor, 14);

			var window = UIApplication.SharedApplication.Windows[0];
			var btnShift = new UIButton (UIButtonType.System);
			btnShift.SetTitle ("Shifting", UIControlState.Normal);
			btnShift.TouchUpInside += (sender, e) => LayoutSwitch.ApplyShifting(window);

			var btnOverlapping = new UIButton (UIButtonType.System);
			btnOverlapping.SetTitle ("Overlapping", UIControlState.Normal);
			btnOverlapping.TouchUpInside += (sender, e) => LayoutSwitch.ApplyOverlapping(window);

			var btnBlurry = new UIButton (UIButtonType.System);
			btnBlurry.SetTitle ("Blurry", UIControlState.Normal);
			btnBlurry.TouchUpInside += (sender, e) => LayoutSwitch.ApplyBlurry(window);

			var infoBlurry = new UILabel () {
				Text = "Try blurry with the screen B (from left menu)",
				Lines = 2,
				LineBreakMode = UILineBreakMode.WordWrap,
				TextAlignment = UITextAlignment.Center
			};

			View.AddSubviews (
				title,
				info,
				btnShift,
				btnOverlapping,
				btnBlurry,
				infoBlurry
			);

			View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints ();
			View.AddConstraints (
				title.AtTopOf(View, 70),
				title.WithSameCenterX(View),
				info.WithSameCenterX(title),
				info.Below(title, 5),
				btnShift.Below(info, 5),
				btnShift.WithSameCenterX(title),
				btnOverlapping.Below(btnShift, 5),
				btnOverlapping.WithSameCenterX(title),
				btnBlurry.Below(btnOverlapping, 5),
				btnBlurry.WithSameCenterX(title),
				infoBlurry.Below(btnBlurry),
				infoBlurry.WithSameLeft(View),
				infoBlurry.WithSameRight(View)
			);
		}
	}
}



