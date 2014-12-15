using System.Drawing;
using System.Linq;
using UIKit;
using SlidingPanels.Lib.PanelContainers;
using SlidingPanels.Lib.Layouts;
using CoreGraphics;
using System;

namespace SlidingPanels.Lib.TransitionLogic.Overlap
{
	/// <summary>
	/// Transition logic for the right panel container when using the "overlapping" layout.
	/// </summary>
	public class OverlapRightPanelContainerTransitionLogic : OverlapPanelContainerTransitionLogic
	{
		#region Data Members

		/// <summary>
		/// X coordinate where the user touched when starting a slide operation
		/// </summary>
		protected nfloat _touchPositionStartXPosition = 0.0f;

		#endregion

		/// <summary>
		/// Gets the panel position.
		/// </summary>
		/// <value>The panel position.</value>
		public override CGRect GetPanelPosition(UIView contentView, CGSize panelSize)
		{
			return new CGRect 
			{
				X = 0,
				Y = 0,
				Width = panelSize.Width,
				Height = contentView.Bounds.Height
			};
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the container view 
		/// when this Panel is showing
		/// </summary>
		/// <returns>The container view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override CGRect GetContainerViewPositionWhenSliderIsHidden(CGRect containerFrame, CGRect topViewCurrentFrame, CGSize panelSize) {
			containerFrame.X = topViewCurrentFrame.Width;
			return containerFrame;
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the container view 
		/// when this Panel is hidden
		/// </summary>
		/// <returns>The container view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override CGRect GetContainerViewPositionWhenSliderIsVisible(CGRect containerFrame, CGRect topViewCurrentFrame, CGSize panelSize) {
			containerFrame.X = topViewCurrentFrame.Width - panelSize.Width;
			return containerFrame;
		}

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
			return (touchPosition.X >= topViewCurrentFrame.Size.Width - EdgeTolerance) && (touchPosition.X <= topViewCurrentFrame.Size.Width);
		}

		public override bool SlidingToHideAllowed(CGPoint touchPosition, CGRect topViewCurrentFrame, UIView contentView, CGSize panelSize)
		{
			return (touchPosition.X >= topViewCurrentFrame.Size.Width - panelSize.Width - EdgeTolerance) && (touchPosition.X <= topViewCurrentFrame.Size.Width - panelSize.Width + EdgeTolerance);
		}

		/// <summary>
		/// Called when sliding has started on this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override void SlidingStarted (CGPoint touchPosition, CGRect topViewCurrentFrame, UIView contentView, CGSize panelSize)
		{
			_touchPositionStartXPosition = touchPosition.X;
		}

		/// <summary>
		/// Called while the user is sliding this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override CGRect Sliding (CGPoint touchPosition, CGRect topViewCurrentFrame, UIView contentView, CGSize panelSize)
		{
			var panelWidth = panelSize.Width;
			var x = touchPosition.X;

			if (x < (topViewCurrentFrame.Width - panelWidth))
				x = topViewCurrentFrame.Width - panelWidth;
			if (x > topViewCurrentFrame.Width)
				x = topViewCurrentFrame.Width;

			CGRect frame = contentView.Frame;
			frame.X = x;
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
			var screenWidth = topViewCurrentFrame.Width;
			var panelWidth = panelSize.Width;
			var visibleWidth = screenWidth - contentView.Frame.X;
			return visibleWidth > (panelWidth / 2);
		}
	}
}