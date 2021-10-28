/*!	@file
	@brief PluggableVR: カメラ操作機能 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! カメラ操作機能 
	public class CameraControl
	{
		public Transform Root;
		public Camera Camera;
		public Loc Loc;

		public CameraControl()
		{
			Update();
		}

		public void Update()
		{
			Root = GameObject.Find("/Camera")?.transform;
			Camera = (Root == null) ? null : Root.Find("Main Camera")?.GetComponent<Camera>();
			Loc = (Camera == null) ? Loc.Identity : Loc.FromWorldTransform(Camera.transform);
		}
	}
}
