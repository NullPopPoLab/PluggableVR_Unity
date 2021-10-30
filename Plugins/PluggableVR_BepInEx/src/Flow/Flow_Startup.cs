/*!	@file
	@brief PluggableVR: 手順遷移 開始時 
	@author NullPopPoLab
	@sa https://github.com/NullPopPoLab/PluggableVR_Unity
*/
using UnityEngine;
using NullPopPoSpecial;

namespace PluggableVR.HS2
{
	//! 手順遷移 開始時 
	internal class Flow_Startup : Flow_Common
	{
		protected override void OnStart()
		{
			Global.Logger.LogInfo(ToString() + " bgn");
			base.OnStart();

			// Unityシーンロードに連動する遷移 
			Global.Transit["ADV"] = () => new Flow_ADV();
			Global.Transit["CharaCustom"] = () => new Flow_CharaCustom();
			Global.Transit["Home"] = () => new Flow_Home();
			Global.Transit["HScene"] = () => new Flow_HScene();
			Global.Transit["LobbyScene"] = () => new Flow_LobbyScene();
			Global.Transit["Title"] = () => new Flow_Title();

			// VR初期設定 
			var scale = 8.0f;
			var avatar = new DemoAvatar(Loc.Identity, scale);
			var player = new DemoPlayer(avatar, scale);
			player.Camera.SourceMode = VRCamera.ESourceMode.Blind;
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

			// タイトル画面遷移待ち 
			if (Global.LastLoadedScene != "Logo") return null;

			return new Flow_Delay(new Flow_Init());
		}
	}
}