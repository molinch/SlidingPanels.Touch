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
using SlidingPanels.Lib.Layouts;
using SlidingPanels.Lib;
using UIKit;
using SlidingPanels.Lib.PanelContainers;
using SlidingPanels.Panels;
using SlidingPanels.Lib.TransitionEffects;
using Cirrious.FluentLayouts.Touch;

namespace SlidingPanels
{
	public class LayoutSwitch
	{
		private static void Apply(UIWindow window, ISlidingLayout layout,
			Func<PanelContainer, IPanelContainerWithEffect> funcEffect,
			Action<PanelContainer> leftContainerCustomization,
			Action<PanelContainer> rightContainerCustomization
		) {
			var navController = new SlidingPanelsNavigationViewController(new ExampleContentA (), layout);

			var rootController = new UIViewController ();
			rootController.AddChildViewController (navController);
			rootController.View.AddSubview (navController.View);

			window.RootViewController = rootController;

			var leftContainer = new PanelContainer (layout, new LeftPanelViewController (navController), PanelType.LeftPanel, funcEffect);
			leftContainerCustomization (leftContainer);
			navController.InsertPanel (leftContainer);

			var rightContainer = new PanelContainer (layout, new RightPanelViewController (navController), PanelType.RightPanel, funcEffect);
			rightContainerCustomization (rightContainer);
			navController.InsertPanel (rightContainer);
		}

		public static void ApplyShifting(UIWindow window)
		{
			Apply (window, new ShiftingLayout (), null,
				pc => pc.View.BackgroundColor = UIColor.FromRGB(0, 255, 0),
				pc => pc.View.BackgroundColor = UIColor.FromRGB(255, 0, 0));
		}

		public static void ApplyOverlapping(UIWindow window)
		{
			Apply (window, new OverlappingLayout (), null,
				pc => {
					pc.View.Opaque = false;
					pc.View.BackgroundColor = UIColor.FromRGBA(50, 200, 50, 0.7f);
				},
				pc => {
					pc.View.Opaque = false;
					pc.View.BackgroundColor = UIColor.FromRGBA(200, 0, 0, 0.7f);
				});
		}

		public static void ApplyBlurry(UIWindow window)
		{
			Apply (window, new OverlappingLayout (), pc => new BlurryContainer (pc),
				pc => pc.View.BackgroundColor = UIColor.Clear,
				pc => pc.View.BackgroundColor = UIColor.Clear);
		}
	}
}

