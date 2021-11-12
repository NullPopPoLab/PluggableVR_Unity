/*!	@file
	@brief NullPopPoSpecial: シーンセレクタ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial
*/
using System;
using System.Collections.Generic;

namespace NullPopPoSpecial
{
	//! シーンセレクタ基底 
	public class SceneSelector
	{
		~SceneSelector(){
			Terminate();
		}

		public void Reset(){
			Terminate();
			OnReset();
		}

		public void Terminate(){
			OnTerminate();
		}

		public void Update(){
			OnUpdate();
		}

		protected virtual void OnReset() { }
		protected virtual void OnTerminate() { }
		protected virtual void OnUpdate() { }
	}

	//! 連番指定のシーンセレクタ 
	public class SceneList: SceneSelector
	{
		private List<SceneScopeBase> _grp = new List<SceneScopeBase>();

		protected override void OnReset() { _grp.Clear(); }

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

		public SceneScopeBase this[int idx]
		{
			get { return Get(idx); }
			set	{ Set(idx, value); }
		}

		public SceneScopeBase Get(int idx)
		{
			return (idx < _grp.Count) ? _grp[idx] : null;
		}

		public void Set(int idx, SceneScopeBase scn)
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

		public void Add(SceneScopeBase scn)
		{
			_grp.Add(scn);
		}
	}

	//! 列挙指定のシーンセレクタ 
	public class SceneEnum<T>: SceneList where T: struct
	{
		public SceneScopeBase this[T idx]
		{
			get { return Get(idx); }
			set { Set(idx, value); }
		}

		public SceneScopeBase Get(T idx)
		{
			return base.Get((int)(object)idx);
		}

		public void Set(T idx, SceneScopeBase scn)
		{
			base.Set((int)(object)idx, scn);
		}

		public void Remove(T idx)
		{
			base.Remove((int)(object)idx);
		}
	}

	//! 辞書指定のシーンセレクタ 
	public class SceneDic<Ti>: SceneSelector
	{
		private Dictionary<Ti, SceneScopeBase> _grp = new Dictionary<Ti, SceneScopeBase>();

		protected override void OnReset() { _grp.Clear(); }

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

		public SceneScopeBase this[Ti idx]
		{
			get { return Get(idx); }
			set { Set(idx, value); }
		}

		public SceneScopeBase Get(Ti idx)
		{
			return _grp.ContainsKey(idx) ? _grp[idx] : null;
		}

		public void Set(Ti idx, SceneScopeBase scn)
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
