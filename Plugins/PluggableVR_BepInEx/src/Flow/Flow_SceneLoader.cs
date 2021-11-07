/*!	@file
	@brief PluggableVR: 手順遷移 シーンロードGUI 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.CS2
{
	//! 手順遷移 シーンロードGUI 
	internal class Flow_SceneLoader : Flow
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
			return new Flow_SceneLoaded();
		}
	}
}