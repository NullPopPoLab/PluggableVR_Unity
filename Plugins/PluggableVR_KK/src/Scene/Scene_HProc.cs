/*!	@file
	@brief PluggableVR: HProc シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! HProc シーン付随動作 
	internal class Scene_HProc : SceneScope
	{
		private WorldScope _world = new WorldScope();
		private Camera _prevcam;

		protected override void OnStart()
		{
			Global.Logger.LogDebug(ToString() + " bgn");
			base.OnStart();

			// UIのVR対応までひとまず Canvas をoverlayにしとく 
			var ui = GameObject.Find("/Canvas").GetComponent<Canvas>();
			ui.renderMode = RenderMode.ScreenSpaceOverlay;

			// 元カメラ退避 
			var mng = VRManager.Instance;
			_prevcam = (mng.Camera == null) ? null : mng.Camera.Source.Target;
			if (_prevcam != null) mng.Camera.Recall();

			// メインカメラ捕捉 
			var player = mng.Player;
			player.SetCamera(Camera.main);
			player.Camera.BePassive();

			// メインカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();
//			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
//			cam.Possess<AmplifyOcclusionEffect>();
//			cam.Possess<AmplifyColorEffect>();
//			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
//			cam.Possess<UnityStandardAssets.ImageEffects.SunShafts>();
//			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();
//			cam.Possess<Obi.ObiFluidRenderer>();

			// メインカメラから除去するComponent 
//			cam.Possess<CameraEffectorConfig>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(10);

			_world.Start("/HScene");
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(ToString() + " end");
			base.OnTerminate();

			_world.Terminate();

			// 移設Component除去 
			var mng = VRManager.Instance;
			mng.Camera.Dispose();

			// 元カメラに戻す 
			var player = mng.Player;
			player.SetCamera(_prevcam);
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			_world.Update();
		}
	}
}
