using System;
using System.Collections.Generic;
using SlidingPanels.Lib.PanelContainers;
using System.Drawing;
using MonoTouch.UIKit;
using SlidingPanels.Lib.TransitionLogic;
using SlidingPanels.Lib.TransitionLogic.Shift;

namespace SlidingPanels.Lib.Layouts
{
	/// <summary>
	/// Shifting layout for the sliding panels. Shifting means that the whole frame will move.
	/// </summary>
	public class ShiftingLayout: ISlidingLayout
	{
		#region Constants

		/// <summary>
		///     How fast do we show/hide panels.
		/// </summary>
		protected const float AnimationSpeed = 0.25f;

		#endregion

		#region Handle touch gestures
		public void SlidingGestureBegan(UIViewController slidingController, PanelContainer container, PointF touchPt) {
		}

		public void SlidingGestureMoved(UIViewController slidingController, PanelContainer container, PointF touchPt) {
			RectangleF newFrame = container.Sliding (touchPt, slidingController.View.Frame);
			slidingController.View.Frame = newFrame;
		}

		public void SlidingGestureEnded(UIViewController slidingController, PanelContainer container, PointF touchPt) {
		}
		#endregion

		#region Handle panels
		public PanelContainerTransitionLogic CreateTransitionLogic(PanelType panelType)
		{
			switch (panelType) {
				case PanelType.LeftPanel:
					return new ShiftLeftPanelContainerTransitionLogic();
				case PanelType.RightPanel:
					return new ShiftRightPanelContainerTransitionLogic();
				case PanelType.BottomPanel:
					return new ShiftBottomPanelContainerTransitionLogic();
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
			container.WhenInserted(slidingController);
		}

		public void WhenPanelStartsShowing(PanelContainer container, UIInterfaceOrientation orientation)
		{
			RectangleF frame = UIScreen.MainScreen.Bounds;

			if (orientation != UIInterfaceOrientation.Portrait) {
				frame.Width = UIScreen.MainScreen.Bounds.Height;
				frame.Height = UIScreen.MainScreen.ApplicationFrame.Width;
				frame.X = UIScreen.MainScreen.ApplicationFrame.Y;

				if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft) {
					frame.Y = UIScreen.MainScreen.ApplicationFrame.X;
				} else {
					frame.Y = UIScreen.MainScreen.Bounds.Width - UIScreen.MainScreen.ApplicationFrame.Width;
				}

			}

			container.View.Frame = frame;
		}

		public void CompleteShowPanel(UIViewController slidingController, PanelContainer container, Action onComplete)
		{
			container.Show();
			if (container.View.Superview != null) {
				container.View.Superview.BringSubviewToFront(container.View);
			}
			UIView.Animate(AnimationSpeed, 0, UIViewAnimationOptions.CurveEaseInOut,
				delegate {
					slidingController.View.Frame = container.GetTopViewPositionWhenSliderIsVisible(slidingController.View.Frame);
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
					slidingController.View.Frame = container.GetTopViewPositionWhenSliderIsHidden(slidingController.View.Frame);
				},
				delegate
				{
					if (container.View.Superview != null) {
						container.View.Superview.SendSubviewToBack(container.View);
					}
					if (onComplete != null)
						onComplete();
				});
		}
		#endregion
	}
}

