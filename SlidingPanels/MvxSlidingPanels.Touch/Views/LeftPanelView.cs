using System;
using SlidingPanels.Lib;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;

namespace MvxSlidingPanels.Touch.Views
{
    public class LeftPanelView : MvxViewController, ILeftPanelView
    {
		#region IPanelView implementation

		public event EventHandler TopViewSwapped;

		public void RefreshContent ()
		{
		}

		public System.Drawing.SizeF Size
		{
			get
			{
				// This panel will appear on the left side.  The associated container doesn't
				// care about the height so we set it to an arbitrary value of -1.
				return new System.Drawing.SizeF (250, -1);
			}
		}

		#endregion

        public LeftPanelView ()
        {
        }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.Red;

		}
    }
}
