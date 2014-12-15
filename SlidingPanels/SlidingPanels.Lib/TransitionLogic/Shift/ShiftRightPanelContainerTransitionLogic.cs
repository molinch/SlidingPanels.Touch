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
using UIKit;
using System.Drawing;
using SlidingPanels.Lib.Layouts;
using SlidingPanels.Lib.PanelContainers;
using CoreGraphics;

namespace SlidingPanels.Lib.TransitionLogic.Shift
{
	/// <summary>
	/// Transition logic for the right panel container when using the "shifting" layout.
	/// </summary>
	public class ShiftRightPanelContainerTransitionLogic : ShiftPanelContainerTransitionLogic
	{
		#region Data Members

		/// <summary>
		/// starting X Coordinate of the top view
		/// </summary>
		protected nfloat _topViewStartXPosition = 0.0f;

		/// <summary>
		/// X coordinate where the user touched when starting a slide operation
		/// </summary>
		protected nfloat _touchPositionStartXPosition = 0.0f;

		#endregion

		#region Position Methods

		/// <summary>
		/// Gets the panel position.
		/// </summary>
		/// <value>The panel position.</value>
		public override CGRect GetPanelPosition(UIView contentView, CGSize panelSize)
		{
			return new CGRect 
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
		public override CGRect GetTopViewPositionWhenSliderIsVisible(CGRect containerViewFrame, CGRect topViewCurrentFrame, CGSize panelSize)
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
		public override CGRect GetTopViewPositionWhenSliderIsHidden(CGRect containerViewFrame, CGRect topViewCurrentFrame, CGSize panelSize)
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
		public override bool SlidingToShowAllowed(CGPoint touchPosition, CGRect topViewCurrentFrame, UIView contentView, CGSize panelSize)
		{
			return (touchPosition.X >= contentView.Bounds.Size.Width - EdgeTolerance && touchPosition.X <= contentView.Bounds.Size.Width);
		}

		public override bool SlidingToHideAllowed(CGPoint touchPosition, CGRect topViewCurrentFrame, UIView contentView, CGSize panelSize)
		{
			// TODO FMT: to be implemented...
			return true;
		}

		/// <summary>
		/// Called when sliding has started on this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override void SlidingStarted (CGPoint touchPosition, CGRect topViewCurrentFrame, UIView contentView, CGSize panelSize)
		{
			_touchPositionStartXPosition = touchPosition.X;
			_topViewStartXPosition = topViewCurrentFrame.X;
		}

		/// <summary>
		/// Called while the user is sliding this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override CGRect Sliding (CGPoint touchPosition, CGRect topViewCurrentFrame, UIView contentView, CGSize panelSize)
		{
			var screenWidth = contentView.Bounds.Size.Width;
			var panelWidth = panelSize.Width;
			var leftEdge = screenWidth - panelWidth;
			var translation = touchPosition.X - _touchPositionStartXPosition;

			CGRect frame = topViewCurrentFrame;

			frame.X = _topViewStartXPosition + translation;

			var y = frame.X + frame.Width;

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
		public override bool SlidingEnded (CGPoint touchPosition, CGRect topViewCurrentFrame, UIView contentView, CGSize panelSize)
		{
			var screenWidth = contentView.Bounds.Size.Width;
			var panelWidth = panelSize.Width;

			var y = topViewCurrentFrame.X + topViewCurrentFrame.Width;
			return (y < (screenWidth - (panelWidth / 2)));
		}

		#endregion
	}
}

