﻿using System;
using SlidingPanels.Lib.PanelContainers;
using System.Drawing;
using UIKit;
using SlidingPanels.Lib.TransitionLogic;
using CoreGraphics;

namespace SlidingPanels.Lib.Layouts
{
	/// <summary>
	/// Contract to implement a custom layout for the sliding panels.
	/// </summary>
	public interface ISlidingLayout
	{
		#region Handle touch gestures

		void SlidingGestureBegan(UIViewController slidingController, PanelContainer container, CGPoint touchPt);

		void SlidingGestureMoved(UIViewController slidingController, PanelContainer container, CGPoint touchPt);

		void SlidingGestureEnded(UIViewController slidingController, PanelContainer container, CGPoint touchPt);

		#endregion

		#region Handle panels

		PanelContainerTransitionLogic CreateTransitionLogic(PanelType panelType);

		/// <summary>
		///     Insert a panel in the view hierarchy.  If this is done early in
		///     the creation process,  we postponed adding  until later, at one
		///     point we are guarantee that Superview is not null.
		/// </summary>
		/// <param name="container">Container.</param>
		void WhenPanelInserted(UIViewController slidingController, PanelContainer container);

		void WhenPanelStartsShowing(PanelContainer container, UIInterfaceOrientation orientation);

		void CompleteShowPanel(UIViewController slidingController, PanelContainer container, Action onComplete);

		void CompleteHidePanel(UIViewController slidingController, PanelContainer container, Action onComplete);

		#endregion
	}
}

