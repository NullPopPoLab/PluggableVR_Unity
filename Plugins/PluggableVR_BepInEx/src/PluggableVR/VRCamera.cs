/*!	@file
	@brief PluggableVR: VRカメラ管理 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using System.Collections.Generic;
using NullPopPoSpecial;

namespace PluggableVR
{
	//! VRカメラ管理 
	public class VRCamera : PlugCommon
	{
		public enum ESourceMode
		{
			Disabled, //!< 元カメラは無効化 
			Blind, //!< 元カメラの可視対象を全て外す 
		}

		public ESourceMode SourceMode;
		public Camera Target { get; private set; }
		public Camera Source { get; private set; }

		public Transform Transform { get { return Target.transform; } }
		public GameObject Object { get { return Target.gameObject; } }

		public struct Possessing
		{
			public Behaviour Src;
			public Behaviour Dst;

			public Possessing(Behaviour src, Behaviour dst)
			{
				Src = src;
				Dst = dst;
			}
		}
		private List<Possessing> _possessing = new List<Possessing>();

		public bool Active
		{
			get { return Object.activeSelf; }
			set { Object.SetActive(value); }
		}

		public VRCamera(Transform rig)
		{
			SourceMode = ESourceMode.Disabled;

			var obj = CreateChildObject("VRCamera", rig, Loc.Identity, false);
			Target = obj.AddComponent<Camera>();
			Target.nearClipPlane = 0.01f;
			Target.gameObject.AddComponent<AudioListener>();

			Suppress<AudioListener>();
		}

		//! 再設定 
		public void Reset(Camera src)
		{
			_possessing.Clear();
			Source = src;
			if (src == null) return;

			// 元のメインカメラに対する措置 
			switch (SourceMode)
			{
				case ESourceMode.Disabled:
					Source.enabled = false;
					break;

				case ESourceMode.Blind:
					Source.cullingMask = 0;
					break;
			}

			var lsn = src.GetComponent<AudioListener>();
			if (lsn != null) lsn.enabled = false;
		}

		//! 元カメラの Component 封印 
		public void Suppress<T>() where T : Behaviour
		{
			if (Source == null) return;

			var cs = Source.GetComponents<T>();
			for (var i = 0; i < cs.Length; ++i)
			{
				cs[i].enabled = false;
			}
		}

		//! 元カメラの Component 移設 
		public void Possess<T>() where T : Behaviour
		{
			if (Source == null) return;

			var cs = Source.GetComponents<T>();
			for (var i = 0; i < cs.Length; ++i)
			{
				var src = cs[i];
				var dst = ComponentUt.Possess(src, Target.gameObject);
				if (dst == null) continue;
				_possessing.Add(new Possessing(src, dst));
			}
		}

		//! 移設された Component 返却 
		public void Recall()
		{
			var l = _possessing.Count;
			for (var i = 0; i < l; ++i)
			{
				var t = _possessing[i];
				t.Src.enabled = t.Dst.enabled;
				Component.Destroy(t.Dst);
			}
		}

		//! 移設された Component 除去 
		public void Dispose()
		{
			var l = _possessing.Count;
			for (var i = 0; i < l; ++i)
			{
				var t = _possessing[i];
				Component.Destroy(t.Dst);
			}
		}
	}
}
