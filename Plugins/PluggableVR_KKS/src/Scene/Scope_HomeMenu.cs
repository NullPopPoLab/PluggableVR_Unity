/*!	@file
	@brief PluggableVR: HomeMenu スコープ付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KKS
{
	//! HomeMenu スコープ付随動作 
	internal class Scope_HomeMenu : ComponentScope<ActionGame.HomeMenu>
	{
		public bool Interrupt;

		private bool _visible;
		public bool IsVisible
		{
			get { return _visible; }
			private set
			{
				if (_visible == value) return;
				_visible = value;

				if (value)
				{
					Global.Logger.LogDebug(ToString() + " bgn");

					var mng = VRManager.Instance;
					mng.Camera.BeActive();
				}
				else
				{
					Global.Logger.LogDebug(ToString() + " end");
				}
			}
		}

		protected override void OnTerminate()
		{
			base.OnTerminate();

			IsVisible = false;
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			IsVisible = Target.visible && !Interrupt;
		}
	}
}
