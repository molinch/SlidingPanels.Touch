using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Linq;

namespace SlidingPanels.Lib.TransitionLogic.Overlap
{
	public abstract class OverlapPanelContainerTransitionLogic: PanelContainerTransitionLogic
	{
		/// <summary>
		/// Makes this Panel visible
		/// </summary>
		public override void Show (UIView contentView)
		{
			if (contentView.Hidden)
				contentView.Frame = new RectangleF(new PointF(CurrentController.View.Frame.Width, 0), contentView.Frame.Size);
			contentView.Hidden = false;
		}

		protected UIViewController CurrentController
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

