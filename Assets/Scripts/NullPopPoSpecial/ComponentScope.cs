/*!	@file
	@brief NullPopPoSpecial: 特定Componentが存在している間の動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/NullPopPoSpecial
*/
using System;
using UnityEngine;

namespace NullPopPoSpecial
{
	//! 特定Componentが存在している間の動作 
	public class ComponentScope<T> where T : Behaviour
	{
		public T Target { get; private set; }
		public GameObject GameObject { get { return (Target == null) ? null : Target.gameObject; } }
		public Transform Transform { get { return (Target == null) ? null : Target.transform; } }
		public RectTransform RectTransform { get { return Transform as RectTransform; } }

		public bool IsAvailable { get { return Target != null; } }
		public bool IsEnabled { get { return (Target == null) ? false : Target.enabled; } }
		public bool IsBusy { get; private set; }

		public Loc LocalLoc { get { return Loc.FromLocalTransform(Transform); } }
		public Loc WorldLoc { get { return Loc.FromWorldTransform(Transform); } }


		private ChangingCensor<string> _name;
		public string Name { get { return _name.Current; } }
		public bool NameChanged
		{
			get
			{
				if (Target == null) return false;
				return _name.Update(Target.name);
			}
		}

		private string _nowPath
		{
			get
			{
				var path = "/" + Target.name;
				for (var p = Target.transform.parent; p != null; p = p.parent)
				{
					path = "/" + p.name + path;
				}
				return path;
			}
		}

		private ChangingCensor<string> _path;
		public string Path { get { return _path.Current; } }
		public bool PathChanged
		{
			get
			{
				if (Target == null) return false;
				return _path.Update(_nowPath);
			}
		}

		public void Start(string path)
		{
			Terminate();
			_path.Reset((path == null) ? "" : path);
			Start();
		}

		public void Start(T target)
		{
			Terminate();
			Target = target;
			if (Target == null) return;
			_path.Reset(_nowPath);
			Start();
		}

		public void Start()
		{
			if (IsBusy) return;
			if (Target == null)
			{
				if (String.IsNullOrEmpty(_path.Current)) return;
				var obj = GameObject.Find(_path.Current);
				Target = (obj == null) ? null : obj.GetComponent<T>();
				if (Target == null) return;
			}
			IsBusy = true;
			_name.Reset(Target.name);
			OnStart();
		}

		public void Terminate()
		{
			if (!IsBusy) return;
			IsBusy = false;
			OnTerminate();
			Target = null;
		}

		public void Replace(T target)
		{
			if (Target == null)
			{
				Start(target);
				return;
			}
			if (target != null)
			{
				if (target.GetInstanceID() == Target.GetInstanceID()) return;
			}
			Start(target);
		}

		public void Update()
		{
			if (!IsBusy) Start();
			if (Target == null)
			{
				Terminate();
				return;
			}
			OnUpdate();
		}

		protected virtual void OnStart() { }
		protected virtual void OnTerminate() { }
		protected virtual void OnUpdate() { }
	}
}
