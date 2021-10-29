/*!	@file
	@brief PluggableVR: 手順遷移 ホームシーン 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 ホームシーン 
	internal class Flow_Home : Flow_Common
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

			if (!LastLoadedScene.Update(Global.LastLoadedScene)) return null;

			return new Flow_Delay(new Flow_Home_Main());
		}
	}

	//! 手順遷移 ホームシーン 本体 
	internal class Flow_Home_Main : Flow_Common
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			UpdateCameraParam(4);
			Possess<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			Possess<CrossFade>();
			Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
//			Possess<UnityStandardAssets.ImageEffects.DepthOfField>();
			Possess<UnityStandardAssets.ImageEffects.SunShafts>();
			Possess<CameraEffector.ConfigEffectorWet>();

			Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");

			Remove<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			Remove<CrossFade>();
			Remove<UnityStandardAssets.ImageEffects.GlobalFog>();
//			Remove<UnityStandardAssets.ImageEffects.DepthOfField>();
			Remove<UnityStandardAssets.ImageEffects.SunShafts>();
			Remove<CameraEffector.ConfigEffectorWet>();

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();
			return StepScene();
		}
	}
}