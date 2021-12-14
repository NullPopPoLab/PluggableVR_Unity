/*!	@file
	@brief PluggableVR: VRカーソル 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR
{
	//! VRカーソル 
	public class VRCursor: PlugCommon
	{
		public Transform Ctrl { get; protected set; }
		public Transform View { get; protected set; }
		public Transform Pointer;

		public void Update(){
			OnUpdate();
		}
		protected virtual void OnUpdate() { }
	}
}
