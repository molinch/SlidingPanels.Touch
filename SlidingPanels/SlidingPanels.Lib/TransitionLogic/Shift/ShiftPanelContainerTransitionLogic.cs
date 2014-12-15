using System;
using UIKit;
using System.Drawing;
using SlidingPanels.Lib.PanelContainers;

namespace SlidingPanels.Lib.TransitionLogic.Shift
{
	/// <summary>
	/// Base abstract class giving the transition logic for panel containers when using the "shifting" layout.
	/// </summary>
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

		public override void ResizeContainer (PanelContainer container)
		{
			container.PanelVC.View.Frame = this.GetPanelPosition(container.View, container.Size);
		}

		public override void RotateContainer (PanelContainer container)
		{
			ResizeContainer(container);
		}
	}
}

