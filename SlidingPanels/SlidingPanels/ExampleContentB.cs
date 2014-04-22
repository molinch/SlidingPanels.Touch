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
using Cirrious.FluentLayouts.Touch;

namespace SlidingPanels
{
	public class ExampleContentB : UIViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.Blue;

			var title = new UILabel () {
				Text = "This is screen B",
				TextColor = UIColor.White
			};

			var info = new UILabel () {
				Text = "Slide manually the panels or use the buttons below",
				TextColor = UIColor.White,
				Lines = 2,
				LineBreakMode = UILineBreakMode.WordWrap,
				TextAlignment = UITextAlignment.Center
			};

			var navController = NavigationController as SlidingPanelsNavigationViewController;

			var btnTriggerLeftMenu = new UIButton (UIButtonType.System);
			btnTriggerLeftMenu.SetTitle ("Toggle left menu", UIControlState.Normal);
			btnTriggerLeftMenu.TouchUpInside += (sender, e) => navController.TogglePanel(PanelType.LeftPanel);
			btnTriggerLeftMenu.SetTitleColor (UIColor.Yellow, UIControlState.Normal);

			var btnTriggerRightMenu = new UIButton (UIButtonType.System);
			btnTriggerRightMenu.SetTitle ("Toggle right menu", UIControlState.Normal);
			btnTriggerRightMenu.TouchUpInside += (sender, e) => navController.TogglePanel(PanelType.RightPanel);
			btnTriggerRightMenu.SetTitleColor (UIColor.Yellow, UIControlState.Normal);

			var paysage = new UIImageView(UIImage.FromBundle("Images/paysage.jpg")) {
				ContentMode = UIViewContentMode.ScaleAspectFill,
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			View.AddSubviews (
				paysage,
				title,
				info,
				btnTriggerLeftMenu,
				btnTriggerRightMenu
			);

			View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints ();
			View.AddConstraints (
				title.AtTopOf(View, 70),
				title.WithSameCenterX(View),
				info.Below(title, 10),
				info.WithSameLeft(View),
				info.WithSameRight(View),
				btnTriggerLeftMenu.Below(info),
				btnTriggerLeftMenu.WithSameCenterX(title),
				btnTriggerRightMenu.Below(btnTriggerLeftMenu),
				btnTriggerRightMenu.WithSameCenterX(title),
				paysage.WithSameLeft(View),
				paysage.WithSameRight(View),
				paysage.WithSameTop(View),
				paysage.WithSameBottom(View)
			);
		}
	}
}

