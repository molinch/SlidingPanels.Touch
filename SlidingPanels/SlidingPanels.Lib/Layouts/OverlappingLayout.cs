using System;
using System.Collections.Generic;
using SlidingPanels.Lib.PanelContainers;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SlidingPanels.Lib.TransitionLogic;
using SlidingPanels.Lib.TransitionLogic.Overlap;
using SlidingPanels.Lib.TransitionLogic.Shift;

namespace SlidingPanels.Lib.Layouts
{
	/// <summary>
	/// Overlapping layout for the sliding panels. Overlapping means that the menu will cover the existing content.
	/// </summary>
	public class OverlappingLayout: ISlidingLayout
	{
		/// <summary>
		///     How fast do we show/hide panels.
		/// </summary>
		protected const float AnimationSpeed = 0.25f;

		#region Handle touch gestures
		public void SlidingGestureBegan(UIViewController slidingController, PanelContainer container, PointF touchPt)
		{
			BringContainerToForeground (container);
		}

		public void SlidingGestureMoved(UIViewController slidingController, PanelContainer container, PointF touchPt) {
			RectangleF newFrame = container.Sliding(touchPt, slidingController.View.Frame);
			container.View.Frame = newFrame;
		}

		public void SlidingGestureEnded(UIViewController slidingController, PanelContainer container, PointF touchPt) {
		}
		#endregion

		#region Handle panels

		public PanelContainerTransitionLogic CreateTransitionLogic(PanelType panelType)
		{
			switch (panelType) {
				case PanelType.LeftPanel:
					return new OverlapLeftPanelContainerTransitionLogic ();
				case PanelType.RightPanel:
					return new OverlapRightPanelContainerTransitionLogic();
				case PanelType.BottomPanel:
					throw new NotImplementedException("Currently only the right panel can be an overlapped panel.");
			}
			throw new Exception("Unexpected panel type.");
		}

		/// <summary>
		///     Insert a panel in the view hierarchy.  If this is done early in
		///     the creation process,  we postponed adding  until later, at one
		///     point we are guarantee that Superview is not null.
		/// </summary>
		/// <param name="container">Container.</param>
		public void WhenPanelInserted(UIViewController slidingController, PanelContainer container)
		{
			container.View.Frame = container.GetContainerViewPositionWhenSliderIsHidden(slidingController.View.Frame);
			BringContainerToForeground (container);
			container.WhenInserted(slidingController);
		}

		public void WhenPanelStartsShowing(PanelContainer container, UIInterfaceOrientation orientation)
		{
			BringContainerToForeground (container);
		}

		public void CompleteShowPanel(UIViewController slidingController, PanelContainer container, Action onComplete)
		{
			if (!container.IsVisible)
				container.Show();
			UIView.Animate(AnimationSpeed, 0, UIViewAnimationOptions.CurveEaseInOut,
				delegate {
					container.View.Frame = container.GetContainerViewPositionWhenSliderIsVisible(slidingController.View.Frame);
				},
				delegate
				{
					if (onComplete != null)
						onComplete();
				});
		}

		public void CompleteHidePanel (UIViewController slidingController, PanelContainer container, Action onComplete)
		{
			container.ViewWillDisappear(true);

			UIView.Animate(AnimationSpeed, 0, UIViewAnimationOptions.CurveEaseInOut,
				delegate {
					container.View.Frame = container.GetContainerViewPositionWhenSliderIsHidden(slidingController.View.Frame);
				},
				delegate
				{
					if (onComplete != null)
						onComplete();
				});
		}

		#endregion

		private void BringContainerToForeground(PanelContainer container) {
			if (container != null && container.View.Superview != null)
				container.View.Superview.BringSubviewToFront(container.View);
		}
	}
}

