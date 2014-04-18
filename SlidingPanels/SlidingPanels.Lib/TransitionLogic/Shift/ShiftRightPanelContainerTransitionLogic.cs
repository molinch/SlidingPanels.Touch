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
	/// Container class for Sliding Panels located on the right edge of the device screen
	/// </summary>
	public class ShiftRightPanelContainerTransitionLogic : ShiftPanelContainerTransitionLogic
	{
		#region Data Members

		/// <summary>
		/// starting X Coordinate of the top view
		/// </summary>
		protected float _topViewStartXPosition = 0.0f;

		/// <summary>
		/// X coordinate where the user touched when starting a slide operation
		/// </summary>
		protected float _touchPositionStartXPosition = 0.0f;

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
				X = contentView.Bounds.Width - panelSize.Width,
				Y = -contentView.Frame.Y,
				Width = panelSize.Width,
				Height = contentView.Bounds.Height
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
			topViewCurrentFrame.X = - panelSize.Width;
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
			topViewCurrentFrame.X = 0;
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
		public override bool SlidingAllowed(PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize)
		{
			return (touchPosition.X >= contentView.Bounds.Size.Width - EdgeTolerance && touchPosition.X <= contentView.Bounds.Size.Width);
		}

		/// <summary>
		/// Called when sliding has started on this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override void SlidingStarted (PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize)
		{
			_touchPositionStartXPosition = touchPosition.X;
			_topViewStartXPosition = topViewCurrentFrame.X;
		}

		/// <summary>
		/// Called while the user is sliding this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override RectangleF Sliding (PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize)
		{
			float screenWidth = contentView.Bounds.Size.Width;
			float panelWidth = panelSize.Width;
			float leftEdge = screenWidth - panelWidth;
			float translation = touchPosition.X - _touchPositionStartXPosition;

			RectangleF frame = topViewCurrentFrame;

			frame.X = _topViewStartXPosition + translation;

			float y = frame.X + frame.Width;

			if (y >= screenWidth) 
			{ 
				frame.X = 0; 
			}

			if (y <= leftEdge) 
			{ 
				frame.X = leftEdge - frame.Width; 
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
			float screenWidth = contentView.Bounds.Size.Width;
			float panelWidth = panelSize.Width;

			float y = topViewCurrentFrame.X + topViewCurrentFrame.Width;
			return (y < (screenWidth - (panelWidth / 2)));
		}

		#endregion
	}
}

