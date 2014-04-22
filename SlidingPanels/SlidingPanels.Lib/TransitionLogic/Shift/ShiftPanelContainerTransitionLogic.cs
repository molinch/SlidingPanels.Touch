using System;
using MonoTouch.UIKit;
using System.Drawing;
using SlidingPanels.Lib.PanelContainers;

namespace SlidingPanels.Lib.TransitionLogic.Shift
{
	public abstract class ShiftPanelContainerTransitionLogic: PanelContainerTransitionLogic
	{
		/// <summary>
		/// Makes this Panel visible
		/// </summary>
		public override void Show (PanelContainer container)
		{
			base.Show (container);
			container.View.Layer.ZPosition = -1;
			container.View.Hidden = false;
		}

		public override void Hide (PanelContainer container)
		{
			base.Hide (container);
			container.View.Hidden = true;
		}

		public override void ResizeViews (PanelContainer container)
		{
			container.PanelVC.View.Frame = this.GetPanelPosition(container.View, container.Size);
		}

		public override void RotateView (PanelContainer container)
		{
			ResizeViews(container);
		}
	}
}

