using System;
using System.Drawing;
using MonoTouch.UIKit;
using SlidingPanels.Lib.PanelContainers;

namespace SlidingPanels.Lib.TransitionLogic
{
	/// <summary>
	/// Base abstract class giving the transition logic for panel containers.
	/// </summary>
	public abstract class PanelContainerTransitionLogic
	{
		#region Constants

		/// <summary>
		/// Designates the edge tolerance in pts.  This defaults to 40 pts.
		/// </summary>
		private float _edgeTolerance = 40F;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the distance from the Left, right or bottom edge of screen 
		/// which is sensitive to sliding gestures (in pts).
		/// </summary>
		/// <value>The edge tolerance.</value>
		public virtual float EdgeTolerance {
			get {
				return _edgeTolerance;
			}
			set {
				_edgeTolerance = value;
			}
		}

		#endregion

		#region Position Methods

		/// <summary>
		/// Returns a rectangle representing the location and size of the top view 
		/// when this Panel is hidden
		/// </summary>
		/// <returns>The top view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public virtual RectangleF GetTopViewPositionWhenSliderIsHidden(RectangleF containerViewFrame, RectangleF topViewCurrentFrame, SizeF panelSize) {
			throw new NotImplementedException("Implement the method in your child class if this position is needed.");
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the top view 
		/// when this Panel is showing
		/// </summary>
		/// <returns>The top view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public virtual RectangleF GetTopViewPositionWhenSliderIsVisible(RectangleF containerViewFrame, RectangleF topViewCurrentFrame, SizeF panelSize) {
			throw new NotImplementedException("Implement the method in your child class if this position is needed.");
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the container view 
		/// when this Panel is showing
		/// </summary>
		/// <returns>The container view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public virtual RectangleF GetContainerViewPositionWhenSliderIsHidden(RectangleF containerViewFrame, RectangleF topViewCurrentFrame, SizeF panelSize) {
			throw new NotImplementedException("Implement the method in your child class if this position is needed.");
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the container view 
		/// when this Panel is hidden
		/// </summary>
		/// <returns>The container view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public virtual RectangleF GetContainerViewPositionWhenSliderIsVisible(RectangleF containerViewFrame, RectangleF topViewCurrentFrame, SizeF panelSize) {
			throw new NotImplementedException("Implement the method in your child class if this position is needed.");
		}

		/// <summary>
		/// Gets the panel position.
		/// </summary>
		/// <value>The panel position.</value>
		public abstract RectangleF GetPanelPosition(UIView contentView, SizeF panelSize);

		#endregion

		#region Sliding Methods

		public abstract bool SlidingToHideAllowed(PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize);

		/// <summary>
		/// Determines whether this instance can start sliding given the touch position and the 
		/// current location/size of the top view. 
		/// Note that touchPosition is in Screen coordinate.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view's current frame.</param>
		public abstract bool SlidingToShowAllowed(PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize);

		/// <summary>
		/// Called when sliding has started on this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public abstract void SlidingStarted(PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize);

		/// <summary>
		/// Called while the user is sliding this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public abstract RectangleF Sliding(PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize);

		/// <summary>
		/// Determines if a slide is complete
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public abstract bool SlidingEnded(PointF touchPosition, RectangleF topViewCurrentFrame, UIView contentView, SizeF panelSize);

		#endregion

		#region Appearance Methods

		public bool IsVisible {
			get;
			set;
		}

		/// <summary>
		/// Makes this Panel visible
		/// </summary>
		public virtual void Show(PanelContainer container) {
			IsVisible = true;
		}

		/// <summary>
		/// Hides this Panel
		/// </summary>
		public virtual void Hide (PanelContainer container) {
			IsVisible = false;
		}

		#endregion

		public abstract void ResizeContainer (PanelContainer container);

		public abstract void RotateContainer(PanelContainer container);
	}
}

