/*!	@file
	@brief PluggableVR: 手順遷移 開始時 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using System;
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.KK
{
	//! 手順遷移 開始時 
	internal class Flow_Startup : Flow
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// Unityシーンロードに連動する遷移 
			Global.Transit["Title"] =()=> new Flow_Title();
			Global.Transit["CustomScene"] =()=> new Flow_CustomScene();

			// VR初期設定 
			var scale = 1.0f;
			var avatar = new DemoAvatar(Loc.Identity, scale);
			var player = new DemoPlayer(avatar, scale);
			player.Camera.SourceMode = VRCamera.ESourceMode.Disabled;
			VRManager.Instance.SetPlayer(player);

			// 手の軸表示を消す 
			avatar.LeftHand.SetActive(false);
			avatar.RightHand.SetActive(false);
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