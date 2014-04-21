using System;
using MonoTouch.UIKit;
using System.Linq;
using SlidingPanels.Lib.Tools;
using UIImageEffects;
using System.Drawing;
using SlidingPanels.Lib.PanelContainers;

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
			backgroundShifter = new UIView(new RectangleF(new PointF(0, 0), container.View.Frame.Size));
			container.View.ClipsToBounds = true;
			container.View.Add(backgroundShifter);
			container.View.SendSubviewToBack(backgroundShifter);
			GenerateTransluency ();
		}

		public void ShowContainer()
		{
		}

		public void SlidingStarted(PointF touchPosition, RectangleF topViewCurrentFrame)
		{
			GenerateTransluency();
		}

		/// <summary>
		/// Called while the user is sliding this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public RectangleF Sliding(PointF touchPosition, RectangleF topViewCurrentFrame, RectangleF containerNewFrame)
		{
			if (blurryBackground != null) {
				backgroundShifter.Frame = new RectangleF(
					0 - containerNewFrame.X, 0, topViewCurrentFrame.Width, topViewCurrentFrame.Height
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
		public void SlidingEnded (PointF touchPosition, RectangleF topViewCurrentFrame) {
		}

		private void GenerateTransluency()
		{
			var displayedController = CurrentController;
			if (displayedController == null)
				return;

			var viewBackground = displayedController.View.MakeSnapShot();

			if (blurryBackground != null) {
				blurryBackground.RemoveFromSuperview();
			}
			blurryBackground = new UIImageView(viewBackground.ApplyLightEffect());

			backgroundShifter.Add(blurryBackground);
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

		private UIWindow Window {
			get {
				return UIApplication.SharedApplication.KeyWindow;
			}
		}

		/// <summary>
		/// Indicates if the device is in landscape mode.
		/// </summary>
		/// <value><c>true</c> if the device is in landscape mode; otherwise, <c>false</c>.</value>
		public static bool IsLandscapeOrientation {
			get {
				return UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight;
			}
		}

		/// <summary>
		/// Gives the real current window width which takes care of the current orientation.
		/// </summary>
		/// <value>Real current window width which takes care of the current orientation.</value>
		private float CurrentScreenWidth {
			get {
				if (IsLandscapeOrientation)
					return Math.Max(Window.Bounds.Width, Window.Bounds.Height);
				return Math.Min(Window.Bounds.Width, Window.Bounds.Height);
			}
		}

		/// <summary>
		/// Gives the real current window height which takes care of the current orientation.
		/// </summary>
		/// <value>Real current window height which takes care of the current orientation.</value>
		public float CurrentScreenHeight {
			get {
				if (IsLandscapeOrientation)
					return Math.Min (Window.Bounds.Width, Window.Bounds.Height);
				return Math.Max(Window.Bounds.Width, Window.Bounds.Height);
			}
		}
	}
}

