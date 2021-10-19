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
		internal static Input Input { get; private set; }

		internal VRController VRController { get; private set; }

		private Camera _prevMainCamera;

		private bool _enabled;
		public bool Enabled
		{
			get { return _enabled; }
			set
			{
				if (value != _enabled) return;
				_enabled = value;
				if (value)
				{
				}
				else
				{
				}
			}
		}

		internal VRManager()
		{
		}

		internal void Initialize()
		{
			Input = Oculus.Input.Setup();

			OVRPlugin.rotation = true;
			OVRPlugin.useIPDInPositionTracking = true;
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

		internal void FixedUpdate()
		{
			Input.FixedUpdate();
		}

		internal void Update()
		{
			Input.Update();

			_switchVRController();
			if (VRController != null) VRController.Update();
		}

		internal void LateUpdate()
		{
			Input.LateUpdate();
		}
	}
}
