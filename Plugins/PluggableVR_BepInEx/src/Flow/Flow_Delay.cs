/*!	@file
	@brief PluggableVR: 手順遷移 フレーム経過待ち 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.CS
{
	//! 手順遷移 フレーム経過待ち 
	public class Flow_Delay : Flow
	{
		private Flow _next;
		private int _wait;

		public Flow_Delay(Flow next, int wait = 0)
		{
			_next = next;
			_wait = wait;
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();
			if (--_wait >= 0) return null;
			if (_next == null) Terminate();
			return _next;
		}
	}
}
