/*!	@file
	@brief PluggableVR: 手順遷移 開始時 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using PluggableVR;
using NullPopPoSpecial;

namespace PluggableVR.CS
{
	//! 手順遷移 開始時 
	public class Flow_Startup : Flow
	{
		protected override Flow OnUpdate()
		{
			// メインカメラ生成待ち 
			Global.MainCamera = Camera.main;
			if (Global.MainCamera == null) return null;

			// 操作開始 
			var sc = Global.MainCamera;
			var mng = VRManager.Instance;
			mng.Controller.Initialize(Loc.FromWorldTransform(sc.transform));

			// レイヤ0が表示対象外になるので4に変更 
			var a = mng.Controller.Avatar;
			var layer = 4;
			a.Head.layer = layer;
			a.View.transform.Find("Neck").gameObject.layer = layer;
			a.View.transform.Find("Shoulder").gameObject.layer = layer;
			a.UpFromHead.layer = layer;
			a.ForeFromHead.layer = layer;

			// 元のカメラパラメータを反映 
			var dc = mng.Controller.Player.Camera.GetComponent<Camera>();
			dc.clearFlags = sc.clearFlags;
			dc.cullingMask = sc.cullingMask;

			// 本来のメインカメラは無効化 
			sc.enabled = false;
			var lsn = sc.GetComponent<AudioListener>();
			if (lsn != null) lsn.enabled = false;

			return new Flow_Edit();
		}
	}
}