/*!	@file
	@brief NullPopPoSpecial: Componentグループ 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial
*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NullPopPoSpecial
{
	//! Componentグループ基底 
	public class ComponentGroup<T> where T:Behaviour
	{
		public int Count{ get{ return OnCount(); } }

		~ComponentGroup(){
//			Terminate();
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

		public void Broadcast(Action<ComponentScope<T>> func)
		{
			OnBroadcast(func);
		}

		protected virtual int OnCount() { return 0; }
		protected virtual void OnReset() { }
		protected virtual void OnCleanup() { }
		protected virtual void OnTerminate() { }
		protected virtual void OnUpdate() { }
		protected virtual void OnBroadcast(Action<ComponentScope<T>> func) { }
	}

	//! 連番指定のオブジェクトグループ 
	public class ComponentList<T>: ComponentGroup<T> where T: Behaviour
	{
		private List<ComponentScope<T>> _grp = new List<ComponentScope<T>>();
		private List<ComponentScope<T>> _swap = new List<ComponentScope<T>>();

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

		protected override void OnBroadcast(Action<ComponentScope<T>> func) {

			for (var i = 0; i < _grp.Count; ++i)
			{
				var obj = _grp[i];
				if (obj == null) continue;
				func(obj);
			}
		}

		public ComponentScope<T> this[int idx]
		{
			get { return Get(idx); }
			set	{ Set(idx, value); }
		}

		public ComponentScope<T> Get(int idx)
		{
			return (idx < _grp.Count) ? _grp[idx] : null;
		}

		public void Set(int idx, ComponentScope<T> obj)
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

		public void Add(ComponentScope<T> obj)
		{
			_grp.Add(obj);
		}
	}

	//! 列挙指定のオブジェクトグループ 
	public class ComponentEnum<Ti,Tc>: ComponentList<Tc> where Ti: struct where Tc : Behaviour
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

		public ComponentScope<Tc> this[Ti idx]
		{
			get { return Get(idx); }
			set { Set(idx, value); }
		}

		public ComponentScope<Tc> Get(Ti idx)
		{
			return base.Get((int)(object)idx);
		}

		public void Set(Ti idx, ComponentScope<Tc> obj)
		{
			base.Set((int)(object)idx, obj);
		}

		public void Remove(Ti idx)
		{
			base.Remove((int)(object)idx);
		}
	}

	//! 辞書指定のオブジェクトグループ 
	public class ComponentDic<Ti,Tc>: ComponentGroup<Tc> where Tc: Behaviour
	{
		private Dictionary<Ti, ComponentScope<Tc>> _grp = new Dictionary<Ti, ComponentScope<Tc>>();
		private Dictionary<Ti, ComponentScope<Tc>> _swap = new Dictionary<Ti, ComponentScope<Tc>>();

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

		protected override void OnBroadcast(Action<ComponentScope<Tc>> func)
		{

			foreach (var p in _grp)
			{
				var obj = p.Value;
				if (obj == null) continue;
				func(obj);
			}
		}

		public ComponentScope<Tc> this[Ti idx]
		{
			get { return Get(idx); }
			set { Set(idx, value); }
		}

		public ComponentScope<Tc> Get(Ti idx)
		{
			return _grp.ContainsKey(idx) ? _grp[idx] : null;
		}

		public void Set(Ti idx, ComponentScope<Tc> obj)
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
