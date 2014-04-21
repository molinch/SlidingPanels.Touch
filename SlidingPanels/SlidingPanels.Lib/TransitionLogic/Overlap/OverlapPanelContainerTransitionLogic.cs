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
			base.Show (contentView);
			if (contentView.Superview != null)
				contentView.Superview.BringSubviewToFront(contentView);
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

		public override void ResizeViews (UIView contentView, UIView panelView, SizeF panelSize)
		{
			panelView.Frame = GetPanelPosition(contentView, panelSize);
			contentView.Frame = new RectangleF (contentView.Frame.Location, panelView.Frame.Size);
		}
	}
}

