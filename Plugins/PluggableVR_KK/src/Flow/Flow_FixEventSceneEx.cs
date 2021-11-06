/*!	@file
	@brief PluggableVR: 手順遷移 イベント 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! 手順遷移 イベント 
	internal class Flow_FixEventSceneEx : Flow_Common
	{
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

			// 再生検知 
			if (Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/ADV.unity").isLoaded)
			{
				return new Flow_ADV(this, false, "Assets/Illusion/Game/Scene/FixEventSceneEx.unity");
			}

			return StepScene();
		}
	}
}
