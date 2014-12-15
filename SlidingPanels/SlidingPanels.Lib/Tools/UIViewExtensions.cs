using UIKit;
using System.Drawing;
using CoreGraphics;

namespace SlidingPanels.Lib.Tools
{
	/// <summary>
	/// UIView extensions
	/// </summary>
	public static class UIViewExtensions
	{
		public static UIImage MakeSnapShot(this UIView view)
		{
			return MakeSnapShot(view, view.Frame);
		}

		public static UIImage MakeSnapShot(this UIView view, CGRect snapshotFrame)
		{
			UIGraphics.BeginImageContextWithOptions(snapshotFrame.Size, true, 1f); // opaque and smaller scale to consume less memory
			try {
				view.DrawViewHierarchy(snapshotFrame, false);
				return UIGraphics.GetImageFromCurrentImageContext();
			} finally {
				UIGraphics.EndImageContext();
			}
		}
	}
}