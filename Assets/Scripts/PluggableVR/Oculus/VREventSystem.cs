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
		private OVRInputModule _oim;
		private StandaloneInputModule _sim;

		public OVRCursor Cursor{
			get{ return (_oim==null)?null:_oim.m_Cursor; }
			set{ if(_oim!=null)_oim.m_Cursor = value; }
		}
		public override Transform Pointer{
			get { return (_oim == null) ? null : _oim.rayTransform; }
			set { if (_oim != null) _oim.rayTransform = value; }
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
