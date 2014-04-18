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
					container.View.Frame = container.GetTopViewPositionWhenSliderIsHidden(View.Frame);
				},
				delegate
				{
					if (container.View.Superview != null) {
						container.View.Superview.SendSubviewToBack(container.View);
					}
					View.RemoveGestureRecognizer(_tapToClose);
					container.Hide();
					container.ViewDidDisappear(true);
				});
		}

		/// <summary>
		///     Insert a panel in the view hierarchy.  If this is done early in
		///     the creation process,  we postponed adding  until later, at one
		///     point we are guarantee that Superview is not null.
		/// </summary>
		/// <param name="container">Container.</param>
		public override void InsertPanel(PanelContainer container)
		{
			base.InsertPanel(container);
			container.View.Frame = container.GetTopViewPositionWhenSliderIsHidden(View.Frame);
		}

		public override void ShowPanel (PanelContainer container)
		{
			//FMT: shouldn't do it otherwise it will screw our container frame
			//container.ViewWillAppear(true);
			container.Show();
			if (container.View.Superview != null) {
				container.View.Superview.BringSubviewToFront(container.View);
			}
			UIView.Animate(AnimationSpeed, 0, UIViewAnimationOptions.CurveEaseInOut,
				delegate {
					container.View.Frame = container.GetTopViewPositionWhenSliderIsVisible(View.Frame);
				},
				delegate
				{
					View.AddGestureRecognizer(_tapToClose);
					container.ViewDidAppear(true);
				});
		}
	}
}

