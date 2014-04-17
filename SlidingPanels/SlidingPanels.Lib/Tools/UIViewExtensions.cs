using MonoTouch.UIKit;

namespace SlidingPanels.Lib.Tools
{
	public static class UIViewExtensions
	{
		public static UIImage MakeSnapShot(this UIView view)
		{
			UIGraphics.BeginImageContext(view.Frame.Size);
			try {
				view.DrawViewHierarchy(view.Frame, true);
				return UIGraphics.GetImageFromCurrentImageContext();
			} finally {
				UIGraphics.EndImageContext();
			}
		}
	}
}