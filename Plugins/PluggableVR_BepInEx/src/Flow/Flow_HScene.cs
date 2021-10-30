/*!	@file
	@brief PluggableVR: 手順遷移 えっちシーン 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 えっちシーン 
	internal class Flow_HScene : Flow_Common
	{
		private Transform _camera;
		private ChangingCensor<Loc> _cameraLoc;

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			_camera = GameObject.Find("/HCamera").transform;
			_cameraLoc = new ChangingCensor<Loc>(Loc.FromWorldTransform(_camera));
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// カメラ位置設定待ち 
			if (!_cameraLoc.Update(Loc.FromWorldTransform(_camera))) return null;
			return new Flow_HScene_Main();

//			if (!LastLoadedScene.Update(Global.LastLoadedScene)) return null;
//			return new Flow_Delay(new Flow_HScene_Main());
		}
	}

	//! 手順遷移 えっちシーン 本体 
	internal class Flow_HScene_Main : Flow_Common
	{
		private Camera _mainCamera;

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// Camera構造が通常と違う 
			var cb = GameObject.Find("/HCamera").transform;
			_mainCamera = cb.Find("Main Camera").GetComponent<Camera>();

			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(_mainCamera);

			// 元のカメラから移行するComponent 
			var cam = player.Camera;
			cam.Possess<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			cam.Possess<Obi.ObiFluidRenderer>();
			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
			cam.Possess<UnityStandardAssets.ImageEffects.SunShafts>();
			cam.Possess<GameScreenShotAssist>();
			// 移行してはいけないらしい 
//			cam.Possess<PlaceholderSoftware.WetStuff.WetStuff>();

			// ぼやけて見づらいのでoffっとく 
			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();
			// VRでカメラいぢられたくないのでoffっとく 
			cam.Suppress<ScreenEffect>();
			cam.Suppress<CameraEffector.ConfigEffectorWet>();
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