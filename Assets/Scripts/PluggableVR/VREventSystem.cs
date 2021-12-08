/*!	@file
	@brief PluggableVR: VR対応EventSystem 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using NullPopPoSpecial;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PluggableVR
{
	//! VR対応EventSystem 
	public class VREventSystem: ComponentScope<EventSystem>
	{
		public virtual Transform Pointer { get; set; }

		protected override bool OnAudit() {
			if (!base.OnAudit())return false;
			return true;
		}

		protected override void OnAcquired() { 
			base.OnAcquired();
		}

		protected override void OnUnacquired() {
			base.OnUnacquired();
		}
	}
}
