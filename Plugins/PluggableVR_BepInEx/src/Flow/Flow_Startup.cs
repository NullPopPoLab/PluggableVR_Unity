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
			var mng = VRManager.Instance;
			mng.Controller.Initialize(Loc.FromWorldTransform(Global.MainCamera.transform));

			// 本来のメインカメラは無効化 
			Global.MainCamera.enabled = false;
			var lsn = Global.MainCamera.GetComponent<AudioListener>();
			if (lsn != null) lsn.enabled = false;

			return new Flow_Edit();
		}
	}
}