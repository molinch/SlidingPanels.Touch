using System;
using MonoTouch.UIKit;

namespace SlidingPanels.Lib.TransitionLogic.Shift
{
	public abstract class ShiftPanelContainerTransitionLogic: PanelContainerTransitionLogic
	{
		/// <summary>
		/// Makes this Panel visible
		/// </summary>
		public override void Show (UIView contentView)
		{
			contentView.Layer.ZPosition = -1;
			contentView.Hidden = false;
		}
	}
}

