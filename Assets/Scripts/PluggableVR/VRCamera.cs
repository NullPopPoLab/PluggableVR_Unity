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
		public enum ERevision
		{
			Legacy,
			Unity2019,
		}
		public static ERevision Revision;

		public enum ESourceMode
		{
			Disabled, //!< 元カメラは無効化 
			Blind, //!< 元カメラの可視対象を全て外す 
		}
		public static ESourceMode SourceMode;

		public Camera Target { get; private set; }
		public VRCameraSource Source { get; private set; }

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

		//! カメラ操作部基底 
		public VRCameraController Controller{
			get{ return _controller; }
			set{
				if (_controller != null) _controller.Terminate();
				_controller = value;
				if (value != null && Source.IsAvailable) value.Start(Source.GameObject);
			}
		}
		private VRCameraController _controller;

		public VRCameraController.EPostproc Postproc{
			get{ return (_controller == null) ? VRCameraController.EPostproc.None : _controller.Postproc; }
		}

		public void DontControl(){ Controller = null; }
		public void BeActive() { Controller = new VRCameraActive(); }
		public void BePassive() { Controller = new VRCameraPassive(); }

		public bool Active
		{
			get { return Object.activeSelf; }
			set { Object.SetActive(value); }
		}

		StereoTargetEyeMask _backupStereoTargetEyeMask;

		public VRCamera(Transform rig)
		{
			Source = new VRCameraSource();

			var obj = CreateChildObject("VRCamera", rig, Loc.Identity, false);
			switch (Revision)
			{
				case ERevision.Legacy:
					Target = obj.AddComponent<Camera>();
					break;

				case ERevision.Unity2019:
					obj.AddComponent<OVRCameraRig>();
					Target = obj.transform.Find("TrackingSpace/CenterEyeAnchor").GetComponent<Camera>();
					break;
			}

			Target.nearClipPlane = 0.01f;
			Target.gameObject.AddComponent<AudioListener>();
			Suppress<AudioListener>();
		}

		//! 再設定 
		public void Reset(Camera src)
		{
			Dispose();
			Source.Start(src);
			if (src == null) return;

			Target.clearFlags = src.clearFlags;
			Target.cullingMask = src.cullingMask;
			Target.farClipPlane = src.farClipPlane;
			Target.depth = src.depth;

			// 元のメインカメラに対する措置 
			_backupStereoTargetEyeMask = src.stereoTargetEye;
			src.stereoTargetEye = StereoTargetEyeMask.None;
			switch (SourceMode)
			{
				case ESourceMode.Disabled:
					src.enabled = false;
					break;

				case ESourceMode.Blind:
					src.clearFlags = CameraClearFlags.Nothing;
					src.cullingMask = 0;
					break;
			}

			var lsn = src.GetComponent<AudioListener>();
			if (lsn != null) lsn.enabled = false;
		}

		//! 元カメラの Component 封印 
		public void Suppress<T>() where T : Behaviour
		{
			if (!Source.IsAvailable) return;

			var cs = Source.GameObject.GetComponents<T>();
			for (var i = 0; i < cs.Length; ++i)
			{
				cs[i].enabled = false;
			}
		}

		//! 元カメラの Component 移設 
		public void Possess<T>() where T : Behaviour
		{
			if (!Source.IsAvailable) return;

			var cs = Source.GameObject.GetComponents<T>();
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
			if (!Source.IsAvailable)
			{
				Dispose();
				return;
			}

			var l = _possessing.Count;
			for (var i = 0; i < l; ++i)
			{
				var t = _possessing[i];
				t.Src.enabled = t.Dst.enabled;
				Component.DestroyImmediate(t.Dst);
			}
			_possessing.Clear();

			var cam = Source.Target;
			cam.clearFlags = Target.clearFlags;
			cam.cullingMask = Target.cullingMask;
			cam.stereoTargetEye = _backupStereoTargetEyeMask;
			cam.enabled = Target.enabled;
		}

		//! 移設された Component 除去 
		public void Dispose()
		{
			var l = _possessing.Count;
			for (var i = 0; i < l; ++i)
			{
				var t = _possessing[i];
				Component.DestroyImmediate(t.Dst);
			}
			_possessing.Clear();
		}

		//! VRカメラの位置を元カメラに反映 
		public void Feedback()
		{
			if (!Source.IsAvailable) return;
			var loc = Loc.FromWorldTransform(Target.transform);
			if (_controller != null) loc *= new Loc(_controller.FeedbackOffset, Quaternion.identity);
			loc.ToWorldTransform(Source.Transform);
		}

		public void Update()
		{
			Source.Update();

			if (_controller != null)
			{
				_controller.Update();
				if (_controller.Postproc == VRCameraController.EPostproc.Feedback) Feedback();
			}
		}
	}
}
