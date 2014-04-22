﻿using System;
using MonoTouch.UIKit;
using System.Linq;
using SlidingPanels.Lib.Tools;
using UIImageEffects;
using System.Drawing;
using SlidingPanels.Lib.PanelContainers;
using System.Threading.Tasks;

namespace SlidingPanels.Lib.TransitionEffects
{
	public class BlurryContainer: IPanelContainerWithEffect
	{
		private readonly PanelContainer container;
		private UIView backgroundShifter;
		private UIImageView blurryBackground;

		public BlurryContainer(PanelContainer container)
		{
			this.container = container;
		}

		public void CustomizeContainer()
		{
			backgroundShifter = new UIView(new RectangleF(new PointF(0, 0), container.View.Frame.Size)) {
				Opaque = true
			};
			blurryBackground = new UIImageView(new RectangleF(0, 0, WindowState.CurrentScreenWidth, WindowState.CurrentScreenHeight)) {
				Opaque = true
			};
			backgroundShifter.Add(blurryBackground);

			container.View.ClipsToBounds = true;
			container.View.Add(backgroundShifter);
			container.View.SendSubviewToBack(backgroundShifter);

			GenerateTranslucency ();
		}

		public void ShowContainer()
		{
			if (!HasBackground || container.IsVisible)
				return;

			GenerateTranslucency ();

			var resetFrame = new RectangleF (
				0, 0, WindowState.CurrentScreenWidth, WindowState.CurrentScreenHeight
			);
			blurryBackground.Frame = resetFrame;

			backgroundShifter.Frame = GetBackgroundShifterEndFrame (resetFrame); // FMT: we are supposed to pass topViewCurrentFrame here :/
		}

		public void SlidingStarted(PointF touchPosition, RectangleF topViewCurrentFrame)
		{
			if (container.View.Frame == container.GetContainerViewPositionWhenSliderIsVisible(topViewCurrentFrame))
				return;

			GenerateTranslucency();
		}

		/// <summary>
		/// Called while the user is sliding this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public RectangleF Sliding(PointF touchPosition, RectangleF topViewCurrentFrame, RectangleF containerNewFrame)
		{
			if (HasBackground) {
				backgroundShifter.Frame = new RectangleF(
					0 - containerNewFrame.X, 0, WindowState.CurrentScreenWidth, WindowState.CurrentScreenHeight
				);
			}

			return containerNewFrame;
		}

		/// <summary>
		/// Determines if a slide is complete
		/// </summary>
		/// <returns><c>true</c>, if sliding has ended, <c>false</c> otherwise.</returns>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public void SlidingEnded (PointF touchPosition, RectangleF topViewCurrentFrame, bool showing) {
			if (!HasBackground || !showing)
				return;

			backgroundShifter.Frame = GetBackgroundShifterEndFrame (topViewCurrentFrame);
		}

		private RectangleF GetBackgroundShifterEndFrame(RectangleF topViewCurrentFrame) {
			bool isLeftPanel = container.PanelType == PanelType.LeftPanel;
			return new RectangleF(
				isLeftPanel ? 0 : (0 - topViewCurrentFrame.Width + container.Size.Width),
				0,
				WindowState.CurrentScreenWidth, WindowState.CurrentScreenHeight
			);
		}

		private void GenerateTranslucency()
		{
			var displayedController = CurrentController;
			if (displayedController == null)
				return;

			var view = displayedController.View;
			var viewBackground = view.MakeSnapShot(new RectangleF(0, 0, WindowState.CurrentScreenWidth, WindowState.CurrentScreenHeight));
			var blurredImage = viewBackground.ApplyLightEffect();
			blurryBackground.Image = blurredImage;
		}

		private UIViewController CurrentController
		{
			get
			{
				var window = UIApplication.SharedApplication.KeyWindow ?? UIApplication.SharedApplication.Windows[0];
				var navController = window.RootViewController.ChildViewControllers[0];
				return navController.ChildViewControllers.LastOrDefault();
			}
		}

		private bool HasBackground {
			get {
				return blurryBackground != null && blurryBackground.Image != null;
			}
		}

		#region Handle width/height independent of the orientation

		#endregion
	}
}

