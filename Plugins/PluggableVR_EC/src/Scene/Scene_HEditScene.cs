/*!	@file
	@brief PluggableVR: HEditScene シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_EC
{
	//! HEditScene シーン付随動作 
	internal class Scene_HEditScene : SceneScope
	{
		private WorldScope _world1 = new WorldScope();
		private WorldScope _world2 = new WorldScope();
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
			player.Camera.BePassive();

			// 元のカメラから移設するComponent 
			var cam = player.Camera;
			cam.Possess<FlareLayer>();

			// アバター表示Layerをカメラの表示対象内で選択 
			mng.Avatar.SetLayer(10);

			_world1.Start("/Character");
			_world2.Start("/MainGroup");

			// ScreenScapceCameraになってるCanvas
			// 表示されないので当面はOverlayに変えとく 
			_addCanvas("/CommonSpace/ADV Canvas/Canvas Text Effect");
			_addCanvas("/ScreenCapture/Canvas Cap");
			_addCanvas("/UI/Initialize/Canvas");
			_addCanvas("/UI/HPart/PartSetting/Canvas");
			_addCanvas("/UI/HPart/PartInfoSetting/Canvas");
			_addCanvas("/UI/HPart/MotionSetting/Canvas");
			_addCanvas("/UI/HPart/IKParentSetting/Canvas");
			_addCanvas("/UI/HPart/AnimationListSetting/Canvas");
			_addCanvas("/UI/HPart/ItemDropDownCanvas/Canvas");
			_addCanvas("/UI/HPart/FaceDropDownCanvas/Canvas");
			_addCanvas("/UI/HPart/SEDropDownCanvas/Canvas");
			_addCanvas("/UI/HPart/DankonDropDownCanvas/Canvas");
			_addCanvas("/UI/HPart/ImageEffectColorDropDownCanvas/Canvas");
			_addCanvas("/UI/HPart/MapDropDownCanvas/Canvas");
			_addCanvas("/UI/HPart/CanvasIKParentSelect");
			_addCanvas("/UI/BaseSetting/Canvas");
			_addCanvas("/UI/BaseSetting/CanvasTagSelect");
			_addCanvas("/UI/CharaSelect/Canvas");
			_addCanvas("/UI/BGMDropDownCanvas/Canvas");
			_addCanvas("/UI/Canvas Guide Input");
			_addCanvas("/UI/CvsColor");
			_addCanvas("/SaveUI/Canvas");
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(ToString() + " end");
			base.OnTerminate();

			_world1.Terminate();
			_world2.Terminate();

			// カメラ解除 
			var mng = VRManager.Instance;
			mng.Camera.Dispose();
			mng.Player.SetCamera(null);
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();

			_world1.Update();
			_world2.Update();
			_canvas.Update();
		}
	}
}
