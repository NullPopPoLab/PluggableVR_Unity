/*!	@file
	@brief PluggableVR: MainCamera スコープ付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! MainCamera スコープ付随動作 
	internal class Scope_MainCamera: ComponentScope<Camera>
	{
		protected override void OnStart()
		{
			base.OnStart();
		}

		protected override void OnTerminate()
		{
			base.OnTerminate();
		}
	}
}
