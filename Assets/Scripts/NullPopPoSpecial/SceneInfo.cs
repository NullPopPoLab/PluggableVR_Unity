/*!	@file
	@brief NullPopPoSpecial: シーン情報 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial
*/
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace NullPopPoSpecial
{
	public class SceneReportBase
	{
		public virtual void OnSceneLoaded(string path, LoadSceneMode mode) { }
		public virtual void OnSceneUnloaded(string path) { }
		public virtual void OnSceneChanged(string prev, string now) { }
	}

	//! シーン情報 
	public static class SceneInfo
	{
		public static bool Enabled { get; private set; }
		private static string _activeScene = "";
		public static string ActiveScene { get { return _activeScene; } private set { _activeScene = value; } }

		private static SceneReportBase _report;
		private static Dictionary<string, Scene> _scene = new Dictionary<string, Scene>();
		public static Dictionary<string, Scene>.KeyCollection AvailableScenes { get { return _scene.Keys; } }

		public static void Enable(SceneReportBase report)
		{
			if (Enabled) return;
			Enabled = true;
			_report = report;

			SceneManager.sceneLoaded += _onSceneLoaded;
			SceneManager.sceneUnloaded += _onSceneUnloaded;
			SceneManager.activeSceneChanged += _onSceneChanged;
		}

		public static void Disable()
		{
			if (!Enabled) return;
			Enabled = false;
			_report = null;

			SceneManager.sceneLoaded -= _onSceneLoaded;
			SceneManager.sceneUnloaded -= _onSceneUnloaded;
			SceneManager.activeSceneChanged -= _onSceneChanged;
		}

		private static void _onSceneLoaded(Scene scn, LoadSceneMode mode)
		{
			var p = (scn.path == null) ? "" : scn.path;
			_scene[p] = scn;

			if (_report != null) _report.OnSceneLoaded(p, mode);
		}

		private static void _onSceneUnloaded(Scene scn)
		{
			var p = (scn.path == null) ? "" : scn.path;
			if (p == ActiveScene)
			{
				if (_report != null) _report.OnSceneChanged(ActiveScene, "");
				ActiveScene = "";
			}

			if (_report != null) _report.OnSceneUnloaded(p);
			if (_scene.ContainsKey(p)) _scene.Remove(p);
		}

		private static void _onSceneChanged(Scene prev, Scene next)
		{
			var p1 = (prev.path == null) ? "" : prev.path;
			var p2 = (next.path == null) ? "" : next.path;

			// まだロードされてないシーンがnextに渡されることがある 
			if (!string.IsNullOrEmpty(p2) && !_scene.ContainsKey(p2)) _scene[p2] = next;

			ActiveScene = p2;
			if (_report != null) _report.OnSceneChanged(p1, p2);
		}

		public static Scene Find(string path)
		{
			if (String.IsNullOrEmpty(path)) return default(Scene);
			return _scene.ContainsKey(path) ? _scene[path] : default(Scene);
		}
	}
}
