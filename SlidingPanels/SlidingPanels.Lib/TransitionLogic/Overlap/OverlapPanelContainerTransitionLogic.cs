using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Linq;
using SlidingPanels.Lib.PanelContainers;

namespace SlidingPanels.Lib.TransitionLogic.Overlap
{
	public abstract class OverlapPanelContainerTransitionLogic: PanelContainerTransitionLogic
	{
		/// <summary>
		/// Makes this Panel visible
		/// </summary>
		public override void Show (PanelContainer container)
		{
			Console.WriteLine ("Show");
			base.Show (container);

			container.View.Frame = GetContainerViewPositionWhenSliderIsHidden(container.View.Frame, WindowState.CurrentScreenFrame, container.Size);

			if (container.View.Superview != null)
				container.View.Superview.BringSubviewToFront(container.View);
		}

		public override void Hide (PanelContainer container)
		{
			base.Hide (container);
			if (container.View.Superview != null)
				container.View.Superview.SendSubviewToBack(container.View);
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

		public override void ResizeViews (PanelContainer container)
		{
			container.View.Frame = new RectangleF (container.View.Frame.Location, container.Size);
			container.PanelVC.View.Frame = GetPanelPosition(container.View, container.Size);
		}

		public override void RotateView (PanelContainer container)
		{
			Hide(container); // FMT: currently when we rotate it's really messy so to be easier panels are temporarly hidden
			ResizeViews(container);
		}
	}
}

