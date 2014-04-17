using System;
using System.Drawing;
using System.Linq;
using MonoTouch.UIKit;
using SlidingPanels.Lib;
using SlidingPanels.Lib.PanelContainers;

namespace SlidingPanels.Lib
{
	public class OverlappingSlidingNavigationController : SlidingPanelsNavigationViewController
	{
		public OverlappingSlidingNavigationController(UIViewController controller) : base(controller) {
		}

		protected override SlidingGestureRecogniser CreateGestureRecogniser()
		{
			var slidingGesture = new OverlappingSlidingGestureRecogniser(_panelContainers, ShouldReceiveTouch, this);
			slidingGesture.ShowPanel += (sender, e) => ShowPanel(((SlidingGestureEventArgs) e).PanelContainer);
			slidingGesture.HidePanel += (sender, e) => HidePanel(((SlidingGestureEventArgs) e).PanelContainer);
			return slidingGesture;
		}

		public override void HidePanel (PanelContainer container)
		{
			container.ViewWillDisappear(true);

			UIView.Animate(AnimationSpeed, 0, UIViewAnimationOptions.CurveEaseInOut,
				delegate {
					if (container.View.Superview == null)
						return;
					container.View.Superview.SendSubviewToBack(container.View);
				},
				delegate
				{
					View.RemoveGestureRecognizer(_tapToClose);
					container.Hide();
					container.ViewDidDisappear(true);
				});
		}

		public override void ShowPanel (PanelContainer container)
		{
			container.ViewWillAppear(true);
			container.Show();

			UIView.Animate(AnimationSpeed, 0, UIViewAnimationOptions.CurveEaseInOut,
				delegate {
					if (container.View.Superview == null)
						return;
					container.View.Superview.BringSubviewToFront(container.View);
				},
				delegate
				{
					View.AddGestureRecognizer(_tapToClose);
					container.ViewDidAppear(true);
				});
		}
	}
}

