using System;
using System.Drawing;
using UIKit;
using SlidingPanels.Lib.TransitionEffects;
using SlidingPanels.Lib.PanelContainers;
using CoreGraphics;

namespace SlidingPanels.Lib.TransitionLogic
{
	/// <summary>
	/// Wrapper containing the transition logic for a panel container plus an additional UI effect (like translucency). 
	/// </summary>
	public class EffectTransitionLogic : PanelContainerTransitionLogic
	{
		private readonly PanelContainerTransitionLogic transitionLogic;
		private readonly IPanelContainerWithEffect effect;
		private bool containerCustomized = false;

		public EffectTransitionLogic(PanelContainerTransitionLogic transitionLogic, IPanelContainerWithEffect effect) {
			this.transitionLogic = transitionLogic;
			this.effect = effect;
		}

		#region Position Methods

		/// <summary>
		/// Returns a rectangle representing the location and size of the top view 
		/// when this Panel is hidden
		/// </summary>
		/// <returns>The top view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override CGRect GetTopViewPositionWhenSliderIsHidden(CGRect containerViewFrame, CGRect topViewCurrentFrame, CGSize panelSize) {
			return transitionLogic.GetTopViewPositionWhenSliderIsHidden(containerViewFrame, topViewCurrentFrame, panelSize);
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the top view 
		/// when this Panel is showing
		/// </summary>
		/// <returns>The top view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override CGRect GetTopViewPositionWhenSliderIsVisible(CGRect containerViewFrame, CGRect topViewCurrentFrame, CGSize panelSize) {
			return transitionLogic.GetTopViewPositionWhenSliderIsVisible(containerViewFrame, topViewCurrentFrame, panelSize);
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the container view 
		/// when this Panel is showing
		/// </summary>
		/// <returns>The container view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override CGRect GetContainerViewPositionWhenSliderIsHidden(CGRect containerViewFrame, CGRect topViewCurrentFrame, CGSize panelSize) {
			return transitionLogic.GetContainerViewPositionWhenSliderIsHidden(containerViewFrame, topViewCurrentFrame, panelSize);
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the container view 
		/// when this Panel is hidden
		/// </summary>
		/// <returns>The container view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override CGRect GetContainerViewPositionWhenSliderIsVisible(CGRect containerViewFrame, CGRect topViewCurrentFrame, CGSize panelSize) {
			return transitionLogic.GetContainerViewPositionWhenSliderIsVisible(containerViewFrame, topViewCurrentFrame, panelSize);
		}

		/// <summary>
		/// Gets the panel position.
		/// </summary>
		/// <value>The panel position.</value>
		public override CGRect GetPanelPosition(UIView contentView, CGSize panelSize) {
			return transitionLogic.GetPanelPosition(contentView, panelSize);
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
			return transitionLogic.SlidingToShowAllowed(touchPosition, topViewCurrentFrame, contentView, panelSize);
		}

		public override bool SlidingToHideAllowed(CGPoint touchPosition, CGRect topViewCurrentFrame, UIView contentView, CGSize panelSize)
		{
			return transitionLogic.SlidingToHideAllowed(touchPosition, topViewCurrentFrame, contentView, panelSize);
		}

		/// <summary>
		/// Called when sliding has started on this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override void SlidingStarted(CGPoint touchPosition, CGRect topViewCurrentFrame, UIView contentView, CGSize panelSize)
		{
			effect.SlidingStarted(touchPosition, topViewCurrentFrame);
			transitionLogic.SlidingStarted(touchPosition, topViewCurrentFrame, contentView, panelSize);
		}

		/// <summary>
		/// Called while the user is sliding this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override CGRect Sliding(CGPoint touchPosition, CGRect topViewCurrentFrame, UIView contentView, CGSize panelSize)
		{
			var newFrame = transitionLogic.Sliding(touchPosition, topViewCurrentFrame, contentView, panelSize);
			effect.Sliding(touchPosition, topViewCurrentFrame, newFrame);
			return newFrame;
		}

		/// <summary>
		/// Determines if a slide is complete
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override bool SlidingEnded(CGPoint touchPosition, CGRect topViewCurrentFrame, UIView contentView, CGSize panelSize)
		{
			bool ended = transitionLogic.SlidingEnded(touchPosition, topViewCurrentFrame, contentView, panelSize);
			effect.SlidingEnded(touchPosition, topViewCurrentFrame, ended);
			return ended;
		}

		#endregion

		#region Appearance Methods

		/// <summary>
		/// Makes this Panel visible
		/// </summary>
		public override void Show(PanelContainer container) {
			effect.ShowContainer();
			transitionLogic.Show(container);
			base.Show (container);
		}

		/// <summary>
		/// Hides this Panel
		/// </summary>
		public override void Hide (PanelContainer container)
		{
			transitionLogic.Hide(container);
			base.Hide (container);
		}

		#endregion

		public override void WhenInserted(UIViewController slidingController, PanelContainer container) {
			if (!containerCustomized) {
				effect.CustomizeContainer(slidingController);
				containerCustomized = true;
			}
		}

		public override void ResizeContainer (PanelContainer container, CGSize screenSize)
		{
			transitionLogic.ResizeContainer (container, screenSize);
		}

		public override void RotateContainer (PanelContainer container, CGSize screenSize)
		{
			transitionLogic.RotateContainer (container, screenSize);
		}
	}
}

