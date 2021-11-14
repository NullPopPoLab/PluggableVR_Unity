/*!	@file
	@brief NullPopPoSpecial: 特定シーンが存在している間の動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial
*/

namespace NullPopPoSpecial
{
	//! 特定シーンが存在している間の動作 
	public class SceneScope
	{
		public string Path { get; private set; }
		public bool IsAvailable { get { return SceneInfo.Find(Path).isLoaded; } }
		public bool IsBusy { get; private set; }

		private bool _active;
		public bool Active{
			get{ return _active; }
			set{
				if (_active == value) return;
				_active = value;
				if (IsBusy) return;
				if (value) OnActivate();
				else OnDeactivate();
			}
		}


		public void Start(string path)
		{
			Terminate();
			Path = path;
			Start();
		}

		public void Start()
		{
			if (IsBusy) return;
			var scn = SceneInfo.Find(Path);
			if (!scn.isLoaded) return;
			IsBusy = true;
			OnStart();
			if (_active) OnActivate();
		}

		public void Terminate()
		{
			if (!IsBusy) return;
			IsBusy = false;
			if (_active) OnDeactivate();
			OnTerminate();
		}

		public void Update()
		{
			if (!IsBusy) Start();
			var scn = SceneInfo.Find(Path);
			if (!scn.isLoaded)
			{
				Terminate();
				return;
			}
			OnUpdate();
		}

		protected virtual void OnStart() { }
		protected virtual void OnTerminate() { }
		protected virtual void OnActivate() { }
		protected virtual void OnDeactivate() { }
		protected virtual void OnUpdate() { }
	}
}
