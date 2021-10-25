﻿/*!	@file
	@brief PluggableVR: 手順遷移 開始時 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.SN2
{
	//! 手順遷移 開始時 
	public class Flow_Startup : Flow
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
		}

		protected override Flow OnUpdate()
		{
			// メインカメラ生成待ち 
			Global.MainCamera = Camera.main;
			if (Global.MainCamera == null) return null;

			// 操作開始 
			var sc = Global.MainCamera;
			var loc = Loc.FromWorldTransform(sc.transform);
			var avatar = new DemoAvatar(loc);
			var player = new DemoPlayer(avatar);
			player.Rig.localScale = new Vector3(8,8,8);

			var mng = VRManager.Instance;
			mng.SetPlayer(player);

			// 元のカメラパラメータを反映 
			var dc = player.Camera.GetComponent<Camera>();
			dc.clearFlags = sc.clearFlags;
			dc.cullingMask = sc.cullingMask;
			dc.farClipPlane = sc.farClipPlane;
			dc.nearClipPlane = 0.1f;
			// 変更不可らしい 
//			dc.fieldOfView = sc.fieldOfView;

			// 本来のメインカメラは無効化 
			sc.enabled = false;
			var lsn = sc.GetComponent<AudioListener>();
			if (lsn != null) lsn.enabled = false;

			return new Flow_Edit();
		}
	}
}