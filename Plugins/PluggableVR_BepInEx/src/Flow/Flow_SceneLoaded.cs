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
	internal class Flow_SceneLoaded : Flow
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			// ロードせずに抜けたケースの検知 
			var obj = GameObject.Find("/SceneLoadScene");
			if (obj == null) return new Flow_Edit();

			// ロード通知待ち 
			if (Global.LastLoadedScene != "StudioNotification") return null;

			// 元カメラ位置に移動 
			var mng = VRManager.Instance;
			var player = mng.Player;
			var cam = player.Camera;

			VRManager.Instance.Reloc(Loc.FromWorldTransform(cam.Source.transform));

			return new Flow_Edit();
		}
	}
}