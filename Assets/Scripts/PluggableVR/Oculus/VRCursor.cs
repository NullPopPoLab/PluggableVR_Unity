/*!	@file
	@brief PluggableVR: VRカーソル 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using NullPopPoSpecial;
using UnityEngine;

namespace PluggableVR.Oculus
{
	//! VRカーソル 
	public class VRCursor: ComponentScope<OVRGazePointer>
	{
		public Transform From;

		protected override void OnStart() {
			base.OnStart();
			Target.enabled=false;
		}

		protected override bool OnAudit() {
			if(From==null)return false;
			if(!base.OnAudit())return false;
			return true;
		}

		protected override void OnAcquired() { 
			base.OnAcquired();
			Target.rayTransform=From;
			Target.enabled=true;
		}

		protected override void OnUnacquired() {
			Target.enabled=false;
			base.OnUnacquired();
		}
	}
}
