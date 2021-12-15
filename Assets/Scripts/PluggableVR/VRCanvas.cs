﻿/*!	@file
	@brief PluggableVR: VR対応Canvas 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using NullPopPoSpecial;
using UnityEngine;

namespace PluggableVR
{
	//! VR対応Canvas 
	public class VRCanvas: ComponentScope<Canvas>
	{
		public const float DefaultDistance = 0.5f;
		public const float DefaultScale = 0.005f;

		//! 配置先 
		public struct Placing{
			public float Distance; //!< 距離 
			public float Height; //!< 高さ 
			public float Slide; //!< 横位置 
			public Vector3 Scale; //!< 拡大率 

			public static Placing Default{ get {
					var t = new Placing();
					t.Distance = DefaultDistance;
					t.Scale = new Vector3(DefaultScale, DefaultScale, DefaultScale);
					return t;
			} }
		}

		private Placing _place = Placing.Default;
		public virtual Placing Place
		{
			get { return _place; }
			set {
				_place = value;
				if (!IsAvailable) return;

				// 現在のVRCameraから位置決め 
				var cam = VRManager.Instance.Camera;
				if (cam == null) return;
				var loc = Loc.FromWorldTransform(cam.Transform);
				loc.Pos += loc.Rot * new Vector3(value.Slide, value.Height, value.Distance);
				loc.ToWorldTransform(Transform);
				Transform.localScale = value.Scale;
			}
		}

		public Transform Pointer{ get; protected set; }

		public static VRCanvas Create(Placing place){
			var t = new VRCanvas();
			t.Place = place;
			return t;
		}

		protected override void OnStart()
		{
			base.OnStart();
			Place = _place;
		}

		protected override bool OnAudit()
		{
			if (!base.OnAudit()) return false;
			var mng = VRManager.Instance;
			var vcam = mng.Camera;
			var wcam = (vcam == null) ? null : vcam.Target;
			Target.worldCamera = wcam;
			if (wcam == null) return false;
			return true;
		}

		protected override void OnAcquired() { 
			base.OnAcquired();
			Target.renderMode = RenderMode.WorldSpace;
		}

		protected override void OnUnacquired() {
			base.OnUnacquired();
		}

		public void SetPointer(Transform src)
		{
			Pointer = src;
			OnSetPointer(src);
		}
		protected virtual void OnSetPointer(Transform src) { }
	}
}
