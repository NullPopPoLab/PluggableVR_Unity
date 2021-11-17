/*!	@file
	@brief PluggableVR: Init シーン付随動作 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_SN2
{
	//! Init シーン付随動作 
	internal class Scene_Init : SceneScope
	{
		protected override void OnStart()
		{
			Global.Logger.LogDebug(ToString() + " bgn");
			base.OnStart();

			// VR初期設定 
			var scale = 8.0f;
			var avatar = new DemoAvatar(Loc.Identity, scale);
			var player = new DemoPlayer(avatar, scale);
			VRManager.Instance.SetPlayer(player);

			// 手の軸表示を消す 
			avatar.LeftHand.Axes.Node.SetActive(false);
			avatar.RightHand.Axes.Node.SetActive(false);

			// DynamicBoneとの接触 
			var dc0 = avatar.Head.Collider.AddComponent<DynamicBoneCollider>();
			var dc1 = avatar.LeftHand.Collider.AddComponent<DynamicBoneCollider>();
			var dc2 = avatar.RightHand.Collider.AddComponent<DynamicBoneCollider>();
			dc0.m_Radius = dc1.m_Radius = dc2.m_Radius = 0.5f;
			dc0.m_Height = dc1.m_Height = dc2.m_Height = 2.0f;
			dc0.m_Direction = dc1.m_Direction = dc2.m_Direction = DynamicBoneCollider.Direction.Y;
			dc0.m_Bound = dc1.m_Bound = dc2.m_Bound = DynamicBoneCollider.Bound.Outside;
			Global.DemoAvatarExtra.HeadCollider = dc0;
			Global.DemoAvatarExtra.LeftHandCollider = dc1;
			Global.DemoAvatarExtra.RightHandCollider = dc2;
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogDebug(ToString() + " end");
			base.OnTerminate();
		}
	}
}
