/*!	@file
	@brief PluggableVR: CustomScene シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KKS
{
	//! CustomScene シーン付随動作 
	internal class Scene_CustomScene : SceneScope
	{
		private CharaObserver _charaF = new CharaObserver();
		private CharaObserver _charaM = new CharaObserver();
		private Camera _prevcam;

		protected override void OnStart()
		{
			Global.Logger.LogDebug(ToString() + " bgn");
			base.OnStart();

			// 元カメラ退避 
			var mng = VRManager.Instance;
			_prevcam = (mng.Camera == null) ? null : mng.Camera.Source.Target;
			if (_prevcam != null) mng.Camera.Recall();

			 // メインカメラ捕捉 
			 var player = mng.Player;
			player.SetCamera(GameObject.Find("/CustomScene/CamBase/Camera").GetComponent<Camera>());
			player.Camera.BeActive();

			// 元のカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();
			cam.Possess<ChaCustom.CustomRender>();
			cam.Possess<GameScreenShot>();
			cam.Possess<UnityEngine.EventSystems.PhysicsRaycaster>();
//			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
//			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// 元のカメラから遮断するComponent 
			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(10);

			// キャラ状態捕捉 
			_charaF.Start("/chaF_001");
			_charaM.Start("/chaM_001");
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(ToString() + " end");
			base.OnTerminate();

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

			_charaF.Update();
			_charaM.Update();
		}
	}
}
