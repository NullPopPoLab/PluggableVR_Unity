/*!	@file
	@brief PluggableVR: シーン遷移監視 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace PluggableVR_HS2
{
	//! シーンがロードされている間のみ有効な動作 
	internal class Scenewalk: SceneReportBase
	{
		private static SceneDic<string> _scene = new SceneDic<string>();

		internal Scenewalk(){
			_scene["Assets/Illusion/Scene/ADV.unity"] = new Scene_ADV();
			_scene["Assets/Illusion/Scene/CharaCustom.unity"] = new Scene_CharaCustom();
			_scene["Assets/Illusion/Scene/Home.unity"] = new Scene_Home();
			_scene["Assets/Illusion/Scene/HScene.unity"] = new Scene_HScene();
			_scene["Assets/Illusion/Scene/Init.unity"] = new Scene_Init();
			_scene["Assets/Illusion/Scene/LobbyScene.unity"] = new Scene_LobbyScene();
			_scene["Assets/Illusion/Scene/Title.unity"] = new Scene_Title();
		}

		internal void Update(){
			_scene.Update();
		}

		public override void OnSceneLoaded(string path, LoadSceneMode mode)
		{
			base.OnSceneLoaded(path, mode);

			Global.Logger.LogDebug("Scene Loaded: " + path);
			var scn = SceneInfo.Find(path);
//			HierarchyDumper.Dumper.Dump2File("Hier_" + Global.ProcessName, "Load-" + scn.name);

			var proc = _scene[(scn.path == null) ? "" : scn.path];
			if (proc != null) proc.Start(path);
		}

		public override void OnSceneUnloaded(string path)
		{
			base.OnSceneUnloaded(path);

			Global.Logger.LogDebug("Scene Unloaded: " + path);
			var scn = SceneInfo.Find(path);
//			HierarchyDumper.Dumper.Dump2File("Hier_" + Global.ProcessName, "Unload-" + scn.name);

			var proc = _scene[(scn.path == null) ? "" : scn.path];
			if (proc != null) proc.Terminate();
		}

		public override void OnSceneChanged(string prev, string next)
		{
			base.OnSceneChanged(prev, next);

			Global.Logger.LogDebug("Scene Changed: " + prev + " => " + next);
			var scn1 = SceneInfo.Find(prev);
			var scn2 = SceneInfo.Find(next);
//			HierarchyDumper.Dumper.Dump2File("Hier_" + Global.ProcessName, "Changed-" + scn1.name + "-" + scn2.name);

			var proc1 = _scene[(scn1.path == null) ? "" : scn1.path];
			if (proc1 != null) proc1.Active = false;
			var proc2 = _scene[(scn2.path == null) ? "" : scn2.path];
			if (proc2 != null) proc2.Active = true;
		}
	}
}
