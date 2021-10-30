/*!	@file
	@brief PluggableVR: 手順遷移 会話シーン 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 会話シーン 
	internal class Flow_ADV : Flow_Common
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

			var mc = GameObject.Find("/ADVMainScene/ADVScene(Clone)/BasePosition/Cameras/Main Camera");
			if (mc == null) return null;
			return new Flow_ADV_Main();

//			return new Flow_Delay(new Flow_ADV_Main());
		}
	}

	//! 手順遷移 会話シーン 本体 
	internal class Flow_ADV_Main : Flow_Common
	{
		private Camera _mainCamera;

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// Camera構造が通常と違う 
			_mainCamera = GameObject.Find("/ADVMainScene/ADVScene(Clone)/BasePosition/Cameras/Main Camera").GetComponent<Camera>();

			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(_mainCamera);

			// 元のカメラから移行するComponent 
			var cam = player.Camera;
			cam.Possess<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
			cam.Possess<UnityStandardAssets.ImageEffects.SunShafts>();
			// 移行してはいけないらしい 
//			cam.Possess<PlaceholderSoftware.WetStuff.WetStuff>();

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