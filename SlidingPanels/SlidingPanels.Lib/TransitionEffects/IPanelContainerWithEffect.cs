using System;
using System.Drawing;

namespace SlidingPanels.Lib.TransitionEffects
{
	public interface IPanelContainerWithEffect
	{
		void CustomizeContainer();

		void ShowContainer();

		void SlidingStarted(PointF touchPosition, RectangleF topViewCurrentFrame);

		/// <summary>
		/// Called while the user is sliding this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		RectangleF Sliding(PointF touchPosition, RectangleF topViewCurrentFrame, RectangleF containerNewFrame);

		/// <summary>
		/// Determines if a slide is complete
		/// </summary>
		/// <returns><c>true</c>, if sliding has ended, <c>false</c> otherwise.</returns>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		void SlidingEnded (PointF touchPosition, RectangleF topViewCurrentFrame, bool showing);
	}
}

