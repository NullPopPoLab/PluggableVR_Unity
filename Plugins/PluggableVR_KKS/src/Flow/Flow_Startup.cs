/*!	@file
	@brief PluggableVR: 手順遷移 開始時 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;
using PluggableVR;

namespace PluggableVR_KKS
{
	//! 手順遷移 開始時 
	internal class Flow_Startup : Flow
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// Unityシーンロードに連動する遷移 
			Global.Transit["Action"] = () => new Flow_Action();
			Global.Transit["OpeningScene"] = () => new Flow_OpeningScene();
			Global.Transit["Title"] = () => new Flow_Title();

			// VR初期設定 
			var scale = 1.0f;
			var avatar = new DemoAvatar(Loc.Identity, scale);
			var player = new DemoPlayer(avatar, scale);
			VRManager.Instance.SetPlayer(player);

			// 手の軸表示を消す 
			avatar.LeftHand.Axes.Node.SetActive(false);
			avatar.RightHand.Axes.Node.SetActive(false);

			// DynamicBoneとの接触を試す 
			var dc1 = avatar.LeftHand.Collider.AddComponent<DynamicBoneCollider>();
			var dc2 = avatar.RightHand.Collider.AddComponent<DynamicBoneCollider>();
			dc1.m_Radius = dc2.m_Radius = 0.5f;
			dc1.m_Height = dc2.m_Height = 2.0f;
			dc1.m_Bound = dc2.m_Bound = DynamicBoneCollider.Bound.Outside;
		}

		protected override void OnTerminate()
		{
			Global.Logger.LogInfo(ToString() + " end");
			base.OnTerminate();
		}

		protected override Flow OnUpdate()
		{
			base.OnUpdate();
			return new Flow_Logo();
		}
	}
}