using System.Drawing;
using System.Linq;
using MonoTouch.UIKit;
using SlidingPanels.Lib.PanelContainers;
using SlidingPanels.Lib.Layouts;

namespace SlidingPanels.Lib.TransitionLogic.Overlap
{
	public class OverlapRightPanelContainerTransitionLogic : OverlapPanelContainerTransitionLogic
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
		/// Returns a rectangle representing the location and size of the container view 
		/// when this Panel is showing
		/// </summary>
		/// <returns>The container view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override RectangleF GetContainerViewPositionWhenSliderIsHidden(RectangleF topFrame, RectangleF topViewCurrentFrame, SizeF panelSize) {
			topFrame.X = panelSize.Width;
			return topFrame;
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the container view 
		/// when this Panel is hidden
		/// </summary>
		/// <returns>The container view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override RectangleF GetContainerViewPositionWhenSliderIsVisible(RectangleF topFrame, RectangleF topViewCurrentFrame, SizeF panelSize) {
			topFrame.X = 0;
			return topFrame;
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
			float panelWidth = panelSize.Width;
			float translation = touchPosition.X - _touchPositionStartXPosition;

			RectangleF frame = topViewCurrentFrame;
			if (translation < 0)
				frame.X = panelWidth + translation;
			else
				frame.X = translation;
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
			float visibleWidth = screenWidth - contentView.Frame.X;
			return visibleWidth > (panelWidth / 2);
		}
	}
}