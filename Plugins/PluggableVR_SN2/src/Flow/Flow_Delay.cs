/*!	@file
	@brief PluggableVR: 手順遷移 フレーム経過待ち 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_SN2
{
	//! 手順遷移 フレーム経過待ち 
	public class Flow_Delay : FlowBase
	{
		private FlowBase _next;
		private int _wait;

		public Flow_Delay(FlowBase next, int wait = 0)
		{
			_next = next;
			_wait = wait;
		}

		protected override FlowBase OnUpdate()
		{
			base.OnUpdate();
			if (--_wait >= 0) return null;
			if (_next == null) Terminate();
			return _next;
		}
	}
}
