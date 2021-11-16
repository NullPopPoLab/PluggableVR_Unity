/*!	@file
	@brief PluggableVR: ADV シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! ADV シーン付随動作 
	internal class Scene_ADV : SceneScope
	{
		private WorldScope _world = new WorldScope();
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
			player.SetCamera(Camera.main);
			player.Camera.BePassive();

			// メインカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();
//			cam.Possess<AmplifyColorEffect>();
//			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
//			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
//			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(4);

			_world.Start("/ADVScene/BasePosition/Characters");
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

			_world.Update();
		}
	}
}
