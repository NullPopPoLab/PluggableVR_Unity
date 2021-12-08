/*!	@file
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
		public Transform Pointer{ get; protected set; }

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
