/*!	@file
	@brief NullPopPoSpecial: シーングループ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial
*/
using System;
using System.Collections.Generic;

namespace NullPopPoSpecial
{
	//! シーングループ基底 
	public class SceneGroup
	{
		public int Count { get { return OnCount(); } }

		~SceneGroup(){
			Terminate();
		}

		public void Reset(){
			Terminate();
			OnReset();
		}

		public void Cleanup()
		{
			OnCleanup();
		}

		public void Terminate(){
			OnTerminate();
		}

		public void Update(){
			OnUpdate();
		}

		protected virtual int OnCount() { return 0; }
		protected virtual void OnCleanup() { }
		protected virtual void OnReset() { }
		protected virtual void OnTerminate() { }
		protected virtual void OnUpdate() { }
	}

	//! 連番指定のシーングループ 
	public class SceneList: SceneGroup
	{
		private List<SceneScope> _grp = new List<SceneScope>();
		private List<SceneScope> _swap = new List<SceneScope>();

		protected override int OnCount() { return _grp.Count; }
		protected override void OnReset() { _grp.Clear(); }

		protected override void OnCleanup()
		{
			var keep = _swap;
			for (var i = 0; i < _grp.Count; ++i)
			{
				var scn = _grp[i];
				if (scn == null) continue;
				if (!scn.IsBusy) continue;
				keep.Add(scn);
			}
			_grp.Clear();
			_swap = _grp;
			_grp = keep;
		}

		protected override void OnTerminate() {

			for (var i = 0; i < _grp.Count; ++i)
			{
				var scn = _grp[i];
				if (scn == null) continue;
				scn.Terminate();
			}
		}

		protected override void OnUpdate() {

			for (var i = 0; i < _grp.Count; ++i)
			{
				var scn = _grp[i];
				if (scn == null) continue;
				scn.Update();
			}
		}

		public SceneScope this[int idx]
		{
			get { return Get(idx); }
			set	{ Set(idx, value); }
		}

		public SceneScope Get(int idx)
		{
			return (idx < _grp.Count) ? _grp[idx] : null;
		}

		public void Set(int idx, SceneScope scn)
		{
			if (idx < _grp.Count)
			{
				Remove(idx);
				_grp[idx] = scn;
			}
			else{
				while (idx < _grp.Count) _grp.Add(null);
				_grp.Add(scn);
			}
		}

		public void Remove(int idx)
		{
			if (idx >= _grp.Count) return;
			var prev = _grp[idx];
			if (prev != null) prev.Terminate();
			_grp[idx] = null;
		}

		public void Add(SceneScope scn)
		{
			_grp.Add(scn);
		}
	}

	//! 列挙指定のシーングループ 
	public class SceneEnum<T>: SceneList where T: struct
	{
		protected override void OnCleanup()
		{
			var l = Count;
			for (var i = 0; i < l; ++i)
			{
				var scn = base.Get(i);
				if (scn == null) continue;
				if (scn.IsBusy) continue;
				base.Set(i, null);
			}
		}

		public SceneScope this[T idx]
		{
			get { return Get(idx); }
			set { Set(idx, value); }
		}

		public SceneScope Get(T idx)
		{
			return base.Get((int)(object)idx);
		}

		public void Set(T idx, SceneScope scn)
		{
			base.Set((int)(object)idx, scn);
		}

		public void Remove(T idx)
		{
			base.Remove((int)(object)idx);
		}
	}

	//! 辞書指定のシーングループ 
	public class SceneDic<Ti>: SceneGroup
	{
		private Dictionary<Ti, SceneScope> _grp = new Dictionary<Ti, SceneScope>();
		private Dictionary<Ti, SceneScope> _swap = new Dictionary<Ti, SceneScope>();

		protected override int OnCount() { return _grp.Count; }
		protected override void OnReset() { _grp.Clear(); }

		protected override void OnCleanup()
		{
			var keep = _swap;
			foreach (var p in _grp)
			{
				var scn = p.Value;
				if (scn == null) continue;
				if (!scn.IsBusy) continue;
				keep[p.Key] = scn;
			}
			_grp.Clear();
			_swap = _grp;
			_grp = keep;
		}

		protected override void OnTerminate()
		{
			foreach (var p in _grp)
			{
				var scn = p.Value;
				if (scn == null) continue;
				scn.Terminate();
			}
		}

		protected override void OnUpdate()
		{
			foreach (var p in _grp)
			{
				var scn = p.Value;
				if (scn == null) continue;
				scn.Update();
			}
		}

		public SceneScope this[Ti idx]
		{
			get { return Get(idx); }
			set { Set(idx, value); }
		}

		public SceneScope Get(Ti idx)
		{
			return _grp.ContainsKey(idx) ? _grp[idx] : null;
		}

		public void Set(Ti idx, SceneScope scn)
		{
			Remove(idx);
			_grp[idx] = scn;
		}

		public void Remove(Ti idx)
		{
			if (!_grp.ContainsKey(idx)) return;
			var prev = _grp[idx];
			if (prev != null) prev.Terminate();
			_grp.Remove(idx);
		}
	}
}
