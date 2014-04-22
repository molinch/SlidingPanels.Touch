/// Copyright (C) 2013 Pat Laplante & Frank Caico
///
///	Permission is hereby granted, free of charge, to  any person obtaining a copy 
/// of this software and associated documentation files (the "Software"), to deal 
/// in the Software without  restriction, including without limitation the rights 
/// to use, copy,  modify,  merge, publish,  distribute,  sublicense, and/or sell 
/// copies of the  Software,  and  to  permit  persons  to   whom the Software is 
/// furnished to do so, subject to the following conditions:
///
///		The above  copyright notice  and this permission notice shall be included 
///     in all copies or substantial portions of the Software.
///
///		THE  SOFTWARE  IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
///     OR   IMPLIED,   INCLUDING  BUT   NOT  LIMITED   TO   THE   WARRANTIES  OF 
///     MERCHANTABILITY,  FITNESS  FOR  A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
///     IN NO EVENT SHALL  THE AUTHORS  OR COPYRIGHT  HOLDERS  BE  LIABLE FOR ANY 
///     CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT 
///     OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION  WITH THE SOFTWARE OR 
///     THE USE OR OTHER DEALINGS IN THE SOFTWARE.
/// -----------------------------------------------------------------------------

using System;
using MonoTouch.UIKit;
using System.Drawing;
using SlidingPanels.Lib.Layouts;
using SlidingPanels.Lib.TransitionLogic;
using SlidingPanels.Lib.TransitionEffects;

namespace SlidingPanels.Lib.PanelContainers
{
    /// <summary>
    /// Base class for Left, Right or Bottom Panel Containers
    /// This is an abstract class and cannot be used directly
    /// </summary>
    public class PanelContainer : UIViewController
    {
		#region Properties

		protected ISlidingLayout Layout {
			get;
			private set;
		}

		public PanelContainerTransitionLogic TransitionLogic {
			get;
			private set;
		}

        /// <summary>
        /// Gets the view controller contained inside this panel
        /// </summary>
        /// <value>The panel V.</value>
        public UIViewController PanelVC { 
            get; 
            private set; 
        }

        /// <summary>
        /// Gets the type of the panel (Left, Right or Bottom)
        /// </summary>
        /// <value>The type of the panel.</value>
        public PanelType PanelType { 
            get; 
            private set; 
        }

        /// <summary>
        /// Gets a value indicating whether the panel is currently showing
        /// </summary>
        /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
        public virtual bool IsVisible { 
            get { 
				return TransitionLogic.IsVisible; 
            } 
        }

        /// <summary>
        /// Gets the size of the panel
        /// </summary>
        /// <value>The size.</value>
        public virtual SizeF Size { 
            get; 
            private set; 
        }

        /// <summary>
        /// Gets the distance from the Left, right or bottom edge of screen 
        /// which is sensitive to sliding gestures (in pts).
        /// </summary>
        /// <value>The edge tolerance.</value>
        public virtual float EdgeTolerance {
            get {
				return TransitionLogic.EdgeTolerance;
            }
            set {
				TransitionLogic.EdgeTolerance = value;
            }
        }

		/// <summary>
		/// Gets or sets a value indicating whether this PanelContainer is allowed to slide.
		/// </summary>
		/// <value><c>true</c> if sliding is allowed; otherwise, <c>false</c>.</value>
		public bool SlidingAllowed { get; set; }

        #endregion

        #region Construction / Destruction

        /// <summary>
        /// Initializes a new instance of the <see cref="SlidingPanels.Lib.PanelContainers.PanelContainer"/> class.
        /// </summary>
        /// <param name="panel">Panel.</param>
        /// <param name="panelType">Panel type.</param>
		public PanelContainer (ISlidingLayout layout, UIViewController panel, PanelType panelType,
			Func<PanelContainer, IPanelContainerWithEffect> optionalEffect = null)
        {
			Layout = layout;

			TransitionLogic = layout.CreateTransitionLogic(panelType);
			if (optionalEffect != null) {
				TransitionLogic = new EffectTransitionLogic(TransitionLogic, optionalEffect(this));
			}

            PanelVC = panel;
            PanelType = panelType;
            Size = panel.View.Frame.Size;
			SlidingAllowed = true;
        }

        #endregion

        #region View Lifecycle

        /// <summary>
        /// Called when the panel view is first loaded
        /// </summary>
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            View.Frame = UIScreen.MainScreen.Bounds;

            AddChildViewController (PanelVC);
            View.AddSubview (PanelVC.View);

            Hide ();
			TransitionLogic.ResizeViews(View, PanelVC.View, Size);
        }

        /// <summary>
        /// Called every time the Panel is about to be shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear (bool animated)
        {
			Layout.WhenPanelStartsShowing(this, InterfaceOrientation);
            PanelVC.ViewWillAppear (animated);
            base.ViewWillAppear (animated);
			TransitionLogic.ResizeViews(View, PanelVC.View, Size);
        }

        /// <summary>
        /// Called every time after the Panel is shown
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewDidAppear (bool animated)
        {
            PanelVC.ViewDidAppear (animated);
            base.ViewDidAppear (animated);
        }

        /// <summary>
        /// Called whenever the Panel is about to be hidden
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillDisappear (bool animated)
        {
            PanelVC.ViewWillDisappear (animated);
            base.ViewWillDisappear (animated);
        }

        /// <summary>
        /// Called every time after the Panel is hidden
        /// </summary>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewDidDisappear (bool animated)
        {
            PanelVC.ViewDidDisappear (animated);
            base.ViewDidDisappear (animated);
        }

        #endregion

        #region Visibility Control

        /// <summary>
        /// Toggle the visibility of this panel
        /// </summary>
        public virtual void Toggle ()
        {
			if (!IsVisible) {
                Show ();
            } else {
                Hide ();
            }
        }

        /// <summary>
        /// Makes this Panel visible
        /// </summary>
		public virtual void Show ()
        {
			TransitionLogic.Show(View);
        }

        /// <summary>
        /// Hides this Panel
        /// </summary>
        public virtual void Hide ()
        {
			TransitionLogic.Hide(View);
        }

		public virtual bool CanStartSliding(PointF touchPosition, RectangleF topViewCurrentFrame) {
			if (!SlidingAllowed)
				return false;

			if (IsVisible) {
				if (TransitionLogic.SlidingToHideAllowed(touchPosition, topViewCurrentFrame, View, Size))
					return topViewCurrentFrame.Contains(touchPosition);
			}

			return TransitionLogic.SlidingToShowAllowed(touchPosition, topViewCurrentFrame, View, Size);
		}

        #endregion

		#region Position Methods

		/// <summary>
		/// Returns a rectangle representing the location and size of the top view 
		/// when this Panel is hidden
		/// </summary>
		/// <returns>The top view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public RectangleF GetTopViewPositionWhenSliderIsHidden(RectangleF topViewCurrentFrame) {
			return TransitionLogic.GetTopViewPositionWhenSliderIsHidden(View.Frame, topViewCurrentFrame, Size);
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the top view 
		/// when this Panel is showing
		/// </summary>
		/// <returns>The top view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public RectangleF GetTopViewPositionWhenSliderIsVisible(RectangleF topViewCurrentFrame) {
			return TransitionLogic.GetTopViewPositionWhenSliderIsVisible(View.Frame, topViewCurrentFrame, Size);
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the container view 
		/// when this Panel is showing
		/// </summary>
		/// <returns>The container view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public RectangleF GetContainerViewPositionWhenSliderIsHidden(RectangleF topViewCurrentFrame) {
			return TransitionLogic.GetContainerViewPositionWhenSliderIsHidden(View.Frame, topViewCurrentFrame, Size);
		}

		/// <summary>
		/// Returns a rectangle representing the location and size of the container view 
		/// when this Panel is hidden
		/// </summary>
		/// <returns>The container view position when slider is visible.</returns>
		/// <param name="topViewCurrentFrame">Top view current frame.</param>
		public RectangleF GetContainerViewPositionWhenSliderIsVisible(RectangleF topViewCurrentFrame) {
			return TransitionLogic.GetContainerViewPositionWhenSliderIsVisible(View.Frame, topViewCurrentFrame, Size);
		}

		#endregion

		#region Sliding Methods

        /// <summary>
        /// Called when sliding has started on this Panel
        /// </summary>
        /// <param name="touchPosition">Touch position.</param>
        /// <param name="topViewCurrentFrame">Top view current frame.</param>
		public void SlidingStarted (PointF touchPosition, RectangleF topViewCurrentFrame) {
			TransitionLogic.SlidingStarted(touchPosition, topViewCurrentFrame, View, Size);
		}

        /// <summary>
        /// Called while the user is sliding this Panel
        /// </summary>
        /// <param name="touchPosition">Touch position.</param>
        /// <param name="topViewCurrentFrame">Top view current frame.</param>
		public RectangleF Sliding (PointF touchPosition, RectangleF topViewCurrentFrame) {
			return TransitionLogic.Sliding(touchPosition, topViewCurrentFrame, View, Size);
		}

        /// <summary>
        /// Determines if a slide is complete
        /// </summary>
        /// <returns><c>true</c>, if sliding has ended, <c>false</c> otherwise.</returns>
        /// <param name="touchPosition">Touch position.</param>
        /// <param name="topViewCurrentFrame">Top view current frame.</param>
		public bool SlidingEnded (PointF touchPosition, RectangleF topViewCurrentFrame) {
			return TransitionLogic.SlidingEnded(touchPosition, topViewCurrentFrame, View, Size);
		}

        #endregion

    }
}

