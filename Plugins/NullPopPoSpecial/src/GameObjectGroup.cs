/*!	@file
	@brief NullPopPoSpecial: オブジェクトグループ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial
*/
using System;
using System.Collections.Generic;

namespace NullPopPoSpecial
{
	//! オブジェクトグループ基底 
	public class GameObjectGroup
	{
		public int Count{ get{ return OnCount(); } }

		~GameObjectGroup(){
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
		protected virtual void OnReset() { }
		protected virtual void OnCleanup() { }
		protected virtual void OnTerminate() { }
		protected virtual void OnUpdate() { }
	}

	//! 連番指定のオブジェクトグループ 
	public class GameObjectList: GameObjectGroup
	{
		private List<GameObjectScope> _grp = new List<GameObjectScope>();
		private List<GameObjectScope> _swap = new List<GameObjectScope>();

		protected override int OnCount() { return _grp.Count; }

		protected override void OnReset() { _grp.Clear(); }

		protected override void OnCleanup()
		{
			var keep = _swap;
			for (var i = 0; i < _grp.Count; ++i)
			{
				var obj = _grp[i];
				if (obj == null) continue;
				if (!obj.IsBusy) continue;
				keep.Add(obj);
			}
			_grp.Clear();
			_swap = _grp;
			_grp = keep;
		}

		protected override void OnTerminate() {

			for (var i = 0; i < _grp.Count; ++i)
			{
				var obj = _grp[i];
				if (obj == null) continue;
				obj.Terminate();
			}
		}

		protected override void OnUpdate() {

			for (var i = 0; i < _grp.Count; ++i)
			{
				var obj = _grp[i];
				if (obj == null) continue;
				obj.Update();
			}
		}

		public GameObjectScope this[int idx]
		{
			get { return Get(idx); }
			set	{ Set(idx, value); }
		}

		public GameObjectScope Get(int idx)
		{
			return (idx < _grp.Count) ? _grp[idx] : null;
		}

		public void Set(int idx, GameObjectScope obj)
		{
			if (idx < _grp.Count)
			{
				Remove(idx);
				_grp[idx] = obj;
			}
			else{
				while (idx < _grp.Count) _grp.Add(null);
				_grp.Add(obj);
			}
		}

		public void Remove(int idx)
		{
			if (idx >= _grp.Count) return;
			var prev = _grp[idx];
			if (prev != null) prev.Terminate();
			_grp[idx] = null;
		}

		public void Add(GameObjectScope obj)
		{
			_grp.Add(obj);
		}
	}

	//! 列挙指定のオブジェクトグループ 
	public class GameObjectEnum<T>: GameObjectList where T: struct
	{
		protected override void OnCleanup()
		{
			var l = Count;
			for (var i = 0; i < l; ++i)
			{
				var obj = base.Get(i);
				if (obj == null) continue;
				if (obj.IsBusy) continue;
				base.Set(i, null);
			}
		}

		public GameObjectScope this[T idx]
		{
			get { return Get(idx); }
			set { Set(idx, value); }
		}

		public GameObjectScope Get(T idx)
		{
			return base.Get((int)(object)idx);
		}

		public void Set(T idx, GameObjectScope obj)
		{
			base.Set((int)(object)idx, obj);
		}

		public void Remove(T idx)
		{
			base.Remove((int)(object)idx);
		}
	}

	//! 辞書指定のオブジェクトグループ 
	public class GameObjectDic<Ti>: GameObjectGroup
	{
		private Dictionary<Ti, GameObjectScope> _grp = new Dictionary<Ti, GameObjectScope>();
		private Dictionary<Ti, GameObjectScope> _swap = new Dictionary<Ti, GameObjectScope>();

		protected override int OnCount() { return _grp.Count; }
		protected override void OnReset() { _grp.Clear(); }

		protected override void OnCleanup()
		{
			var keep = _swap;
			foreach (var p in _grp)
			{
				var obj = p.Value;
				if (obj == null) continue;
				if (!obj.IsBusy) continue;
				keep[p.Key] = obj;
			}
			_grp.Clear();
			_swap = _grp;
			_grp = keep;
		}

		protected override void OnTerminate()
		{
			foreach (var p in _grp)
			{
				var obj = p.Value;
				if (obj == null) continue;
				obj.Terminate();
			}
		}

		protected override void OnUpdate()
		{
			foreach (var p in _grp)
			{
				var obj = p.Value;
				if (obj == null) continue;
				obj.Update();
			}
		}

		public GameObjectScope this[Ti idx]
		{
			get { return Get(idx); }
			set { Set(idx, value); }
		}

		public GameObjectScope Get(Ti idx)
		{
			return _grp.ContainsKey(idx) ? _grp[idx] : null;
		}

		public void Set(Ti idx, GameObjectScope obj)
		{
			Remove(idx);
			_grp[idx] = obj;
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
