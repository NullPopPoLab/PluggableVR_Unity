/*!	@file
	@brief PluggableVR: HPlayScene シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_EC
{
	//! HPlayScene シーン付随動作 
	internal class Scene_HPlayScene : SceneScope
	{
		private WorldScope _world = new WorldScope();
		private ComponentList<Canvas> _canvas = new ComponentList<Canvas>();

		private Scope_Canvas _addCanvas(string path)
		{
			var obj = new Scope_Canvas();
			obj.Start(path);
			_canvas.Add(obj);
			return obj;
		}

		protected override void OnStart()
		{
			Global.Logger.LogDebug(ToString() + " bgn");
			base.OnStart();

			// メインカメラ捕捉 
			var mng = VRManager.Instance;
			var player = mng.Player;
			player.SetCamera(Camera.main);
			player.Camera.BePassive();

			// 元のカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();
			cam.Possess<UnityStandardAssets.ImageEffects.BloomAndFlares>();
			cam.Possess<UnityStandardAssets.ImageEffects.SunShafts>();
			cam.Possess<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();

			// 元のカメラから除去するComponent 
			cam.Suppress<UnityStandardAssets.ImageEffects.DepthOfField>();
			cam.Suppress<UnityStandardAssets.ImageEffects.Blur>();
			cam.Suppress<CameraEffectorConfig>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(10);

			_world.Start("/Etc/Character");

			// ScreenScapceCameraになってるCanvas
			// 表示されないので当面はOverlayに変えとく 
			_addCanvas("/CommonSpace/ADV Canvas/Canvas Text Effect");
			_addCanvas("/Etc/ScreenCapture/Canvas Cap");
			_addCanvas("/UI/ADV/Canvas");
			_addCanvas("/UI/H/Canvas");
			_addCanvas("/UI/System/Canvas").SortingOrder = 6;
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(ToString() + " end");
			base.OnTerminate();

			_world.Terminate();

			// カメラ解除 
			var mng = VRManager.Instance;
			mng.Camera.Dispose();
			mng.Player.SetCamera(null);
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			_world.Update();
			_canvas.Update();
		}
	}
}
