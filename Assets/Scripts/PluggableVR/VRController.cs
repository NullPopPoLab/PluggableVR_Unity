/*!	@file
	@brief PluggableVR: VR操作部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 0414 // 未使用メンバ警告抑制 

namespace PluggableVR
{
	internal class VRController : PlugCommon
	{
		private Camera _mainCamera;
		internal VRPlayer Player { get; private set; }
		internal VRAvatar Avatar { get; private set; }

		internal VRController(Camera mc)
		{
			_mainCamera = mc;

			var loc = Loc.FromWorldTransform(mc.transform);
			Avatar = new VRAvatar(loc);
			Player = new VRPlayer(loc, Avatar);

			Avatar.LeftHand.SetActive(false);
			Avatar.RightHand.SetActive(false);

			var cam = mc.GetComponent<Camera>();
			if (cam != null) cam.enabled = false;
			var lsn = mc.GetComponent<AudioListener>();
			if (lsn != null) lsn.enabled = false;
		}

		//! シーン変更捕捉 
		internal void SceneChanged(Scene scn)
		{
			// 現在のメインカメラ位置でリセット 
			Player.ChangeCamera(Loc.FromWorldTransform(_mainCamera.transform));
		}

		//! メインカメラ変更捕捉 
		internal void MainCameraChanged(Camera mc)
		{
			// 変更されたメインカメラ位置でリセット 
			_mainCamera = mc;
			Player.ChangeCamera(Loc.FromWorldTransform(mc.transform));
		}

		internal void Update()
		{
			Player.Update();
		}
	}
}
