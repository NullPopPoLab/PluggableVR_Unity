/*!	@file
	@brief PluggableVR: VR操作部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 0414 // 未使用メンバ警告抑制 

namespace PluggableVR
{
	internal class VRController : PlugCommon
	{
		internal VRPlayer Player { get; private set; }
		internal VRAvatar Avatar { get; private set; }

		//! VRAvatar 生成時の付加動作 
		internal Action<VRAvatar> OnCreateAvatar = null;
		//! VRPlayer 生成時の付加動作 
		internal Action<VRPlayer> OnCreatePlayer = null;

		internal VRController() { }

		//! 初期設定 
		internal void Initialize(Camera cam)
		{
			var loc = Loc.FromWorldTransform(cam.transform);
			Avatar = new VRAvatar(loc);
			if (OnCreateAvatar != null) OnCreateAvatar(Avatar);
			Player = new VRPlayer(Avatar);
			if (OnCreatePlayer != null) OnCreatePlayer(Player);
		}

		//! 機能終了 
		internal void Shutdown()
		{

		}

		//! シーン変更捕捉 
		internal void SceneChanged(Scene scn)
		{
		}

		//! カメラ変更捕捉 
		internal void CameraChanged(Camera cam)
		{
			// 変更されたメインカメラ位置でリセット 
			Player.ChangeCamera(Loc.FromWorldTransform(cam.transform));
		}

		internal void Update()
		{
			Player.Update();
		}
	}
}
