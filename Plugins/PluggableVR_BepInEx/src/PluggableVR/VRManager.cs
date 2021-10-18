/*!	@file
	@brief PluggableVR: VRシステム管理 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PluggableVR
{
	internal class VRManager : PlugCommon
	{
		internal VRController VRController { get; private set; }

		private Camera _prevMainCamera;

		internal VRManager()
		{
		}

		internal void SceneChanged(Scene scn)
		{

			if (VRController != null) VRController.SceneChanged(scn);
		}

		private void _switchVRController()
		{

			// メインカメラが捕捉できないうちは何もしない 
			var mc = Camera.main;
			if (mc == null) return;

			if (VRController == null)
			{
				// ここでVR操作開始可 
				VRController = new VRController(mc);
			}
			else if (mc != _prevMainCamera)
			{
				// メインカメラ変更捕捉 
				_prevMainCamera = mc;
				VRController.MainCameraChanged(mc);
			}
		}

		internal void Update()
		{
			_switchVRController();
			if (VRController != null) VRController.Update();
		}
	}
}
