/*!	@file
	@brief PluggableVR: CharaCustom シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_HS2
{
	//! CharaCustom シーン付随動作 
	internal class Scene_CharaCustom : SceneScope
	{
		private Camera _mainCamera;
		private Camera _coordCamera;
		private Camera _prevcam;
		private WorldScope _world = new WorldScope();

		protected override void OnStart()
		{
			Global.Logger.LogDebug(ToString() + " bgn");
			base.OnStart();

			// 元カメラ退避 
			var mng = VRManager.Instance;
			_prevcam = (mng.Camera == null) ? null : mng.Camera.Source.Target;
			if (_prevcam != null) mng.Camera.Recall();

			// Camera構造が通常と違う 
			var cb = GameObject.Find("/CharaCustom/CustomControl/CharaCamera").transform;
			_mainCamera = cb.Find("Main Camera").GetComponent<Camera>();
			_coordCamera = cb.Find("CoordinateCamera").GetComponent<Camera>();
			_coordCamera.cullingMask = 0;

			var player = mng.Player;
			player.SetCamera(_mainCamera);

			// 元のカメラから移行するComponent 
			var cam = player.Camera;
			cam.Possess<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			cam.Possess<GameScreenShot>();
			cam.Possess<UnityEngine.EventSystems.PhysicsRaycaster>();
			cam.Possess<CharaCustom.CustomRender>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(10);

			_world.Start("/CharaCustom/CustomControl");
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(ToString() + " end");
			base.OnTerminate();

			var mng = VRManager.Instance;
			var player = mng.Player;
			var cam = player.Camera;
			cam.Dispose();

			// 元カメラに戻す 
			player.SetCamera(_prevcam);
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			_world.Update();
		}
	}
}
