/*!	@file
	@brief PluggableVR: VR対応EventSystem Oculus用 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using NullPopPoSpecial;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PluggableVR.Oculus
{
	//! VR対応EventSystem Oculus用 
	public class VREventSystem: PluggableVR.VREventSystem
	{
		private StandaloneInputModule _sim;
		private OVRInputModule _oim;
		private OVRCursor _oc;

		private VRCursor _cursor;
		public override VRCursor Cursor
		{
			get { return _cursor; }
			set {
				_cursor = value;
				_oc = (value == null) ? null : value.Ctrl.GetComponent<OVRCursor>();
				if (_oim != null) _oim.m_Cursor = _oc;
			}
		}
		public override Transform Pointer{
			get { return (_oim == null) ? null : _oim.rayTransform; }
			set {
				if (_oim != null) _oim.rayTransform = value;
				if (_cursor != null) _cursor.Pointer = value;
			}
		}

		public override bool Southpaw
		{
			get { return base.Southpaw; }
			set
			{
				base.Southpaw = value;
				_reassign();
			}
		}

		private void _reassign()
		{
			if (_oim == null) return;
			_oim.joyPadClickButton = Southpaw ? OVRInput.Button.Three : OVRInput.Button.One;
		}

		protected override void OnStart(){
			base.OnStart();
			var gobj=Target.gameObject;
			// OVRInputModule.Awake() を発生させないように一旦封印 
			gobj.SetActive(false);
			_sim= gobj.GetComponent<StandaloneInputModule>();
			if (_sim != null) _sim.enabled = false;
			_oim =gobj.GetComponent<OVRInputModule>();
			if(_oim==null)_oim=gobj.AddComponent<OVRInputModule>();
			_oim.enabled=false;
			_reassign();
		}

		protected override void OnTerminate()
		{
			_sim = null;
			_oim = null;
			base.OnTerminate();
		}

		protected override bool OnAudit() {
			if(!base.OnAudit())return false;
			if (_oim.m_Cursor == null) return false;
			if (_oim.rayTransform == null) return false;
			return true;
		}

		protected override void OnAcquired() { 
			base.OnAcquired();
			var gobj = Target.gameObject;
			gobj.SetActive(true);
			_oim.enabled = true;
		}

		protected override void OnUnacquired() {
			_oim.enabled=false;
			var gobj = Target.gameObject;
			gobj.SetActive(false);
			base.OnUnacquired();
		}
	}
}
