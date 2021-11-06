﻿/*!	@file
	@brief PluggableVR: 手順遷移 ウェディング 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KK
{
	//! 手順遷移 ウェディング 
	internal class Flow_Wedding : Flow_Common
	{
		private bool _show;
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			_show = false;

			// メインカメラの扱い 
			VRCamera.SourceMode = VRCamera.ESourceMode.Disabled;

			// メインカメラ捕捉 
			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(Camera.main);

			// メインカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();
			cam.Possess<AmplifyColorEffect>();
			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// メインカメラから遮断するComponent 
			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(0);
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");

			// 移設Component除去 
			var mng = VRManager.Instance;
			if (_show)
			{
				// 終宴後また使うので有効に戻しとく 
				mng.Camera.Recall();
				mng.Camera.Source.enabled = true;
			}
			else
			{
				// もう参照残ってないので捨て 
				mng.Camera.Dispose();
			}

			// メインカメラ切断 
			var player = mng.Player;
			player.SetCamera(null);

			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// 開宴検知 
			if (Global.Scene.GetSceneInfo("Assets/Illusion/Game/Scene/ADV.unity").isLoaded)
			{
				_show = true;
				return new Flow_ADV(this);
			}

			return StepScene();
		}
	}
}
