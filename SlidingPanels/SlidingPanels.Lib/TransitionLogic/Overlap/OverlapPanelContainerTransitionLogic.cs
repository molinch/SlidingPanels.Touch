using System;
using UIKit;
using System.Drawing;
using System.Linq;
using SlidingPanels.Lib.PanelContainers;
using SlidingPanels.Lib.Tools;
using CoreGraphics;

namespace SlidingPanels.Lib.TransitionLogic.Overlap
{
	/// <summary>
	/// Base abstract class giving the transition logic for panel containers when using the "overlapping" layout.
	/// </summary>
	public abstract class OverlapPanelContainerTransitionLogic: PanelContainerTransitionLogic
	{
		/// <summary>
		/// Makes this Panel visible
		/// </summary>
		public override void Show (PanelContainer container)
		{
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

        public override void ResizeContainer (PanelContainer container, CGSize screenSize)
		{
            var width = Math.Min(container.Size.Width, screenSize.Width);
            container.View.Frame = new CGRect (container.View.Frame.Location.X, 0, width, screenSize.Height);
            container.PanelVC.View.Frame = GetPanelPosition(container.View, new CGSize(width, screenSize.Height));
		}

        public override void RotateContainer (PanelContainer container, CGSize screenSize)
		{
			Hide(container); // FMT: currently when we rotate it's really messy so to be easier panels are temporarly hidden
			ResizeContainer(container, screenSize);
		}
	}
}

