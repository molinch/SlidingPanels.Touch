using System;
using MonoTouch.UIKit;
using System.Linq;
using SlidingPanels.Lib.Tools;
using UIImageEffects;
using System.Drawing;

namespace SlidingPanels.Lib.PanelContainers
{
	public class BlurryRightPanelContainer: OverlappingRightPanelContainer
	{
		private UIView backgroundShifter;
		private UIImageView blurryBackground;

		/// <summary>
		/// Initializes a new instance of the <see cref="SlidingPanels.Lib.PanelContainers.RightPanelContainer" /> class.
		/// </summary>
		/// <param name="panel">Panel.</param>
		public BlurryRightPanelContainer(UIViewController panel)
			: base(panel)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.ClipsToBounds = true;

			backgroundShifter = new UIView(new RectangleF(new PointF(0, 0), View.Frame.Size));
			View.Add(backgroundShifter);
			backgroundShifter.Superview.SendSubviewToBack(backgroundShifter);
		}

		public override void Show()
		{
			base.Show();
			GenerateTransluency();
			backgroundShifter.Frame = new RectangleF(new PointF(0, 0), View.Frame.Size);
		}

		public override void SlidingStarted(PointF touchPosition, RectangleF topViewCurrentFrame)
		{
			base.SlidingStarted(touchPosition, topViewCurrentFrame);
			GenerateTransluency();
		}

		/// <summary>
		/// Called while the user is sliding this Panel
		/// </summary>
		/// <param name="touchPosition">Touch position.</param>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public override RectangleF Sliding(PointF touchPosition, RectangleF topViewCurrentFrame)
		{
			var frame = base.Sliding(touchPosition, topViewCurrentFrame);

			if (blurryBackground != null) {
				backgroundShifter.Frame = new RectangleF(
					0 - frame.X, 0, topViewCurrentFrame.Width, topViewCurrentFrame.Height
				);
			}

			return frame;
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
	}
}

