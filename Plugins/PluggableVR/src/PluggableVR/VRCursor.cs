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

		private Color _color = new Color(1,1,1,1);
		public virtual Color Color
		{
			get { return _color; }
			set { _color = value; }
		}

		private float _alpha = 1.0f;
		public virtual float Alpha {
			get { return _alpha; } 
			set{ _alpha = value; }
		}

		public void Update(){
			OnUpdate();
		}
		protected virtual void OnUpdate() { }
	}
}
