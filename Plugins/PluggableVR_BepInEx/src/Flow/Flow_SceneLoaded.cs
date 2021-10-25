/*!	@file
	@brief PluggableVR: 手順遷移 シーンロード検知後 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.SN2
{
	//! 手順遷移 シーンロード検知後 
	public class Flow_SceneLoaded : Flow
	{
		private Loc _cameraLoc;

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");

			_cameraLoc = Loc.FromWorldTransform(Global.MainCamera.transform);
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
		}

		protected override Flow OnUpdate()
		{
			// ロードせずに抜けたケースの検知 
			var gobj = GameObject.Find("/SceneLoadScene");
			if (gobj == null) return new Flow_Edit();

			// ロード通知待ち 
			if (Global.LastLoadedScene != "StudioNotification") return null;

			// メインカメラ位置に移動 
			var loc = Loc.FromWorldTransform(Global.MainCamera.transform);
			VRManager.Instance.Reloc(loc);
			return new Flow_Edit();
		}
	}
}