// /// Copyright (C) 2013 Pat Laplante & Franc Caico
// ///
// ///	Permission is hereby granted, free of charge, to  any person obtaining a copy 
// /// of this software and associated documentation files (the "Software"), to deal 
// /// in the Software without  restriction, including without limitation the rights 
// /// to use, copy,  modify,  merge, publish,  distribute,  sublicense, and/or sell 
// /// copies of the  Software,  and  to  permit  persons  to   whom the Software is 
// /// furnished to do so, subject to the following conditions:
// ///
// ///		The above  copyright notice  and this permission notice shall be included 
// ///     in all copies or substantial portions of the Software.
// ///
// ///		THE  SOFTWARE  IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
// ///     OR   IMPLIED,   INCLUDING  BUT   NOT  LIMITED   TO   THE   WARRANTIES  OF 
// ///     MERCHANTABILITY,  FITNESS  FOR  A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
// ///     IN NO EVENT SHALL  THE AUTHORS  OR COPYRIGHT  HOLDERS  BE  LIABLE FOR ANY 
// ///     CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT 
// ///     OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION  WITH THE SOFTWARE OR 
// ///     THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// /// -----------------------------------------------------------------------------
//
using System;
using UIKit;
using System.Drawing;
using System.Linq;
using CoreGraphics;

namespace SlidingPanels.Lib.Tools
{
	/// <summary>
	/// Static class giving current screen figures regardless of the orientation.
	/// </summary>
	public static class WindowState
	{
		public static UIWindow Window {
			get {
				return UIApplication.SharedApplication.KeyWindow ?? UIApplication.SharedApplication.Windows.FirstOrDefault();
			}
		}

		/// <summary>
		/// Indicates if the device is in landscape mode.
		/// </summary>
		/// <value><c>true</c> if the device is in landscape mode; otherwise, <c>false</c>.</value>
		public static bool IsLandscapeOrientation {
			get {
				return UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight;
			}
		}

		/// <summary>
		/// Gives the real current window width which takes care of the current orientation.
		/// </summary>
		/// <value>Real current window width which takes care of the current orientation.</value>
		public static nfloat CurrentScreenWidth {
			get {
                return Window.Bounds.Width;
			}
		}

		/// <summary>
		/// Gives the real current window height which takes care of the current orientation.
		/// </summary>
		/// <value>Real current window height which takes care of the current orientation.</value>
		public static nfloat CurrentScreenHeight {
			get {
                return Window.Bounds.Height;
			}
		}

		public static CGRect CurrentScreenFrame {
			get {
				return new CGRect (0, 0, CurrentScreenWidth, CurrentScreenHeight);
			}
		}
	}
}

