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

			// ステージシーンのロード待ち 
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

			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(Camera.main);

			// 元のカメラから移行するComponent 
			var cam = player.Camera;
			cam.Possess<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
			cam.Possess<UnityStandardAssets.ImageEffects.SunShafts>();

			// ぼやけて見づらいのでoffっとく 
			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();
			// VRでカメラいぢられたくないのでoffっとく 
			cam.Suppress<CameraEffector.ConfigEffectorWet>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(4);
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");

			var mng = VRManager.Instance;
			var player = mng.Player;
			var cam = player.Camera;
			cam.Dispose();

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();
			return StepScene();
		}
	}
}