using System;
using MonoTouch.UIKit;
using System.Linq;
using SlidingPanels.Lib.Tools;
using UIImageEffects;
using System.Drawing;

namespace SlidingPanels.Lib.PanelContainers
{
	public class BlurryRightPanelContainer: OverlappingRightPanelContainer
	{
		private UIImageView blurryBackground;

		/// <summary>
		/// Initializes a new instance of the <see cref="SlidingPanels.Lib.PanelContainers.RightPanelContainer" /> class.
		/// </summary>
		/// <param name="panel">Panel.</param>
		public BlurryRightPanelContainer(UIViewController panel)
			: base(panel)
		{
		}

		public override void SlidingStarted(PointF touchPosition, RectangleF topViewCurrentFrame)
		{
			base.SlidingStarted(touchPosition, topViewCurrentFrame);

			var displayedController = CurrentController;
			if (displayedController == null)
				return;

			var viewBackground = displayedController.View.MakeSnapShot();

			if (blurryBackground != null) {
				blurryBackground.RemoveFromSuperview();
			}
			blurryBackground = new UIImageView(viewBackground.ApplyLightEffect());

			View.Add(blurryBackground);
			View.SendSubviewToBack(blurryBackground);
		}

		private UIViewController CurrentController
		{
			get
			{
				var window = UIApplication.SharedApplication.KeyWindow ?? UIApplication.SharedApplication.Windows[0];
				var navController = window.RootViewController.ChildViewControllers[0];
				return navController.ChildViewControllers.LastOrDefault();
			}
		}
	}
}

