/*!	@file
	@brief PluggableVR: Canvas スコープ付随動作 共通部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_EC
{
	//! Canvas スコープ付随動作 共通部 
	internal class Scope_Canvas: ComponentScope<Canvas>
	{
		protected override void OnStart()
		{
			base.OnStart();

			if(Target.renderMode==RenderMode.ScreenSpaceCamera)
				Target.renderMode=RenderMode.ScreenSpaceOverlay;
		}

		protected override void OnTerminate()
		{
			base.OnTerminate();
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();
		}
	}
}
