/*!	@file
	@brief PluggableVR: Studio シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_CS2
{
	//! Studio シーン付随動作 
	internal class Scene_Studio : SceneScope
	{
		private WorldScope _world = new WorldScope();

		protected override void OnStart()
		{
			Global.Logger.LogDebug(ToString() + " bgn");
			base.OnStart();

			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(Camera.main);
			player.Camera.BePassive();

			// 元のカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();
//			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
//			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// 元のカメラから遮断するComponent 
			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(4);

			// ワールド状態捕捉 
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
