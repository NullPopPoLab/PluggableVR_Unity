/*!	@file
	@brief PluggableVR: 手順遷移 編集中 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_CS
{
	//! 手順遷移 編集中 
	internal class Flow_Edit : Flow_Common
	{

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString()+" bgn");
			base.OnStart();
		}
		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();

			// 終了検知 
			if (GameObject.Find("/StudioCheck") != null) return new Flow_ScenePurging();
			// ロード検知 
			if (Global.Scene.GetSceneInfo("Assets/Studio/Scene/StudioNotification.unity").isLoaded) return new Flow_SceneLoaded();

			return null;
		}
	}
}