/*!	@file
	@brief PluggableVR: VR操作部 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;

#pragma warning disable 0414 // 未使用メンバ警告抑制 

namespace PluggableVR
{
	internal class VRController : PlugCommon
	{
		internal VRPlayer Player { get; private set; }
		internal VRAvatar Avatar { get { return Player.Avatar; } }

		internal VRController() { }

		internal bool IsReady { get; private set; }

		//! 初期設定 
		internal void Initialize(VRPlayer player)
		{
			if (IsReady) return;
			IsReady = true;
			Player = player;
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
