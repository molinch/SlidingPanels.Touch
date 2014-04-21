using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace SlidingPanels.Lib.TransitionLogic.Shift
{
	public abstract class ShiftPanelContainerTransitionLogic: PanelContainerTransitionLogic
	{
		/// <summary>
		/// Makes this Panel visible
		/// </summary>
		public override void Show (UIView contentView)
		{
			base.Show (contentView);
			contentView.Layer.ZPosition = -1;
			contentView.Hidden = false;
		}

		public override void Hide (UIView contentView)
		{
			base.Hide (contentView);
			contentView.Hidden = true;
		}

		public override void ResizeViews (UIView contentView, UIView panelView, SizeF panelSize)
		{
			panelView.Frame = this.GetPanelPosition(contentView, panelSize);
		}
	}
}

