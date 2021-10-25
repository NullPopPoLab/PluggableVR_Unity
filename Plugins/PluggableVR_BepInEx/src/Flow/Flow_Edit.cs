/*!	@file
	@brief PluggableVR: 手順遷移 編集中 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.SN2
{
	//! 手順遷移 編集中 
	public class Flow_Edit : Flow
	{
		private Dictionary<string, Func<Flow>> _transit = new Dictionary<string, Func<Flow>>();

		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString()+" bgn");

			// Unityシーンロードに連動する遷移 
			_transit["StudioSceneLoad"] = () =>
			{
				if (GameObject.Find("/SceneLoadScene") == null) return null;
				return new Flow_SceneLoaded();
			};
			_transit["StudioCheck"] = () => new Flow_ScenePurging();
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
		}

		protected override Flow OnUpdate()
		{
			if (!_transit.ContainsKey(Global.LastLoadedScene)) return null;
			return _transit[Global.LastLoadedScene]();
		}
	}
}