/*!	@file
	@brief PluggableVR: PoseCreate シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_EC
{
	//! PoseCreate シーン付随動作 
	internal class Scene_PoseCreate : SceneScope
	{
		private CharaObserver _charaF = new CharaObserver();
		private CharaObserver _charaF2 = new CharaObserver();
		private CharaObserver _charaM = new CharaObserver();
		private ComponentList<Canvas> _canvas = new ComponentList<Canvas>();

		private void _addCanvas(string path)
		{
			var obj = new Scope_Canvas();
			obj.Start(path);
			_canvas.Add(obj);
		}

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

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(10);

			// キャラ状態捕捉 
			_charaF.Start("/chaF_001");
			_charaF2.Start("/chaF_002");
			_charaM.Start("/chaM_001");

			// ScreenScapceCameraになってるCanvas
			// 表示されないので当面はOverlayに変えとく 
			_addCanvas("/PoseCreateScene/Canvas Cap");
			// 背景を変えるとハマる 
//			_addCanvas("/PoseCreateScene/Canvas Background");
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
			_charaF2.Update();
			_charaM.Update();
		}
	}
}
