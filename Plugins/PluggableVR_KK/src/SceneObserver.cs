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

namespace PluggableVR_KK
{
	internal class SceneObserver: SceneObserverBase
	{
		protected override void OnSceneLoaded(string path,LoadSceneMode mode){
			base.OnSceneLoaded(path, mode);

			Global.Logger.LogInfo("Scene Loaded: " + path);
//			HierarchyDumper.Dumper.Dump2File("Hier_" + Global.ProcessName, "Load-" + GetSceneInfo(path).name);
		}

		protected override void OnSceneUnloaded(string path)
		{
			base.OnSceneUnloaded(path);

			Global.Logger.LogInfo("Scene Unloaded: " + path);
//			HierarchyDumper.Dumper.Dump2File("Hier_" + Global.ProcessName, "Unload-" + GetSceneInfo(path).name);
		}

		protected override void OnSceneChanged(string prev, string next){
			base.OnSceneChanged(prev, next);

			Global.Logger.LogInfo("Scene Changed: " + prev + " => " + next);
//			HierarchyDumper.Dumper.Dump2File("Hier_" + Global.ProcessName, "Changed-" + GetSceneInfo(prev).name+"-"+ GetSceneInfo(next).name);
		}
	}
}
