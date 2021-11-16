/*!	@file
	@brief PluggableVR: Wedding シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! Wedding シーン付随動作 
	internal class Scene_Wedding : SceneScope
	{
		private WorldScope _world = new WorldScope();

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
//			cam.Possess<UnityEngine.EventSystems.PhysicsRaycaster>();
//			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
//			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// 元のカメラから遮断するComponent 
			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(10);

			_world.Start("/Wedding");
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(ToString() + " end");
			base.OnTerminate();
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			_world.Update();
		}
	}
}
