/*!	@file
	@brief PluggableVR: VR対応Canvas Oculus用 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using UnityEngine.UI;

namespace PluggableVR.Oculus
{
	//! VR対応Canvas Oculus用 
	public class VRCanvas: PluggableVR.VRCanvas
	{
		private GraphicRaycaster _grc;
		private OVRRaycaster _orc;

		public new static VRCanvas Create(Placing place)
		{
			var t = new VRCanvas();
			t.Place = place;
			return t;
		}

		protected override void OnStart()
		{
			base.OnStart();
			var gobj = Target.gameObject;
			_grc = gobj.GetComponent<GraphicRaycaster>();
			if (_grc != null) _grc.enabled = false;
			if (_orc == null) _orc = gobj.AddComponent<OVRRaycaster>();
			_orc.enabled = false;
			_orc.pointer = (Pointer == null) ? null : Pointer.gameObject;
		}

		protected override bool OnAudit()
		{
			if (!base.OnAudit()) return false;
			if (_orc.pointer == null) return false;
			return true;
		}

		protected override void OnAcquired() { 
			base.OnAcquired();
			_orc.enabled = true;
		}

		protected override void OnUnacquired() {
			if(_orc!=null)_orc.enabled = false;
			base.OnUnacquired();
		}

		protected override void OnSetPointer(Transform src) {
			base.OnSetPointer(src);
			if (_orc != null) _orc.pointer = (src == null) ? null : src.gameObject;
		}
	}
}
