﻿using System.Drawing;
using System.Linq;
using MonoTouch.UIKit;
using SlidingPanels.Lib.PanelContainers;

namespace SlidingPanels.Lib.PanelContainers
{
	public class OverlappingRightPanelContainer : RightPanelContainer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SlidingPanels.Lib.PanelContainers.RightPanelContainer" /> class.
		/// </summary>
		/// <param name="panel">Panel.</param>
		public OverlappingRightPanelContainer(UIViewController panel)
			: base(panel)
		{
		}

		/// <summary>
		/// Makes this Panel visible
		/// </summary>
		public override void Show ()
		{
			View.Hidden = false;
		}

		/// <summary>
		/// Called while the user is sliding this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override RectangleF Sliding (PointF touchPosition, RectangleF topViewCurrentFrame)
		{
			float screenWidth = View.Bounds.Size.Width;
			float panelWidth = Size.Width;
			float translation = touchPosition.X - _touchPositionStartXPosition;

			RectangleF frame = topViewCurrentFrame;
			frame.X = panelWidth + translation;
			return frame;
		}

		/// <summary>
		/// Determines if a slide is complete
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override bool SlidingEnded (PointF touchPosition, RectangleF topViewCurrentFrame)
		{
			float screenWidth = View.Bounds.Size.Width;
			float panelWidth = Size.Width;
			float visibleWidth = screenWidth - View.Frame.X;
			return visibleWidth > (panelWidth / 2);
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the top view 
		/// when this Panel is showing
		/// </summary>
		/// <returns>The top view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override RectangleF GetTopViewPositionWhenSliderIsVisible(RectangleF topViewCurrentFrame)
		{
			return GetTopViewPositionWhenSliderIsHidden(topViewCurrentFrame);
		}
	}
}