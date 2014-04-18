using System;
using MonoTouch.UIKit;
using SlidingPanels.Lib;
using System.Collections.Generic;
using SlidingPanels.Lib.PanelContainers;
using System.Drawing;

namespace SlidingPanels.Lib
{
	public class OverlappingSlidingGestureRecogniser: SlidingGestureRecogniser
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SlidingPanels.Lib.SlidingGestureRecogniser"/> class.
		/// </summary>
		/// <param name="panelContainers">List of Panel Containers to monitor for gestures</param>
		/// <param name="shouldReceiveTouch">Indicates that touch events should be monitored</param>
		/// <param name="slidingController">The Sliding Panels controller</param>
		public OverlappingSlidingGestureRecogniser (List<PanelContainer> panelContainers, UITouchEventArgs shouldReceiveTouch, UIViewController slidingController)
			: base(panelContainers, shouldReceiveTouch, slidingController)
		{

		}

		/// <summary>
		/// Manages what happens when the user begins a possible slide 
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			if (CurrentActivePanelContainer == null)
				return;

			CurrentActivePanelContainer.View.Superview.BringSubviewToFront(CurrentActivePanelContainer.View);
		}

		/// <summary>
		/// Manages what happens while the user is mid-slide
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesMoved (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			//FMT: we shouldn't call base class otherwise it will screw the original frame
			//base.TouchesMoved (touches, evt);

			if (CurrentActivePanelContainer == null)
			{
				return;
			}

			PointF touchPt;
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null) 
			{
				touchPt = touch.LocationInView (this.View);
			}
			else
			{
				return;
			}

			RectangleF newFrame = CurrentActivePanelContainer.Sliding(touchPt, SlidingController.View.Frame);
			CurrentActivePanelContainer.View.Frame = newFrame;
		}
	}
}

