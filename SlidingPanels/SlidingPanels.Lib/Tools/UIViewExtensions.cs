using MonoTouch.UIKit;
using System.Drawing;

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

		public static UIImage MakeSnapShot(this UIView view, RectangleF snapshotFrame)
		{
			UIGraphics.BeginImageContext(snapshotFrame.Size);
			try {
				view.DrawViewHierarchy(snapshotFrame, false);
				return UIGraphics.GetImageFromCurrentImageContext();
			} finally {
				UIGraphics.EndImageContext();
			}
		}
	}
}