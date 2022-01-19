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
		public virtual VRCursor Cursor { get; set; }
		public virtual Transform Pointer { get; set; }

		private bool _southpaw;
		public virtual bool Southpaw
		{
			get { return _southpaw; }
			set
			{
				_southpaw = value;
			}
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();
			if (Cursor != null) Cursor.Update();
		}
	}
}
