/*!	@file
	@brief PluggableVR: Title シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_HS2
{
	//! Title シーン付随動作 
	internal class Scene_Title : SceneScope
	{
		protected override void OnStart()
		{
			Global.Logger.LogDebug(ToString() + " bgn");
			base.OnStart();

			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(Camera.main);
			player.Camera.BePassive();

			// 元のカメラから移行するComponent 
			var cam = player.Camera;
			cam.Possess<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			cam.Possess<GameScreenShot>();
			cam.Possess<UnityStandardAssets.ImageEffects.GlobalFog>();
			cam.Possess<UnityStandardAssets.ImageEffects.SunShafts>();

			// ぼやけて見づらいのでoffっとく 
			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();
			// VRでカメラいぢられたくないのでoffっとく 
			cam.Suppress<CameraEffector.ConfigEffectorWet>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(4);
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
		}
	}
}
