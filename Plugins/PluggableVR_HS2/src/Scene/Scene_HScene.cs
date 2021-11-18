/*!	@file
	@brief PluggableVR: HScene シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_HS2
{
	//! HScene シーン付随動作 
	internal class Scene_HScene : SceneScope
	{
		private Camera _mainCamera;
		private WorldScope _world = new WorldScope();

		protected override void OnStart()
		{
			Global.Logger.LogDebug(ToString() + " bgn");
			base.OnStart();

			// Camera構造が通常と違う 
			var cb = GameObject.Find("/HCamera").transform;
			_mainCamera = cb.Find("Main Camera").GetComponent<Camera>();

			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(_mainCamera);
			player.Camera.BePassive();

			// 元のカメラから移行するComponent 
			var cam = player.Camera;
			cam.Possess<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			cam.Possess<Obi.ObiFluidRenderer>();
			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
			cam.Possess<UnityStandardAssets.ImageEffects.SunShafts>();
			cam.Possess<PlaceholderSoftware.WetStuff.WetStuff>();
			cam.Possess<GameScreenShotAssist>();

			// ぼやけて見づらいのでoffっとく 
			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();
			// VRでカメラいぢられたくないのでoffっとく 
			cam.Suppress<ScreenEffect>();
			cam.Suppress<CameraEffector.ConfigEffectorWet>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(4);

			_world.Start("/CommonSpace");
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(ToString() + " end");
			base.OnTerminate();

			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(null);
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			_world.Update();
		}
	}
}
