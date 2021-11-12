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

namespace PluggableVR_CS2
{
	internal class SceneLauncher: SceneReportBase
	{
		private static SceneDic<string> _launcher = new SceneDic<string>();

		internal SceneLauncher(){
			_launcher["Init"] = new Scene_Init();
			_launcher["Studio"] = new Scene_Studio();
		}

		internal void Update(){
			_launcher.Update();
		}

		public override void OnSceneLoaded(string path, LoadSceneMode mode)
		{
			base.OnSceneLoaded(path, mode);

			Global.Logger.LogDebug("Scene Loaded: " + path);
			var scn = SceneInfo.Find(path);
//			HierarchyDumper.Dumper.Dump2File("Hier_" + Global.ProcessName, "Load-" + scn.name);

			var proc = _launcher[(scn.name == null) ? "" : scn.name];
			if (proc != null) proc.Start(path);
		}

		public override void OnSceneUnloaded(string path)
		{
			base.OnSceneUnloaded(path);

			Global.Logger.LogDebug("Scene Unloaded: " + path);
			var scn = SceneInfo.Find(path);
//			HierarchyDumper.Dumper.Dump2File("Hier_" + Global.ProcessName, "Unload-" + scn.name);

			var proc = _launcher[(scn.name == null) ? "" : scn.name];
			if (proc != null) proc.Terminate();
		}

		public override void OnSceneChanged(string prev, string next)
		{
			base.OnSceneChanged(prev, next);

			Global.Logger.LogDebug("Scene Changed: " + prev + " => " + next);
			var scn1 = SceneInfo.Find(prev);
			var scn2 = SceneInfo.Find(next);
//			HierarchyDumper.Dumper.Dump2File("Hier_" + Global.ProcessName, "Changed-" + scn1.name + "-" + scn2.name);

			var proc1 = _launcher[(scn1.name == null) ? "" : scn1.name];
			if (proc1 != null) proc1.Active = false;
			var proc2 = _launcher[(scn2.name == null) ? "" : scn2.name];
			if (proc2 != null) proc2.Active = true;
		}
	}
}
