/*!	@file
	@brief NullPopPoSpecial: シーン遷移監視 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial
*/
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace NullPopPoSpecial
{
	//! シーン遷移監視 
	public class SceneObserverBase
	{
		public bool Enabled { get; private set; }
		public string ActiveScene { get; private set; }

		private Dictionary<string, Scene> _scene = new Dictionary<string, Scene>();
		public Dictionary<string, Scene>.KeyCollection AvailableScenes { get { return _scene.Keys; } }

		~SceneObserverBase()
		{
			Disable();
		}

		public void Enable()
		{
			if (Enabled) return;
			Enabled = true;

			SceneManager.sceneLoaded += _onSceneLoaded;
			SceneManager.sceneUnloaded += _onSceneUnloaded;
			SceneManager.activeSceneChanged += _onSceneChanged;
		}

		public void Disable()
		{
			if (!Enabled) return;
			Enabled = false;

			SceneManager.sceneLoaded -= _onSceneLoaded;
			SceneManager.sceneUnloaded -= _onSceneUnloaded;
			SceneManager.activeSceneChanged -= _onSceneChanged;
		}

		public Scene GetSceneInfo(string path){
			return _scene.ContainsKey(path) ? _scene[path] : default(Scene);
		}

		private void _onSceneLoaded(Scene scn, LoadSceneMode mode)
		{
			var p = scn.path;
			_scene[p] = scn;

			OnSceneLoaded(p, mode);
		}

		private void _onSceneUnloaded(Scene scn)
		{
			var p = scn.path;
			if (p == ActiveScene)
			{
				OnSceneChanged(ActiveScene, "");
				ActiveScene = "";
			}

			OnSceneUnloaded(p);
			if (_scene.ContainsKey(p)) _scene.Remove(p);
		}

		private void _onSceneChanged(Scene prev, Scene next)
		{
			ActiveScene = next.path;
			OnSceneChanged(prev.path, next.path);
		}

		protected virtual void OnSceneLoaded(string path, LoadSceneMode mode) { }
		protected virtual void OnSceneUnloaded(string path) { }
		protected virtual void OnSceneChanged(string prev, string next) { }
	}
}
