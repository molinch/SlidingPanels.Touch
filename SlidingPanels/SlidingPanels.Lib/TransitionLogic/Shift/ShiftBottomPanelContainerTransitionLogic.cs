/// Copyright (C) 2013 Pat Laplante & Frank Caico
///
///	Permission is hereby granted, free of charge, to  any person obtaining a copy 
/// of this software and associated documentation files (the "Software"), to deal 
/// in the Software without  restriction, including without limitation the rights 
/// to use, copy,  modify,  merge, publish,  distribute,  sublicense, and/or sell 
/// copies of the  Software,  and  to  permit  persons  to   whom the Software is 
/// furnished to do so, subject to the following conditions:
///
///		The above  copyright notice  and this permission notice shall be included 
///     in all copies or substantial portions of the Software.
///
///		THE  SOFTWARE  IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
///     OR   IMPLIED,   INCLUDING  BUT   NOT  LIMITED   TO   THE   WARRANTIES  OF 
///     MERCHANTABILITY,  FITNESS  FOR  A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
///     IN NO EVENT SHALL  THE AUTHORS  OR COPYRIGHT  HOLDERS  BE  LIABLE FOR ANY 
///     CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT 
///     OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION  WITH THE SOFTWARE OR 
///     THE USE OR OTHER DEALINGS IN THE SOFTWARE.
/// -----------------------------------------------------------------------------

using System;
using MonoTouch.UIKit;
using System.Drawing;
using SlidingPanels.Lib.Layouts;
using SlidingPanels.Lib.PanelContainers;

namespace SlidingPanels.Lib.TransitionLogic.Shift
{
	/// <summary>
	/// Transition logic for the bottom panel container when using the "shifting" layout.
	/// </summary>
	public class ShiftBottomPanelContainerTransitionLogic : ShiftPanelContainerTransitionLogic
	{
		#region Data Members

		/// <summary>
		/// starting Y Coordinate of the top view
		/// </summary>
		private float _topViewStartYPosition = 0.0f;

		/// <summary>
		/// Y coordinate where the user touched when starting a slide operation
		/// </summary>
		private float _touchPositionStartYPosition = 0.0f;

		#endregion

		#region Position Methods

		/// <summary>
		/// Gets the panel position.
		/// </summary>
		/// <value>The panel position.</value>
		public override RectangleF GetPanelPosition(UIView contentView, SizeF panelSize)
		{
			return new RectangleF 
			{
				X = 0,
				Y = contentView.Frame.Height - contentView.Frame.Y - panelSize.Height,
				Height = panelSize.Height,
				Width = contentView.Bounds.Width
			};
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the top view 
		/// when this Panel is showing
		/// </summary>
		/// <returns>The top view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override RectangleF GetTopViewPositionWhenSliderIsVisible(RectangleF containerViewFrame, RectangleF topViewCurrentFrame, SizeF panelSize)
		{
			topViewCurrentFrame.Y = containerViewFrame.Height - topViewCurrentFrame.Height - panelSize.Height;
			return topViewCurrentFrame;
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the top view 
		/// when this Panel is hidden
		/// </summary>
		/// <returns>The top view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override RectangleF GetTopViewPositionWhenSliderIsHidden(RectangleF containerViewFrame, RectangleF topViewCurrentFrame, SizeF panelSize)
		{
			topViewCurrentFrame.Y = 0;
			return topViewCurrentFrame;
		}

		#endregion 

		#region Sliding Methods

		/// <summary>
		/// Determines whether this instance can start sliding given the touch position and the 
		/// current location/size of the top view. 
		/// Note that touchPosition is in Screen coordinate.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view's current frame.</param>
		public override bool SlidingToShowAllowed(PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize)
		{
			return (touchPosition.Y >= (contentView.Bounds.Height - EdgeTolerance) && touchPosition.Y <= contentView.Bounds.Height);
		}

		public override bool SlidingToHideAllowed(PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize)
		{
			// TODO FMT: to be implemented...
			return true;
		}

		/// <summary>
		/// Called when sliding has started on this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override void SlidingStarted (PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize)
		{
			_touchPositionStartYPosition = touchPosition.Y;
			_topViewStartYPosition = topViewCurrentFrame.Y;
		}

		/// <summary>
		/// Called while the user is sliding this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override RectangleF Sliding (PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize)
		{
			float translation = touchPosition.Y - _touchPositionStartYPosition;

			RectangleF frame = topViewCurrentFrame;
			frame.Y = _topViewStartYPosition + translation;

			if (frame.Y >= 0) 
			{ 
				frame.Y = 0; 
			}
			else if (frame.Y <= (contentView.Bounds.Height - topViewCurrentFrame.Height - panelSize.Height))
			{
				frame.Y = contentView.Bounds.Height - topViewCurrentFrame.Height - panelSize.Height;
			}
			return frame;
		}

		/// <summary>
		/// Determines if a slide is complete
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override bool SlidingEnded (PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize)
		{
			// touchPosition will be in View coordinate, and will be adjusted to account
			// for the nav bar if visible.

			float screenHeight = topViewCurrentFrame.Height;
			float panelHeight = panelSize.Height;

			float y = topViewCurrentFrame.Y + topViewCurrentFrame.Height;
			return (y < (screenHeight - (panelHeight / 2)));
		}

		#endregion
	}
}

