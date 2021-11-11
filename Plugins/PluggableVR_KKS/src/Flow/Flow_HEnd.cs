/*!	@file
	@brief PluggableVR: 手順遷移 えっち 終了 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System.Collections.Generic;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KKS
{
	//! 手順遷移 えっち 終了 
	internal class Flow_HEnd : Flow_Common
	{
		private Flow _prev;

		internal Flow_HEnd(Flow prev)
		{
			_prev=prev;
		}

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// 終了検知 
			if (!Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/H.unity").isLoaded) return new Flow_Delay(_prev);

			return null;
		}
	}
}
