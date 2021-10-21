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

		internal bool IsReady { get; private set; }

		//! 初期設定 
		internal void Initialize(Loc loc)
		{
			if (IsReady) return;
			IsReady = true;

			Avatar = new VRAvatar(loc);
			if (OnCreateAvatar != null) OnCreateAvatar(Avatar);
			Player = new VRPlayer(Avatar);
			if (OnCreatePlayer != null) OnCreatePlayer(Player);
		}

		//! 機能終了 
		internal void Shutdown()
		{
			if (!IsReady) return;
			IsReady = false;
		}

		//! 位置だけ変更 
		internal void Repos(Vector3 pos)
		{
			if (!IsReady) return;
			Player.Repos(pos);
		}

		//! 向きだけ変更 
		internal void Rerot(Quaternion rot)
		{
			if (!IsReady) return;
			Player.Rerot(rot);
		}

		//! 位置,向き変更 
		internal void Reloc(Loc loc)
		{
			if (!IsReady) return;
			Player.Reloc(loc);
		}

		internal void Update()
		{
			if (!IsReady) return;
			Player.Update();
		}
	}
}
