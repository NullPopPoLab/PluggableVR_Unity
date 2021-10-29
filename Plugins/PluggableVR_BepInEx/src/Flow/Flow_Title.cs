/*!	@file
	@brief PluggableVR: 手順遷移 タイトル画面 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 タイトル画面 
	internal class Flow_Title : Flow_Common
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

			if (!LastLoadedScene.Update(Global.LastLoadedScene))return null;

			return new Flow_Delay(new Flow_Title_Main());
		}
	}

	//! 手順遷移 タイトル画面 本体 
	internal class Flow_Title_Main : Flow_Common
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			UpdateCameraParam(4);
			Possess<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			Possess<CameraEffector.ConfigEffectorWet>();
			Possess<GameScreenShot>();
			Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
//			Possess<UnityStandardAssets.ImageEffects.DepthOfField>();
			Possess<UnityStandardAssets.ImageEffects.SunShafts>();
			Possess<CrossFade>();

			Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");

			Remove<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			Remove<CameraEffector.ConfigEffectorWet>();
			Remove<GameScreenShot>();
			Remove<UnityStandardAssets.ImageEffects.GlobalFog>();
//			Remove<UnityStandardAssets.ImageEffects.DepthOfField>();
			Remove<UnityStandardAssets.ImageEffects.SunShafts>();
			Remove<CrossFade>();

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();
			return StepScene();
		}
	}
}