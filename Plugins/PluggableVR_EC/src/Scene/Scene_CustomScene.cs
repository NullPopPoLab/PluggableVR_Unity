/*!	@file
	@brief PluggableVR: CustomScene シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_EC
{
	//! CustomScene シーン付随動作 
	internal class Scene_CustomScene : SceneScope
	{
		private CharaObserver _charaF = new CharaObserver();
		private CharaObserver _charaM = new CharaObserver();

		protected override void OnStart()
		{
			Global.Logger.LogDebug(ToString() + " bgn");
			base.OnStart();

			// メインカメラ捕捉 
			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(Camera.main);
			player.Camera.BeActive();

			// 元のカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();
			cam.Possess<ChaCustom.CustomRender>();
			cam.Possess<GameScreenShot>();
			cam.Possess<UnityEngine.EventSystems.PhysicsRaycaster>();
			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();

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

			// カメラ解除 
			var mng = VRManager.Instance;
			mng.Camera.Dispose();
			mng.Player.SetCamera(null);
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			_charaF.Update();
			_charaM.Update();
		}
	}
}
